using Microsoft.AspNetCore.Mvc;
using Shop.Data.UnitOfWork;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Web.ViewComponents.CommentComponent
{
    public class CommentComponent : ViewComponent
    {
        private readonly UnitOfWork _db;
        public CommentComponent(UnitOfWork db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync(string productId)
        {
            return await Task.FromResult((IViewComponentResult)
                View("CommentComponent", _db.CommentsGenericRepository.where(c => c.ProductId == productId).ToList()));
        }
    }
}
