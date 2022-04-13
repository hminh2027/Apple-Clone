using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Apple_Clone_Website.Models;
using PagedList;

namespace Apple_Clone_Website.Controllers
{
    public class ProductsController : Controller
    {
        private AppleStoreEntities db = new AppleStoreEntities();

        // GET: Products
        public ActionResult Index(int? page)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "User");
            }
  
            var pageNumber = page ?? 1;
            var pageSize = 5;
            return View(db.Products.ToList().ToPagedList(pageNumber, pageSize));
        }

        public PartialViewResult Color(int id)
        {
            return PartialView(db.ProductColors.Where(model => model.ProductColorID == id).ToList());
        }

        public PartialViewResult RelatedProducts(int id)
        {
            return PartialView(db.Products.Where(model => model.CategoryID == id).ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int id)
        {
            if (Session["user"] == null)
            {

                return RedirectToAction("DangNhap", "User");
            }
            Product product = db.Products.SingleOrDefault(md => md.ProductID == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            if (Session["user"] == null)
            {

                return RedirectToAction("DangNhap", "User");
            }
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,Name,UnitPrice,Description,IsNew,CreatedAt,UpdatedAt,IsDeleted,CategoryID,SupplierID,Image,ProductColorID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["user"] == null)
            {

                return RedirectToAction("DangNhap", "User");
            }
            Product product = db.Products.SingleOrDefault(md => md.ProductID == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }


        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,Name,UnitPrice,Description,IsNew,CreatedAt,UpdatedAt,IsDeleted,CategoryID,SupplierID,Image,ProductColorID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
