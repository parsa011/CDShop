using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Data.UnitOfWork;

namespace Shop.Web.Areas.Admin.Controllers
{
    [Area(areaName: "Admin")]
    [Authorize(Roles = "Admin")]
    public class CommentsController : Controller
    {
        private readonly UnitOfWork _db;
        public CommentsController(UnitOfWork db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.CommentsGenericRepository.where());
        }

        public IActionResult Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                _db.CommentsGenericRepository.Delete(id);
                _db.Save();
            }
            return Redirect("/Admin/Comments/Index");
        }
    }
}