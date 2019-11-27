using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Common.ViewModels.AddressViewModel;
using Shop.Data.UnitOfWork;
using Shop.Domain.Entities;

namespace Shop.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UnitOfWork _db;
        public ProfileController(UnitOfWork db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Addresses()
        {
            return View(_db.AddressesGenericRepository.where(a => a.UserId == User.FindFirst(ClaimTypes.NameIdentifier).Value));
        }

        [HttpGet]
        public IActionResult AddAddress()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddAddress(AddressCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var address = new Address
                {
                    City = model.City,
                    FullAddress = model.FullAddress,
                    IsDeleted = false,
                    PhoneNumber = model.PhoneNumber,
                    PostAddress = model.PostAddress,
                    Title = model.Title,
                    UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value
                };
                _db.AddressesGenericRepository.Insert(address);
                _db.Save();
                return RedirectToAction(nameof(Addresses));
            }
            else
            {
                return View(model);
            }
        }

        public IActionResult DeleteAddress(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                _db.AddressesGenericRepository.Delete(id);
                _db.Save();
            }
            return RedirectToAction(nameof(Addresses));
        }

        [HttpGet]
        public IActionResult EditAddress(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var address = _db.AddressesGenericRepository.GetById(id);
                var model = new AddressCreateViewModel
                {
                    City = address.City,
                    FullAddress = address.FullAddress,
                    PhoneNumber = address.PhoneNumber,
                    PostAddress = address.PostAddress,
                    Title = address.Title,
                    Id = address.Id
                };
                return View(model);
            }
            return RedirectToAction(nameof(Addresses));
        }

        [HttpPost]
        public IActionResult EditAddress(AddressCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var address = _db.AddressesGenericRepository.GetById(model.Id);
                address.City = model.City;
                address.FullAddress = model.FullAddress;
                address.PhoneNumber = model.PhoneNumber;
                address.PostAddress = model.PostAddress;
                address.Title = model.Title;
                address.Id = model.Id;
                _db.AddressesGenericRepository.Update(address);
                _db.Save();
                return RedirectToAction(nameof(Addresses));
            }
            else
            {
                return View(model);
            }
        }
    }
}