using Ecommerce2.Models;
using Ecommerce2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Ecommerce2.Controllers
{
	public class HomeController : Controller
	{
        private readonly Ecommerce2Context db;

        public HomeController(Ecommerce2Context context)
		{
            db = context;
        }

		public IActionResult Index(int? hang)
		{
			var sanPhams = db.SanPhams.AsQueryable();
			if (hang.HasValue)
			{
				sanPhams = sanPhams.Where(p => p.MaHang == hang.Value);
			}
			var spMoi = sanPhams.OrderByDescending(p => p.MaSp)
				.Take(5)
				.Select(p => new SanPhamVM
				{
					MaSanPham = p.MaSp,
					TenSanPham = p.TenSp,
					GiaBan = p.GiaBan ?? 0,
					Hinh = p.Hinh ?? "",
					MoTaDonVi = p.MoTaDonVi ?? "",
					TenHang = p.MaHangNavigation.TenHang
				});
			return View(spMoi);
		}

        [Route("/404")]
        public IActionResult PageNotFound()
        {
            return View();
        }

        public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
