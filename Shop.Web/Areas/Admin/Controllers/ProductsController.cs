﻿using System;
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
    public class ProductsController : Controller
    {
        private readonly UnitOfWork _db;
        public ProductsController(UnitOfWork db)
        {
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
    }
}