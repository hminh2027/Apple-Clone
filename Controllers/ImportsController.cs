using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Apple_Clone_Website.Models;

namespace Apple_Clone_Website.Controllers
{
    public class ImportsController : Controller
    {
        private AppleStoreEntities db = new AppleStoreEntities();

        // GET: Imports
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "User");
            }
            var imports = db.Imports.Include(i => i.ImportDetail).Include(i => i.Store).Include(i => i.User);
            return View(imports.ToList());
        }

        // GET: Imports/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "User");
            }
            Import import = db.Imports.Find(id);
            if (import == null)
            {
                return HttpNotFound();
            }
            return View(import);
        }

        // GET: Imports/Create
        public ActionResult Create()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "User");
            }
            ViewBag.ImportID = new SelectList(db.ImportDetails, "ImportID", "ImportID");
            ViewBag.StoreID = new SelectList(db.Stores, "StoreID", "Name");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username");
            return View();
        }

        // POST: Imports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ImportID,ImportedDate,CreatedAt,UpdatedAt,IsDeleted,UserID,StoreID")] Import import)
        {
            if (ModelState.IsValid)
            {
                db.Imports.Add(import);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ImportID = new SelectList(db.ImportDetails, "ImportID", "ImportID", import.ImportID);
            ViewBag.StoreID = new SelectList(db.Stores, "StoreID", "Name", import.StoreID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username", import.UserID);
            return View(import);
        }

        // GET: Imports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "User");
            }

            Import import = db.Imports.Find(id);
            if (import == null)
            {
                return HttpNotFound();
            }
            ViewBag.ImportID = new SelectList(db.ImportDetails, "ImportID", "ImportID", import.ImportID);
            ViewBag.StoreID = new SelectList(db.Stores, "StoreID", "Name", import.StoreID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username", import.UserID);
            return View(import);
        }

        // POST: Imports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ImportID,ImportedDate,CreatedAt,UpdatedAt,IsDeleted,UserID,StoreID")] Import import)
        {
            if (ModelState.IsValid)
            {
                db.Entry(import).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ImportID = new SelectList(db.ImportDetails, "ImportID", "ImportID", import.ImportID);
            ViewBag.StoreID = new SelectList(db.Stores, "StoreID", "Name", import.StoreID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username", import.UserID);
            return View(import);
        }

        // GET: Imports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Import import = db.Imports.Find(id);
            if (import == null)
            {
                return HttpNotFound();
            }
            return View(import);
        }

        // POST: Imports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Import import = db.Imports.Find(id);
            db.Imports.Remove(import);
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
