using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Data.UnitOfWork;

namespace Shop.Web.ViewComponents.CategoriesComponent
{
    public class CategoriesComponent : ViewComponent
    {
        private readonly UnitOfWork _db;
        public CategoriesComponent(UnitOfWork db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync(string productId)
        {
            return await Task.FromResult((IViewComponentResult)
                View("CategoriesComponent", _db.CategoriesGenericRepository.where()));
        }
    }
}
