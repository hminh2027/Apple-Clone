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

namespace WebBill.Controllers
{
    public class ExportsController : Controller
    {
        private AppleStoreEntities db = new AppleStoreEntities();

        // GET: Exports
        public ActionResult Index(int? page)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "User");
            }
            var pageNumber = page ?? 1;
            var pageSize = 5;
            return View(db.Exports.Include(e => e.ExportDetail).Include(e => e.Store).Include(e => e.User).ToList().ToPagedList(pageNumber, pageSize));
        }

        // GET: Exports/Details/5
        public ActionResult Details(int id)
        {
            Export export = db.Exports.Find(id);
            if (export == null)
            {
                return HttpNotFound();
            }
            return View(export);
        }

        // GET: Exports/Create
        public ActionResult Create()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "User");
            }
            ViewBag.ExportID = new SelectList(db.ExportDetails, "ExportID", "ProductID");
            ViewBag.StoreID = new SelectList(db.Stores, "StoreID", "Name");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username");
            return View();
        }

        // POST: Exports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExportID,ExportedDate,CreatedAt,UpdatedAt,IsDeleted,UserID,StoreID")] Export export)
        {
            if (ModelState.IsValid)
            {
                db.Exports.Add(export);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ExportID = new SelectList(db.ExportDetails, "ExportID", "ProductID", export.ExportID);
            ViewBag.StoreID = new SelectList(db.Stores, "StoreID", "Name", export.StoreID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username", export.UserID);
            return View(export);
        }

        // GET: Exports/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "User");
            }
            Export export = db.Exports.Find(id);
            if (export == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExportID = new SelectList(db.ExportDetails, "ExportID", "ProductID", export.ExportID);
            ViewBag.StoreID = new SelectList(db.Stores, "StoreID", "Name", export.StoreID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username", export.UserID);
            return View(export);
        }

        // POST: Exports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExportID,ExportedDate,CreatedAt,UpdatedAt,IsDeleted,UserID,StoreID")] Export export)
        {
            if (ModelState.IsValid)
            {
                db.Entry(export).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ExportID = new SelectList(db.ExportDetails, "ExportID", "ProductID", export.ExportID);
            ViewBag.StoreID = new SelectList(db.Stores, "StoreID", "Name", export.StoreID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username", export.UserID);
            return View(export);
        }

        // GET: Exports/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Export export = db.Exports.Find(id);
            if (export == null)
            {
                return HttpNotFound();
            }
            return View(export);
        }

        // POST: Exports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Export export = db.Exports.Find(id);
            db.Exports.Remove(export);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
