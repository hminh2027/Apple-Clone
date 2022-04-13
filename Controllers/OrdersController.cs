using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Apple_Clone_Website.Models;

namespace WebBill.Controllers
{
    public class OrdersController : Controller
    {
        private AppleStoreEntities db = new AppleStoreEntities();

        // GET: Orders
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "User");
            }

            var orders = db.Orders.Include(o => o.Customer).Include(o => o.OrderDetail).Include(o => o.PaymentType);
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "User");
            }

            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "User");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Email");
            ViewBag.OrderID = new SelectList(db.OrderDetails, "OrderID", "ProductID");
            ViewBag.PaymentTypeID = new SelectList(db.PaymentTypes, "PaymentTypeID", "Name");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,OrderedDate,ShipAddress,OrderStatus,PaidAt,CreatedAt,UpdatedAt,IsDeleted,CustomerID,PaymentTypeID")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Email", order.CustomerID);
            ViewBag.OrderID = new SelectList(db.OrderDetails, "OrderID", "ProductID", order.OrderID);
            ViewBag.PaymentTypeID = new SelectList(db.PaymentTypes, "PaymentTypeID", "Name", order.PaymentTypeID);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "User");
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Email", order.CustomerID);
            ViewBag.OrderID = new SelectList(db.OrderDetails, "OrderID", "ProductID", order.OrderID);
            ViewBag.PaymentTypeID = new SelectList(db.PaymentTypes, "PaymentTypeID", "Name", order.PaymentTypeID);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,OrderedDate,ShipAddress,OrderStatus,PaidAt,CreatedAt,UpdatedAt,IsDeleted,CustomerID,PaymentTypeID")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Email", order.CustomerID);
            ViewBag.OrderID = new SelectList(db.OrderDetails, "OrderID", "ProductID", order.OrderID);
            ViewBag.PaymentTypeID = new SelectList(db.PaymentTypes, "PaymentTypeID", "Name", order.PaymentTypeID);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
