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
        public IActionResult Create(ProductCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                List<string> imagesName = new List<string>();
                foreach (var item in model.Images.Where(i => i.ContentType == "image/jpeg" || i.ContentType == "image/png"))
                {
                    imagesName.Add(SaveImage(item));
                }
                if (!imagesName.Any())
                {
                    ModelState.AddModelError("Images","لطفا عکس را به درستی انتخاب نمایید");
                    return View(model);
                }
                var pr = new Product
                {
                    CategoryId = model.CategoryId,
                    Category = _db.CategoriesGenericRepository.GetById(model.CategoryId),
                    Content = model.Content,
                    CreatedTime = DateTime.Now,
                    DiskCount = model.DiskCount,
                    HasBox = model.HasBox,
                    HasHelp = model.HasHelp,
                    IsDeleted = false,
                    Price = model.Price,
                    Quantity = model.Quantity,
                    Summary = model.Summary,
                    Title = model.Title
                };
                _db.ProductsGenericRepository.Insert(pr);
                foreach (var item in imagesName)
                {
                    var img = new ProductImage
                    {
                        ImagePath = item,
                        ProductId = pr.Id,
                    };
                    _db.ProductImagesGenericRepository.Insert(img);
                }
                _db.Save();
                return Redirect("/Admin/Products/index");
            }
            ViewBag.Categories = _db.CategoriesGenericRepository.where().ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var pr = _db.ProductsGenericRepository.GetById(id);
                var vm = new ProductCreateViewModel
                {
                    CategoryId = pr.CategoryId,
                    Content = pr.Content,
                    DiskCount = pr.DiskCount,
                    HasBox = pr.HasBox,
                    HasHelp = pr.HasHelp,
                    Id = pr.Id,
                    Price = pr.Price,
                    Quantity = pr.Quantity,
                    Summary = pr.Summary,
                    Title = pr.Title
                };
                ViewBag.Categories = _db.CategoriesGenericRepository.where().ToList();
                return View(vm);
            }
            return Redirect("/Admin/Products/index");
        }

        [HttpPost]
        public IActionResult Edit(ProductCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                List<string> imagesName = new List<string>();
                foreach (var item in model.Images.Where(i => i.ContentType == "image/jpeg" || i.ContentType == "image/png"))
                {
                    imagesName.Add(SaveImage(item));
                }
                if (!imagesName.Any())
                {
                    ModelState.AddModelError("Images", "لطفا عکس را به درستی انتخاب نمایید");
                    return View(model);
                }
                var pr = new Product
                {
                    CategoryId = model.CategoryId,
                    Category = _db.CategoriesGenericRepository.GetById(model.CategoryId),
                    Content = model.Content,
                    CreatedTime = DateTime.Now,
                    DiskCount = model.DiskCount,
                    HasBox = model.HasBox,
                    HasHelp = model.HasHelp,
                    IsDeleted = false,
                    Price = model.Price,
                    Id = model.Id,
                    Quantity = model.Quantity,
                    Summary = model.Summary,
                    Title = model.Title
                };
                _db.ProductsGenericRepository.Update(pr);
                foreach (var item in _db.ProductImagesGenericRepository.where(i => i.ProductId == model.Id))
                {
                    _db.ProductImagesGenericRepository.Delete(item);
                }
                foreach (var item in imagesName)
                {
                    var img = new ProductImage
                    {
                        ImagePath = item,
                        ProductId = pr.Id,
                    };
                    _db.ProductImagesGenericRepository.Insert(img);
                }
                _db.Save();
                return Redirect("/Admin/Products/index");
            }
            ViewBag.Categories = _db.CategoriesGenericRepository.where().ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                _db.ProductsGenericRepository.Delete(id);
                _db.Save();
            }
            return Redirect("/Admin/Products/index");
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