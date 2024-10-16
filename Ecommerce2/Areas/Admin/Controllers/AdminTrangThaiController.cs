using Ecommerce2.Helpers;
using Ecommerce2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminTrangThaiController : Controller
    {
        private readonly Ecommerce2Context _context;

        public AdminTrangThaiController(Ecommerce2Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.TrangThais.ToListAsync());
        }

        // GET: Admin/AdminTrangThai/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var status = await _context.TrangThais
                .FirstOrDefaultAsync(m => m.MaTrangThai == id);
            if (status == null)
            {
                return NotFound();
            }

            return View(status);
        }

        // GET: Admin/AdminTrangThai/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminTrangThai/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaTrangThai,TenTrangThai,MoTa")] TrangThai status)
        {
            if (ModelState.IsValid)
            {
                _context.Add(status);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(status);
        }

        // GET: Admin/AdminTrangThai/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var status = await _context.TrangThais.FindAsync(id);
            if (status == null)
            {
                return NotFound();
            }
            return View(status);
        }

        // POST: Admin/AdminTrangThai/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaTrangThai,TenTrangThai,MoTa")] TrangThai status)
        {
            if (id != status.MaTrangThai)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(status);
                    await _context.SaveChangesAsync();
                    //_notifyService.Success("Cập nhập thành công");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoleExists(status.MaTrangThai))
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
            return View(status);
        }

        // GET: Admin/AdminTrangThai/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var status = await _context.TrangThais
                .FirstOrDefaultAsync(m => m.MaTrangThai == id);
            if (status == null)
            {
                return NotFound();
            }

            return View(status);
        }

        // POST: Admin/AdminTrangThai/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var status = await _context.TrangThais.FindAsync(id);
            _context.TrangThais.Remove(status);
            await _context.SaveChangesAsync();
            //_notifyService.Success("Xóa quyền truy cập thành công");
            return RedirectToAction(nameof(Index));
        }

        private bool RoleExists(int id)
        {
            return _context.PhanQuyens.Any(e => e.MaPq == id);
        }
    }
}
