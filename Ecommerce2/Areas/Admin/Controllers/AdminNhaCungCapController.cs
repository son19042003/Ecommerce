using Ecommerce2.Helpers;
using Ecommerce2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Ecommerce2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminNhaCungCapController : Controller
    {
        private readonly Ecommerce2Context _context;

        public AdminNhaCungCapController(Ecommerce2Context context)
        {
            _context = context;
        }

        // GET: Admin/AdminNcc
        public IActionResult Index(int? page)
        {
            var pageNum = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 10;
            var lsNhaCC = _context.NhaCungCaps.AsNoTracking()
                .OrderByDescending(x => x.MaNcc);
            PagedList<NhaCungCap> models = new PagedList<NhaCungCap>(
                lsNhaCC, pageNum, pageSize);
            ViewBag.CurrentPage = pageNum;
            return View(models);
        }

        // GET: Admin/AdminNhaCungCap/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhaCc = await _context.NhaCungCaps
                .FirstOrDefaultAsync(m => m.MaNcc == id);
            if (nhaCc == null)
            {
                return NotFound();
            }

            return View(nhaCc);
        }

        // GET: Admin/AdminNhaCungCap/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminNhaCungCap/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaNcc,TenCongTy,Logo,NguoiLienLac,Email,DienThoai,DiaChi,MoTa")] NhaCungCap nhaCc, IFormFile fThumb)
        {
            if (ModelState.IsValid)
            {
                if (fThumb != null)
                {
                    string extension = Path.GetExtension(fThumb.FileName);
                    string image = Utilities.SEOUrl(nhaCc.TenCongTy) + extension;
                    nhaCc.Logo = await Utilities.UploadFile(fThumb, @"NhaCungCap", image.ToLower());
                }
                if (string.IsNullOrEmpty(nhaCc.Logo)) nhaCc.Logo = "default.jpg";
                _context.Add(nhaCc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nhaCc);
        }

        // GET: Admin/AdminNhaCungCap/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhaCc = await _context.NhaCungCaps.FindAsync(id);
            if (nhaCc == null)
            {
                return NotFound();
            }
            return View(nhaCc);
        }

        // POST: Admin/AdminNhaCungCap/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaNcc,TenCongTy,Logo,NguoiLienLac,Email,DienThoai,DiaChi,MoTa")] NhaCungCap nhaCc, IFormFile fThumb)
        {
            if (id != nhaCc.MaNcc)
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
                        string image = Utilities.SEOUrl(nhaCc.TenCongTy) + extension;
                        nhaCc.Logo = await Utilities.UploadFile(fThumb, @"NhaCungCap", image.ToLower());
                    }
                    if (string.IsNullOrEmpty(nhaCc.Logo)) nhaCc.Logo = "default.jpg";

                    _context.Update(nhaCc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(nhaCc.MaNcc))
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
            return View(nhaCc);
        }

        // GET: Admin/AdminNhaCungCap/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhaCc = await _context.NhaCungCaps
                .FirstOrDefaultAsync(m => m.MaNcc == id);
            if (nhaCc == null)
            {
                return NotFound();
            }

            return View(nhaCc);
        }

        // POST: Admin/AdminNhaCungCap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var nhaCc = await _context.NhaCungCaps.FindAsync(id);
            _context.NhaCungCaps.Remove(nhaCc);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(string id)
        {
            return _context.NhaCungCaps.Any(e => e.MaNcc == id);
        }
    }
}
