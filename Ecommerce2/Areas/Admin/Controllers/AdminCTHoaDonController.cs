using Ecommerce2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Ecommerce2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminCTHoaDonController : Controller
    {
        private readonly Ecommerce2Context _context;

        public AdminCTHoaDonController(Ecommerce2Context context)
        {
            _context = context;
        }

        public IActionResult Index(int? page)
        {
            var pageNum = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 10;
            var lsCtHoadon = _context.ChiTietHds.AsNoTracking()
                .OrderByDescending(x => x.MaCt);
            PagedList<ChiTietHd> models = new PagedList<ChiTietHd>(
                lsCtHoadon, pageNum, pageSize);
            ViewBag.CurrentPage = pageNum;
            return View(models);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cthoaDon = await _context.ChiTietHds
                .FirstOrDefaultAsync(m => m.MaCt == id);
            if (cthoaDon == null)
            {
                return NotFound();
            }

            return View(cthoaDon);
        }

        // GET: Admin/AdminCTHoaDon/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cthoaDon = await _context.ChiTietHds
                .FirstOrDefaultAsync(m => m.MaCt == id);
            if (cthoaDon == null)
            {
                return NotFound();
            }

            return View(cthoaDon);
        }

        // POST: Admin/AdminCTHoaDon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cthoaDon = await _context.ChiTietHds.FindAsync(id);
            _context.ChiTietHds.Remove(cthoaDon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
