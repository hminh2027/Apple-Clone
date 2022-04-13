using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apple_Clone_Website.Models;

namespace Apple_Clone_Website.Controllers
{
    public class HomeController : Controller
    {
        AppleStoreEntities db = new AppleStoreEntities();

        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Categories()
        {
            var list = db.Categories.ToList();
            return PartialView(list);
        }

        public ActionResult ProductsByCategory(int id)
        {
            return View(db.Products.Where(model => model.CategoryID == id).ToList());
        }

        public PartialViewResult LastestProducts()
        {
            var list = db.Products.Where(d => d.IsNew == true).ToList();
            return PartialView(list);
        }
    }
}