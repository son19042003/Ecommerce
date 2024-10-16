using Ecommerce2.Models;
using Ecommerce2.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce2.ViewComponents
{
    public class MenuHangViewComponent : ViewComponent
    {
        private readonly Ecommerce2Context db;

        public MenuHangViewComponent(Ecommerce2Context context) => db = context;

        public IViewComponentResult Invoke()
        {
            var data = db.Hangs.Select(hang => new MenuHangVM {
                MaHang = hang.MaHang,
                TenHang = hang.TenHang,
                SoLuong = hang.SanPhams.Count()
            }).OrderBy(p => p.TenHang);

            return View(data);
        }
    }
}
