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
    public class CustomersController : Controller
    {
        private AppleStoreEntities db = new AppleStoreEntities();

        //Kiểm tra user đã đăng nhập chưa sau đó ms cho thực hiện qly khách hàng
        public bool isAdmin()
        {
            if (Session["user"] == null)
            {

                return false;
            }
            return true;
        }
        public ActionResult NotLoggedIn()
        {
            return RedirectToAction("DangNhap", "User");
        }
        // GET: Customers
        public ActionResult Index(int? page)
        {
            if (!isAdmin())
            {
                return RedirectToAction("NotLoggedIn");
            }
            var pageNumber = page ?? 1;
            var pageSize = 5;
            return View(db.Customers.ToList().ToPagedList(pageNumber, pageSize));
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            if (!isAdmin())
            {
                return RedirectToAction("NotLoggedIn");
            }
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,Gender,Email,Phone,Country,CreatedAt,UpdatedAt,IsDeleted")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(string id)
        {
            if (!isAdmin())
            {
                return RedirectToAction("NotLoggedIn");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,Gender,Email,Phone,Country,CreatedAt,UpdatedAt,IsDeleted")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DangNhap()
        {
            User user = Session["user"] as User;
            if (user != null)
            {
                return RedirectToAction("Details");
            }
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                string customerName = collection["txtUsername"];
                string passWord = collection["txtPassword"];
                Customer user = db.Customers.SingleOrDefault(n => n.Name == customerName);
                if (user != null)
                {
                    ViewBag.ThongBao = "Login successful";

                    Session.Add("user", user);
                    return RedirectToAction("Details", "User");
                }

            }
            ViewBag.ThongBao = "Login failed";
            return View();
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
