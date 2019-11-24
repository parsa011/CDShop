using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Common.ViewModels;
using Shop.Data.UnitOfWork;
using Shop.Domain.Entities;

namespace Shop.Web.Areas.Admin.Controllers
{
    [Area(areaName: "Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private readonly UnitOfWork _db;

        public CategoriesController(UnitOfWork db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var model = new List<CategoryListViewModels>();
            foreach (var item in _db.CategoriesGenericRepository.where())
            {
                model.Add(new CategoryListViewModels
                {
                    Id = item.Id,
                    Parent = (_db.CategoriesGenericRepository.GetById(item.ParentId) != null) ? _db.CategoriesGenericRepository.GetById(item.ParentId).Title : "",
                    ParentId = (_db.CategoriesGenericRepository.GetById(item.ParentId) != null) ? _db.CategoriesGenericRepository.GetById(item.ParentId).Id : 0,
                    Title = item.Title
                });
            }
            
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(string Title,int Parent)
        {
            if (string.IsNullOrEmpty(Title))
            {
                return Redirect("/Admin/Categories/Index");
            }
            var category = new Category
            {
                Title = Title,
                ParentId = Parent
            };
            _db.CategoriesGenericRepository.Insert(category);
            _db.Save();
            return Redirect("/Admin/Categories/Index");
        }

        [HttpPost]
        public IActionResult Edit(int cId,string Title,int Parent)
        {
            if (!string.IsNullOrEmpty(Title))
            {
                var category = _db.CategoriesGenericRepository.GetById(cId);
                if (category == null)
                {
                    return Redirect("/Admin/Categories/Index");
                }
                category.ParentId = Parent;
                category.Title = Title;
                _db.CategoriesGenericRepository.Update(category);
                _db.Save();
            }
            return Redirect("/Admin/Categories/Index");
        }

        [HttpGet]

        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                _db.CategoriesGenericRepository.Delete(id);
                _db.Save();
            }
            return Redirect("/Admin/Categories/Index");
        }
    }
}