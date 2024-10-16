﻿using System;
using System.Collections.Generic;

namespace Ecommerce2.Models;

public partial class NhaCungCap
{
    public string MaNcc { get; set; } = null!;

    public string TenCongTy { get; set; } = null!;

    public string Logo { get; set; } = null!;

    public string? NguoiLienLac { get; set; }

    public string Email { get; set; } = null!;

    public string? DienThoai { get; set; }

    public string? DiaChi { get; set; }

    public string? MoTa { get; set; }

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
