using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Common.ViewModels;
using Shop.Data.UnitOfWork;
using Shop.Domain.Entities;

namespace Shop.Web.Areas.Admin.Controllers
{
    [Area(areaName: "Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly UnitOfWork _db;
        private readonly IHostingEnvironment _environment;
        public ProductsController(UnitOfWork db, IHostingEnvironment environment)
        {
            _environment = environment;
            _db = db;
        }

        public IActionResult Index()
        {
            var products = new List<ProductListViewModel>();
            foreach (var item in _db.ProductsGenericRepository.where())
            {
                products.Add(new ProductListViewModel
                {
                    Category = _db.CategoriesGenericRepository.where(c => c.Id == item.CategoryId).FirstOrDefault(),
                    Id = item.Id,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    Summary = item.Summary,
                    Title = item.Title
                }); ;
            }
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = _db.CategoriesGenericRepository.where().ToList();
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(ProductCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in model.Images.Where(i => i.ContentType == "image/jpeg" || i.ContentType == "image/png"))
                {
                    
                }
            }
            return View(model);
        }

        private string SaveImage(IFormFile model)
        {
            var uploadsRootFolder = Path.Combine(_environment.WebRootPath, "Uploads");
            if (!Directory.Exists(uploadsRootFolder))
            {
                Directory.CreateDirectory(uploadsRootFolder);
            }
            var filePath = Path.Combine(uploadsRootFolder, model.FileName);
            if (model.ContentType == "image/jpeg" || model.ContentType == "image/png")
            {
                using var fileStream = new FileStream(filePath, FileMode.Create);
                model.CopyToAsync(fileStream).ConfigureAwait(false);
            }
            return "/Uploads/" + model.FileName;
        }
    }
}