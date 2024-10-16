using Ecommerce2.Helpers;
using Ecommerce2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Ecommerce2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminHangController : Controller
    {
        private readonly Ecommerce2Context _context;

        public AdminHangController(Ecommerce2Context context)
        {
            _context = context;
        }

        // GET: Admin/AdminCategories
        public IActionResult Index(int? page)
        {
            var pageNum = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 10;
            var lsCategories = _context.Hangs.AsNoTracking()
                .OrderByDescending(x => x.MaHang);
            PagedList<Hang> models = new PagedList<Hang>(
                lsCategories, pageNum, pageSize);
            ViewBag.CurrentPage = pageNum;
            return View(models);
        }

        // GET: Admin/AdminCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Hangs
                .FirstOrDefaultAsync(m => m.MaHang == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Admin/AdminCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminCategories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaHang,TenHang,TenHangAlias,MoTa,Hinh")] Hang category, IFormFile fThumb)
        {
            if (ModelState.IsValid)
            {
                if (fThumb != null)
                {
                    string extension = Path.GetExtension(fThumb.FileName);
                    string image = Utilities.SEOUrl(category.TenHang) + extension;
                    category.Hinh = await Utilities.UploadFile(fThumb, @"Hang", image.ToLower());
                }
                if (string.IsNullOrEmpty(category.Hinh)) category.Hinh = "default.jpg";
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Admin/AdminCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Hangs.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Admin/AdminCategories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaHang,TenHang,TenHangAlias,MoTa,Hinh")] Hang category, IFormFile fThumb)
        {
            if (id != category.MaHang)
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
                        string image = Utilities.SEOUrl(category.TenHang) + extension;
                        category.Hinh = await Utilities.UploadFile(fThumb, @"Hang", image.ToLower());
                    }
                    if (string.IsNullOrEmpty(category.Hinh)) category.Hinh = "default.jpg";

                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.MaHang))
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
            return View(category);
        }

        // GET: Admin/AdminCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Hangs
                .FirstOrDefaultAsync(m => m.MaHang == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Admin/AdminCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Hangs.FindAsync(id);
            _context.Hangs.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Hangs.Any(e => e.MaHang == id);
        }
    }
}
