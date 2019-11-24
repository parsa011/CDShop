using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Shop.Common.ViewModels.Account;
using Shop.Data;
using Shop.Data.UnitOfWork;
using Shop.Domain.Core;
using Shop.Domain.Entities;
using Shop.Services.Interfaces;
using Shop.Services.Sender;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Shop.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly UnitOfWork _db;
        private readonly ISender _sender;

        public AuthController(UnitOfWork db)
        {
            _sender = new SmsSender();
            _db = db;
        }

        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            if (User.Identity.IsAuthenticated == false)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = _db.UsersGenericRepository
                    .where(u => u.PasswordHash == PasswordHash.HashWithMD5(model.Password)
                    & u.PhoneNumber == model.PhoneNumber).FirstOrDefault();
                if (user != null)
                {
                    if (user.IsActive)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                            new Claim(ClaimTypes.Role,_db.RolesGenericRepository.GetById(user.RoleId).Name),
                            new Claim(ClaimTypes.Name,user.PhoneNumber),
                            new Claim(ClaimTypes.GivenName,user.Name)
                        };
                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);
                        var properties = new AuthenticationProperties()
                        {
                            IsPersistent = model.IsRemember
                        };
                        HttpContext.SignInAsync(principal, properties);
                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("PhoneNumber", "اکانت شما فعال نیست");
                    }
                    return View();
                }
                else
                {
                    ModelState.AddModelError("PhoneNumber", "اطلاعات وارد شده صحیح نمی باشد");
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_db.UsersGenericRepository.where(u => u.PhoneNumber == model.PhoneNumber).Any())
                {
                    model.PhoneNumber = null;
                    ModelState.AddModelError("PhoneNumber", "چنین شماره ای وجود دارد");
                    return View(model);
                }
                User user = new User
                {
                    IsActive = false,
                    PhoneNumber = model.PhoneNumber,
                    ActivationCode = GuidGenerator.NewGuid(),
                    PasswordHash = PasswordHash.HashWithMD5(model.Password),
                    RoleId = _db.RolesGenericRepository.where(r => r.Name == "User").FirstOrDefault().Id,
                    Name = model.FullName
                };
                _db.UsersGenericRepository.Insert(user);
                _db.Save();
                _sender.Send();
                ViewData["activationCode"] = user.ActivationCode;
                return RedirectToAction("ActivateAccount");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult ActivateAccount()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult ActivateAccount(ActivateAccount model)
        {
            if (ModelState.IsValid)
            {
                var user = _db.UsersGenericRepository.where(u => u.ActivationCode == model.Code).FirstOrDefault();
                if (user != null)
                {
                    user.ActivationCode = GuidGenerator.NewGuid();
                    user.IsActive = true;
                    _db.UsersGenericRepository.Update(user);
                    _db.Save();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Code", "کد فعالسازی صحیح نیست");
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult SignOut(string ReturnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                if (Url.IsLocalUrl(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}