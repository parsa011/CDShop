using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Common.UserViewModel;
using Shop.Data.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using Shop.Common.UserViewModel;
using Shop.Data;

namespace Shop.Web.Areas.Admin.Controllers
{
    [Area(areaName: "Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UnitOfWork _db;

        public UsersController(UnitOfWork db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var model = new List<UsersListViewModel>();
            foreach (var item in _db.UsersGenericRepository.where())
            {
                model.Add(new UsersListViewModel
                {
                    Name = item.Name,
                    PhoneNumber = item.PhoneNumber,
                    IsActive = item.IsActive,
                    Id = item.Id,
                    ActivationCode = item.ActivationCode,
                    Role = _db.RolesGenericRepository.GetById(item.RoleId).Name
                });
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var user = _db.UsersGenericRepository.GetById(id);
                var model = new UserEditViewModel
                {
                    Name = user.Name,
                    PhoneNumber = user.PhoneNumber,
                    IsActive = user.IsActive,
                    Id = user.Id,
                    ActivationCode = user.ActivationCode,
                    IsAdmin = (_db.RolesGenericRepository.GetById(user.RoleId).Name == "Admin") ? true : false
                };
                return View(model);
            }

            return Redirect("/Admin/Users/Index");
        }

        [HttpPost]
        public IActionResult Edit(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _db.UsersGenericRepository.where(u => u.Id == model.Id).FirstOrDefault();
                user.Name = model.Name;
                user.ActivationCode = model.ActivationCode;
                user.IsActive = model.IsActive;
                user.PhoneNumber = model.PhoneNumber;
                if (model.IsAdmin)
                {
                    user.RoleId = _db.RolesGenericRepository.where(r => r.Name == "Admin").FirstOrDefault().Id;
                }
                else
                {
                    user.RoleId = _db.RolesGenericRepository.where(r => r.Name == "User").FirstOrDefault().Id;
                }

                if (!string.IsNullOrEmpty(model.PassWord))
                {
                    user.PasswordHash = PasswordHash.HashWithMD5(model.PassWord);
                }

                _db.UsersGenericRepository.Update(user);
                _db.Save();
                return Redirect("/Admin/Users/Index");
            }
            else
            {
                return View(model);
            }
        }
    }
}