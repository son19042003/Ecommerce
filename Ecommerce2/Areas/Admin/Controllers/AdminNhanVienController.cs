using Ecommerce2.Helpers;
using Ecommerce2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminNhanVienController : Controller
    {
        private readonly Ecommerce2Context _context;

        public AdminNhanVienController(Ecommerce2Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.NhanViens.ToListAsync());
        }

        // GET: Admin/AdminNhanVien/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanVien = await _context.NhanViens
                .FirstOrDefaultAsync(m => m.MaNv == id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            return View(nhanVien);
        }

        // GET: Admin/AdminNhanVien/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminNhanVien/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaNv,HoTen,Email,MatKhau,MaPq")] NhanVien nvien)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nvien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nvien);
        }

        // GET: Admin/AdminNhanVien/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nvien = await _context.NhanViens.FindAsync(id);
            if (nvien == null)
            {
                return NotFound();
            }
            return View(nvien);
        }

        // POST: Admin/AdminNhanVien/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaNv,HoTen,Email,MatKhau,MaPq")] NhanVien nvien)
        {
            if (id != nvien.MaNv)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nvien);
                    await _context.SaveChangesAsync();
                    //_notifyService.Success("Cập nhập thành công");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoleExists(nvien.MaNv))
                    {
                        //_notifyService.Success("Có lỗi xảy ra");
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(nvien);
        }

        // GET: Admin/AdminNhanVien/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nvien = await _context.NhanViens
                .FirstOrDefaultAsync(m => m.MaNv == id);
            if (nvien == null)
            {
                return NotFound();
            }

            return View(nvien);
        }

        // POST: Admin/AdminNhanVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var nvien = await _context.NhanViens.FindAsync(id);
            _context.NhanViens.Remove(nvien);
            await _context.SaveChangesAsync();
            //_notifyService.Success("Xóa quyền truy cập thành công");
            return RedirectToAction(nameof(Index));
        }

        private bool RoleExists(string id)
        {
            return _context.NhanViens.Any(e => e.MaNv == id);
        }
    }
}
