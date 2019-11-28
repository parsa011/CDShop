using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Common.ViewModels;
using Shop.Data.UnitOfWork;
using Shop.Domain.Entities;

namespace Shop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly UnitOfWork _db;

        public HomeController(UnitOfWork db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var model = new List<ProductListViewModel>();
            foreach (var item in _db.ProductsGenericRepository.where())
            {
                model.Add(new ProductListViewModel
                {
                    Id = item.Id,
                    Category = _db.CategoriesGenericRepository.GetById(item.CategoryId),
                    ImagePath = _db.ProductImagesGenericRepository.where(i => i.ProductId == item.Id).FirstOrDefault().ImagePath,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    Summary = item.Summary,
                    Title = item.Title
                });
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Details(string id, string title)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var model = new ProductDetailsViewModel();
                model.Product = _db.ProductsGenericRepository.where(p => p.Id == id).FirstOrDefault();
                model.Product.Category = _db.CategoriesGenericRepository.where(c => c.Id == model.Product.CategoryId)
                    .FirstOrDefault();
                model.Images = _db.ProductImagesGenericRepository.where(i => i.ProductId == model.Product.Id).ToList();
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult AddComment(string Name, string Email, string Content, string ProductId)
        {
            if (Name == null || Email == null || Content == null || ProductId == null)
            {
                return Redirect("/Home/index/");
            }
            var cm = new Comment
            {
                ProductId = ProductId,
                Content = Content,
                Email = Email,
                Name = Name,
                CreatedTime = DateTime.Now,
                IsDeleted = false
            };
            _db.CommentsGenericRepository.Insert(cm);
            _db.Save();
            return Redirect("/Home/Details/" + ProductId);
        }

        public IActionResult ProductsInGroup(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction(nameof(Index));
            }
            var model = new List<ProductListViewModel>();
            foreach (var item in _db.ProductsGenericRepository.where(p => p.CategoryId == int.Parse(id)))
            {
                model.Add(new ProductListViewModel
                {
                    Id = item.Id,
                    Category = _db.CategoriesGenericRepository.GetById(item.CategoryId),
                    ImagePath = _db.ProductImagesGenericRepository.where(i => i.ProductId == item.Id).FirstOrDefault().ImagePath,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    Summary = item.Summary,
                    Title = item.Title
                });
            }
            return View(model);
        }
    }
}