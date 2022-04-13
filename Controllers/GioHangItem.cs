namespace Apple_Clone_Website.Controllers
{
    public class GioHangItem
    {
        internal int dThanhTien;

        public GioHangItem(int productID)
        {
            ProductID = productID;
        }

        public int ProductID { get; internal set; }
        public int SoLuong { get; internal set; }
        public decimal UnitPrice { get; internal set; }
    }
}