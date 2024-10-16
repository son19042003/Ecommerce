using Ecommerce2.Helpers;
using Ecommerce2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Ecommerce2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminSanPhamController : Controller
    {
        private readonly Ecommerce2Context _context;

        public AdminSanPhamController(Ecommerce2Context context)
        {
            _context = context;
        }

        // GET: Admin/AdminProducts
        public IActionResult Index(int page = 1, int CatID = 0)
        {
            var pageNum = page;
            var pageSize = 10;

            List<SanPham> lsProducts = new List<SanPham>();

            if (CatID != 0)
            {
                lsProducts = _context.SanPhams.AsNoTracking()
                .Include(x => x.MaHangNavigation)
                .Where(x => x.MaHang == CatID)
                .OrderByDescending(x => x.MaSp).ToList();
            }
            else
            {
                lsProducts = _context.SanPhams.AsNoTracking()
                .Include(x => x.MaHangNavigation)
                .OrderByDescending(x => x.MaSp).ToList();
            }
            PagedList<SanPham> models = new PagedList<SanPham>(
                    lsProducts.AsQueryable(), pageNum, pageSize);
            ViewBag.CurrentCatID = CatID;
            ViewBag.CurrentPage = pageNum;
            ViewData["DanhMucHang"] = new SelectList(_context.Hangs, "MaHang", "TenHang", CatID);

            return View(models);
        }

        public IActionResult Filtter(int CatID = 0)
        {
            var url = $"/Admin/AdminSanPham?MaHang={CatID}";
            if (CatID == 0)
            {
                url = $"/Admin/AdminSanPham";
            }
            return Json(new { status = "success", redirectUrl = url });
        }

        // GET: Admin/AdminProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.SanPhams
                .Include(p => p.MaHangNavigation)
                .FirstOrDefaultAsync(m => m.MaSp == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/AdminProducts/Create
        public IActionResult Create()
        {
            ViewData["DanhMucHang"] = new SelectList(_context.Hangs, "MaHang", "TenHang");
            return View();
        }

        // POST: Admin/AdminProducts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSp,TenSp,TenAlias,MaHang,MoTaDonVi,GiaBan,BaoHanh,Hinh,NgaySx,GiamGia,SoLuong,MoTa,MaNcc")] SanPham product, IFormFile fThumb)
        {
            if (ModelState.IsValid)
            {
                product.TenSp = Utilities.ToTitleCase(product.TenSp);
                if (fThumb != null)
                {
                    string extension = Path.GetExtension(fThumb.FileName);
                    string image = Utilities.SEOUrl(product.TenSp) + extension;
                    product.Hinh = await Utilities.UploadFile(fThumb, @"SanPham", image.ToLower());
                }
                if (string.IsNullOrEmpty(product.Hinh)) product.Hinh = "default.jpg";

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DanhMucHang"] = new SelectList(_context.Hangs, "MaHang", "TenHang", product.MaHang);
            return View(product);
        }

        // GET: Admin/AdminProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.SanPhams.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["DanhMucHang"] = new SelectList(_context.Hangs, "MaHang", "TenHang", product.MaHang);
            return View(product);
        }

        // POST: Admin/AdminProducts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaSp,TenSp,TenAlias,MaHang,MoTaDonVi,GiaBan,BaoHanh,Hinh,NgaySx,GiamGia,SoLuong,MoTa,MaNcc")] SanPham product, IFormFile fThumb)
        {
            if (id != product.MaSp)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    product.TenSp = Utilities.ToTitleCase(product.TenSp);
                    if (fThumb != null)
                    {
                        string extension = Path.GetExtension(fThumb.FileName);
                        string image = Utilities.SEOUrl(product.TenSp) + extension;
                        product.Hinh = await Utilities.UploadFile(fThumb, @"SanPham", image.ToLower());
                    }
                    if (string.IsNullOrEmpty(product.Hinh)) product.Hinh = "default.jpg";
                    product.TenAlias = Utilities.SEOUrl(product.TenSp);

                    _context.Add(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.MaSp))
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
            ViewData["DanhMucHang"] = new SelectList(_context.Hangs, "MaHang", "TenHang", product.MaHang);

            return View(product);
        }

        private bool ProductExists(int id)
        {
            return _context.SanPhams.Any(e => e.MaSp == id);
        }
    }
}
