using Ecommerce2.Helpers;
using Ecommerce2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Ecommerce2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminKhachHangController : Controller
    {
        private readonly Ecommerce2Context _context;

        public AdminKhachHangController(Ecommerce2Context context)
        {
            _context = context;
        }

        // GET: Admin/AdminCustomers
        public IActionResult Index(int? page)
        {
            var pageNum = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 15;
            var lsCustomers = _context.KhachHangs.AsNoTracking()
                .OrderByDescending(x => x.MaKh);
            PagedList<KhachHang> models = new PagedList<KhachHang>(
                lsCustomers, pageNum, pageSize);
            ViewBag.CurrentPage = pageNum;
            return View(models);
        }

        // GET: Admin/AdminCustomers/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.KhachHangs
                .FirstOrDefaultAsync(m => m.MaKh == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Admin/AdminCustomers/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.KhachHangs.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Admin/AdminCustomers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaKh,MatKhau,HoTen,GioiTinh,NgaySinh,DiaChi,DienThoai,Email,Hinh,HieuLuc,RandomKey")] KhachHang customer, IFormFile fThumb)
        {
            if (id != customer.MaKh)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (fThumb != null)
                    {
                        string extension = Path.GetExtension(fThumb.FileName);
                        string image = Utilities.SEOUrl(customer.HoTen) + extension;
                        customer.Hinh = await Utilities.UploadFile(fThumb, @"KhachHang", image.ToLower());
                    }
                    if (string.IsNullOrEmpty(customer.Hinh)) customer.Hinh = "default.jpg";

                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(customer.MaKh))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Admin/AdminCustomers/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.KhachHangs
                .FirstOrDefaultAsync(m => m.MaKh == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Admin/AdminCustomers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var customer = await _context.KhachHangs.FindAsync(id);
            _context.KhachHangs.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(string id)
        {
            return _context.KhachHangs.Any(e => e.MaKh == id);
        }
    }
}
