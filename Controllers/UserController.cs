using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Apple_Clone_Website.Models;

namespace Apple_Clone.Controllers
{
    public class UserController : Controller
    {
        AppleStoreEntities db = new AppleStoreEntities();

        public ActionResult DangXuat()
        {
            Session.Remove("user");
            return RedirectToAction("DangNhap");
        }

        // GET: User
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap");
            }
            else
                return RedirectToAction("Details");
        }
        public User getUserByUsername(string userName)
        {
            return db.Users.SingleOrDefault(n => n.Username == userName);
        }
        // GET: User/Details/5
        public ActionResult Details(string userName = "")
        {
            User user = Session["user"] as User;
            if (user == null)
            {
                return RedirectToAction("DangNhap", "User");
            }
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            // Hash password
            string hashedPw = MD5(collection["txtPassword"]);     

            try
            {
                // TODO: Add insert logic here
                User user = new User();
                
                user.Username = collection["txtUsername"];
                user.Password = hashedPw;
                user.Email = collection["txtEmail"];
                user.Phone = collection["txtTel"];
                user.Birthday = Convert.ToDateTime(collection["txtBirthday"]);
                user.Country = collection["txtCountry"];
                user.Gender = Convert.ToBoolean(collection["Gender"]);

                user.CreatedAt = DateTime.Now;
                user.UpdatedAt = DateTime.Now;
                db.Users.Add(user);
                db.SaveChanges();

                

                return RedirectToAction("DangNhap");
            }
            catch (Exception e)
            {
                return View();
            }
        }
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                // Hash password
                string hashedPw = MD5(collection["txtPassword"]);

                string userName = collection["txtUsername"];
                string passWord = hashedPw;
                User user = db.Users.SingleOrDefault(n => n.Username == userName && n.Password == passWord);
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

        // GET: User/Edit/5
        public ActionResult Edit(string userName)
        {
            User user = Session["user"] as User;
            if (user == null)
            {
                return RedirectToAction("DangNhap", "User");
            }
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                int count = collection.Count;
                string userName = collection["Username"];
                User user = db.Users.SingleOrDefault(n => n.Username == userName);
                if (user != null)
                {
                    user.Password = collection["Password"];
                    user.Email = collection["Email"];
                    user.Phone = collection["Phone"];
                    user.Birthday = Convert.ToDateTime(collection["Birthday"]);
                    user.Country = collection["Country"];
                    user.Gender = Convert.ToBoolean(collection["Gender"]);
                    Session["user"] = user;
                    db.SaveChanges();
                    return RedirectToAction("Details", "User");
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Product
        public ActionResult Product()
        {
            return View();
        }

        private string MD5 (string str)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            byte[] bHash = md5.ComputeHash(Encoding.UTF8.GetBytes(str));

            StringBuilder sbHash = new StringBuilder();

            foreach (byte b in bHash)
            {

                sbHash.Append(String.Format("{0:x2}", b));

            }

            return sbHash.ToString();
        }
    }
}
