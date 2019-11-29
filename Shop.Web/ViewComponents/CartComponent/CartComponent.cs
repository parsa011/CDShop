using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Common.ViewModels.CartViewModel;
using Shop.Data.UnitOfWork;

namespace Shop.Web.ViewComponents.CartComponent
{
    public class CartComponent : ViewComponent
    {
        private readonly UnitOfWork _db;

        public CartComponent(UnitOfWork db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<ShowCartViewModel> model = new List<ShowCartViewModel>();
            if (User.Identity.IsAuthenticated)
            {
                string currentUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var oredr = _db.OrdersGenericRepository.where( o=> o.UserId == currentUserId && !o.IsFinally)
                    .SingleOrDefault();
                if (oredr != null)
                {
                    model.AddRange(_db.OrderDetailsGenericRepository.where(o => o.OrderId == oredr.Id)
                        .Select(s=> new ShowCartViewModel
                        {
                            Count = s.Count,
                            ImageName = _db.ProductImagesGenericRepository.where(i => i.ProductId == s.ProductId).FirstOrDefault().ImagePath,
                            Title = _db.ProductsGenericRepository.GetById(s.ProductId).Title
                        }).ToList());
                }

            }
            return View("CartComponent", model);
        }
    }
}
