using System;
using System.Collections.Generic;

namespace Ecommerce2.Models;

public partial class PhanQuyen
{
    public int MaPq { get; set; }

    public string? TenPq { get; set; }

    public bool Them { get; set; }

    public bool Sua { get; set; }

    public bool Xoa { get; set; }

    public bool Xem { get; set; }

    public virtual ICollection<NhanVien> NhanViens { get; set; } = new List<NhanVien>();
}
