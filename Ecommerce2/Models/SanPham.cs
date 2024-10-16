using System;
using System.Collections.Generic;

namespace Ecommerce2.Models;

public partial class SanPham
{
    public int MaSp { get; set; }

    public string TenSp { get; set; } = null!;

    public string? TenAlias { get; set; }

    public int MaHang { get; set; }

    public string? MoTaDonVi { get; set; }

    public double? GiaBan { get; set; }

    public int? BaoHanh { get; set; }

    public string? Hinh { get; set; }

    public DateTime NgaySx { get; set; }

    public double GiamGia { get; set; }

    public int SoLuong { get; set; }

    public string? MoTa { get; set; }

    public string MaNcc { get; set; } = null!;

    public virtual ICollection<ChiTietHd> ChiTietHds { get; set; } = new List<ChiTietHd>();

    public virtual Hang MaHangNavigation { get; set; } = null!;

    public virtual NhaCungCap MaNccNavigation { get; set; } = null!;
}
