using Ecommerce2.Models;
using Ecommerce2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using SlugGenerator;

namespace Ecommerce2.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly Ecommerce2Context db;
        public SanPhamController(Ecommerce2Context context)
        {
            db = context;
        }

        public IActionResult Index(int? loai, int? page)
        {
            var pageSize = 9;
            var pageNumber = page ?? 1;
            var sanPhams = db.SanPhams.AsQueryable();
            if (loai.HasValue)
            {
                sanPhams = sanPhams.Where(p => p.MaHang == loai.Value);
            }
            var result = sanPhams.Select(p => new SanPhamVM
            {
                MaSanPham = p.MaSp,
                TenSanPham = p.TenSp,
                GiaBan = p.GiaBan ?? 0,
                Hinh = p.Hinh ?? "",
                MoTaDonVi = p.MoTaDonVi ?? "",
                TenHang = p.MaHangNavigation.TenHang
            });
            return View(result.ToPagedList(pageNumber, pageSize));
        }

        public IActionResult Search(string? query)
        {
            var sanPhams = db.SanPhams.AsQueryable();

            if (query != null)
            {
                sanPhams = sanPhams.Where(p => p.TenSp.Contains(query));
            }

            var result = sanPhams.Select(p => new SanPhamVM
            {
                MaSanPham = p.MaSp,
                TenSanPham = p.TenSp,
                GiaBan = p.GiaBan ?? 0,
                Hinh = p.Hinh ?? "",
                MoTaDonVi = p.MoTaDonVi ?? "",
                TenHang = p.MaHangNavigation.TenHang
            });
            return View(result);
        }

        [Route("Detail/{slug}-{id:int}")]
        public IActionResult Detail(int id)
        {
            var data = db.SanPhams
                .Include(p => p.MaHangNavigation)
                .SingleOrDefault(p => p.MaSp == id);
            if (data == null)
            {
                TempData["Message"] = $"Không thấy sản phẩm này";
                return Redirect("/404");
            }

            var result = new ChiTietSanPhamVM
            {
                MaSanPham = data.MaSp,
                TenSanPham = data.TenSp,
                GiaBan = data.GiaBan ?? 0,
                Hinh = data.Hinh ?? string.Empty,
                MoTaDonVi = data.MoTaDonVi ?? string.Empty,
                TenHang = data.MaHangNavigation.TenHang,
                SoLuongTon = data.SoLuong,//tính sau
                Mota = data.MoTa ?? string.Empty,
                NgaySanXuat = data.NgaySx,
                BaoHanh = data.BaoHanh ?? 0
            };

            return View(result);
        }
    }
}
