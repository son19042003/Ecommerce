using Ecommerce2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce2.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class AdminPhanQuyenController : Controller
	{
		private readonly Ecommerce2Context _context;

		public AdminPhanQuyenController(Ecommerce2Context context)
		{
			_context = context;
		}

		// GET: Admin/AdminRoles
		public async Task<IActionResult> Index()
		{
			return View(await _context.PhanQuyens.ToListAsync());
		}

        // GET: Admin/AdminRoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _context.PhanQuyens
                .FirstOrDefaultAsync(m => m.MaPq == id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // GET: Admin/AdminRoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminRoles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaPq,TenPq,Them,Sua,Xoa,Xem")] PhanQuyen role)
        {
            if (ModelState.IsValid)
            {
                _context.Add(role);
                await _context.SaveChangesAsync();
                //_notifyService.Success("Tạo mới thành công");
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        // GET: Admin/AdminRoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _context.PhanQuyens.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        // POST: Admin/AdminRoles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaPq,TenPq,Them,Sua,Xoa,Xem")] PhanQuyen role)
        {
            if (id != role.MaPq)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(role);
                    await _context.SaveChangesAsync();
                    //_notifyService.Success("Cập nhập thành công");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoleExists(role.MaPq))
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
            return View(role);
        }

        // GET: Admin/AdminRoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _context.PhanQuyens
                .FirstOrDefaultAsync(m => m.MaPq == id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // POST: Admin/AdminRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var role = await _context.PhanQuyens.FindAsync(id);
            _context.PhanQuyens.Remove(role);
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
