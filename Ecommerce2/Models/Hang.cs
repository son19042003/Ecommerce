using System;
using System.Collections.Generic;

namespace Ecommerce2.Models;

public partial class Hang
{
    public int MaHang { get; set; }

    public string TenHang { get; set; } = null!;

    public string? TenHangAlias { get; set; }

    public string? MoTa { get; set; }

    public string? Hinh { get; set; }

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
