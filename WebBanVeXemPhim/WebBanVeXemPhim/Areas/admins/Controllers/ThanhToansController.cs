using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebBanVeXemPhim.Models;

namespace WebBanVeXemPhim.Areas.admins.Controllers
{
    [Area("admins")]
    public class ThanhToansController : Controller
    {
        private readonly QuanLyBanVeXemPhimContext _context;

        public ThanhToansController(QuanLyBanVeXemPhimContext context)
        {
            _context = context;
        }

        // GET: admins/ThanhToans
        public async Task<IActionResult> Index()
        {
            var quanLyBanVeXemPhimContext = _context.ThanhToans.Include(t => t.MaVeNavigation);
            return View(await quanLyBanVeXemPhimContext.ToListAsync());
        }

        // GET: admins/ThanhToans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thanhToan = await _context.ThanhToans
                .Include(t => t.MaVeNavigation)
                .FirstOrDefaultAsync(m => m.MaThanhToan == id);
            if (thanhToan == null)
            {
                return NotFound();
            }

            return View(thanhToan);
        }

        // GET: admins/ThanhToans/Create
        public IActionResult Create()
        {
            ViewData["MaVe"] = new SelectList(_context.Ves, "MaVe", "MaVe");
            return View();
        }

        // POST: admins/ThanhToans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaThanhToan,MaVe,PhuongThuc,TrangThai,NgayThanhToan")] ThanhToan thanhToan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(thanhToan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaVe"] = new SelectList(_context.Ves, "MaVe", "MaVe", thanhToan.MaVe);
            return View(thanhToan);
        }

        // GET: admins/ThanhToans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thanhToan = await _context.ThanhToans.FindAsync(id);
            if (thanhToan == null)
            {
                return NotFound();
            }
            ViewData["MaVe"] = new SelectList(_context.Ves, "MaVe", "MaVe", thanhToan.MaVe);
            return View(thanhToan);
        }

        // POST: admins/ThanhToans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaThanhToan,MaVe,PhuongThuc,TrangThai,NgayThanhToan")] ThanhToan thanhToan)
        {
            if (id != thanhToan.MaThanhToan)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(thanhToan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThanhToanExists(thanhToan.MaThanhToan))
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
            ViewData["MaVe"] = new SelectList(_context.Ves, "MaVe", "MaVe", thanhToan.MaVe);
            return View(thanhToan);
        }

        // GET: admins/ThanhToans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thanhToan = await _context.ThanhToans
                .Include(t => t.MaVeNavigation)
                .FirstOrDefaultAsync(m => m.MaThanhToan == id);
            if (thanhToan == null)
            {
                return NotFound();
            }

            return View(thanhToan);
        }

        // POST: admins/ThanhToans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var thanhToan = await _context.ThanhToans.FindAsync(id);
            if (thanhToan != null)
            {
                _context.ThanhToans.Remove(thanhToan);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThanhToanExists(int id)
        {
            return _context.ThanhToans.Any(e => e.MaThanhToan == id);
        }
    }
}
