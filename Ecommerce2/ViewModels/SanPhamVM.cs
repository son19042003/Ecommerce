namespace Ecommerce2.ViewModels
{
    public class SanPhamVM
    {
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string TenHang { get; set; }
        public string Hinh { get; set; }
        public double GiaBan { get; set; }
        public string MoTaDonVi { get; set; }
    }

    public class ChiTietSanPhamVM
    {
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string TenHang { get; set; }
        public string Hinh { get; set; }
        public double GiaBan { get; set; }
        public string MoTaDonVi { get; set; }
        public int BaoHanh { get; set; }
        public DateTime? NgaySanXuat { get; set; }
        public string Mota { get; set; }
        public int SoLuongTon { get; set; }
        public string NhaCungCap { get; set; }
    }
}
