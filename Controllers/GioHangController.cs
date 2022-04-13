using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apple_Clone_Website.Models;

namespace Apple_Clone_Website.Controllers
{
    public class GioHangController : Controller
    {
        AppleStoreEntities db = new AppleStoreEntities();
        // GET: GioHang
        public List<GioHangItem> LayGioHang()
        {
            List<GioHangItem> lstGioHang = Session["GioHang"] as List<GioHangItem>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<GioHangItem>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        public ActionResult ThemGioHang(int ProductID, string strUrl)
        {
            Product product = db.Products.SingleOrDefault(n => n.ProductID == ProductID);
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            // Lấy giỏ hàng từ session
            List<GioHangItem> lstGioHang = LayGioHang();
            //Ktra sp đã có trong giỏ chưa
            GioHangItem gioHangItem = lstGioHang.Find(n => n.ProductID == ProductID);
            if (gioHangItem == null)
            {
                lstGioHang.Add(new GioHangItem(ProductID));
            }
            else
            {
                gioHangItem.SoLuong++;
            }
            return Redirect(strUrl);
        }
        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                RedirectToAction("Index", "SanPham");
            }
            List<GioHangItem> lstGioHang = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
        }
        private int TongSoLuong()
        {
            int iTongSL = 0;
            List<GioHangItem> lstGioHang = Session["GioHang"] as List<GioHangItem>;
            if (lstGioHang != null)
            {
                iTongSL = lstGioHang.Sum(n => n.SoLuong);
            }
            return iTongSL;
        }
        private double TongTien()
        {
            double dTongTien = 0;
            List<GioHangItem> lstGioHang = Session["GioHang"] as List<GioHangItem>;
            if (lstGioHang != null)
            {
                dTongTien = lstGioHang.Sum(n => n.dThanhTien);
            }
            return dTongTien;
        }
        public PartialViewResult GioHangPartial()
        {
            if (TongSoLuong() != 0)
            {
                ViewBag.TongSoLuong = TongSoLuong();
                return PartialView();
            }
            return PartialView();
        }
        public ActionResult SuaGioHang()
        {
            if (Session["GioHang"] == null)
            {
                RedirectToAction("Index", "SanPham");
            }
            List<GioHangItem> lstGioHang = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
        }
        public ActionResult CapNhatGioHang(int ProductID, FormCollection f)
        {
            //Kiem tra ma san pham
            Product product = db.Products.SingleOrDefault(n => n.ProductID == ProductID);
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHangItem> lstGioHang = LayGioHang();
            GioHangItem gioHangItem = lstGioHang.SingleOrDefault(n => n.ProductID == ProductID);
            if (gioHangItem != null)
            {
                gioHangItem.SoLuong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("SuaGioHang", "GioHang");
        }
        public ActionResult XoaGioHang(int ProductID, FormCollection f)
        {
            //Kiem tra ma san pham
            Product product = db.Products.SingleOrDefault(n => n.ProductID == ProductID);
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHangItem> lstGioHang = LayGioHang();
            GioHangItem gioHangItem = lstGioHang.SingleOrDefault(n => n.ProductID == ProductID);
            if (gioHangItem != null)
            {
                lstGioHang.Remove(gioHangItem);
            }
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "SanPham");
            }
            return RedirectToAction("SuaGioHang", "GioHang");
        }
        public ActionResult XoaTatCaGioHang()
        {
            List<GioHangItem> lstGioHang = LayGioHang();
            lstGioHang.Clear();
            Session["GioHang"] = lstGioHang;
            return RedirectToAction("Index", "Home");
        }
        #region Đặt hàng
        [HttpGet]
        public ActionResult DatHang()
        {
            List<GioHangItem> lstGioHang = LayGioHang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(lstGioHang);
        }
        [HttpPost]
        public ActionResult DatHang(FormCollection collection)
        {
            //Them Don Hang
            Order order = new Order();
            Customer customer = new Customer();
            List<GioHangItem> lstGioHang = LayGioHang();

            //Lay thong tin khach tu form
            customer.Email = collection["txtEmail"];
            //customer.Name = collection["txtName"];
            customer.Phone = collection["txtTel"];
            customer.Country = collection["txtAddress"];

            order.CustomerID = customer.CustomerID;
            order.ShipAddress = customer.Country;
            order.CreatedAt = DateTime.Now;
            order.UpdatedAt = DateTime.Now;

            db.Orders.Add(order);
            db.SaveChanges();
            //Lay gio hang tu session
            List<GioHangItem> lstGiohang = LayGioHang();
            foreach (var item in lstGioHang)
            {
                OrderDetail orderDetail = new OrderDetail();
                orderDetail.ProductID = item.ProductID;
                orderDetail.Quantity = item.SoLuong;
                orderDetail.UnitPrice = (decimal)item.UnitPrice;
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
            }


            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}