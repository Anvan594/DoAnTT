using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanVeXemPhim.Models;

namespace WebBanVeXemPhim.Areas.admins.Controllers
{
    public class PhimsController : BaseController
    {
        private readonly QuanLyBanVeXemPhimContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PhimsController(IWebHostEnvironment webHostEnvironment, QuanLyBanVeXemPhimContext context)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString, int page = 1)
        {
            int pageSize = 3;
            var query = _context.Phims.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(p => p.TenPhim.Contains(searchString));
            }

            var danhSachPhim = await query.OrderBy(p => p.MaPhim)
                                          .Skip((page - 1) * pageSize)
                                          .Take(pageSize)
                                          .ToListAsync();

            int totalRecords = await query.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.SearchString = searchString;

            return View(danhSachPhim);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phim = await _context.Phims.FirstOrDefaultAsync(m => m.MaPhim == id);
            if (phim == null)
            {
                return NotFound();
            }

            return PartialView("Details", phim);
        }

        public IActionResult Create()
        {
            return PartialView("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Phim phim, IFormFile PosterFile)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Dữ liệu không hợp lệ!" });
            }

            if (PosterFile != null && PosterFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + PosterFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    PosterFile.CopyTo(fileStream);
                }

                phim.Poster = uniqueFileName;
            }

            _context.Phims.Add(phim);
            _context.SaveChanges();

            return Json(new { success = true, message = "Thêm phim thành công!" });
        }
    

    public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phim = await _context.Phims.FirstOrDefaultAsync(m => m.MaPhim == id);
            if (phim == null)
            {
                return NotFound();
            }

            return PartialView("Edit", phim);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Phim phim)
        {
            if (id != phim.MaPhim)
            {
                return NotFound();
            }

            var existingPhim = await _context.Phims.FindAsync(id);
            if (existingPhim == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(existingPhim).CurrentValues.SetValues(phim);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Phims.Any(e => e.MaPhim == phim.MaPhim))
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

            return PartialView("Edit", phim);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var phim = await _context.Phims.FindAsync(id);
            if (phim == null)
            {
                return Json(new { success = false, message = "Không tìm thấy phim!" });
            }

            _context.Phims.Remove(phim);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Xóa phim thành công!" });
        }

        private bool PhimExists(int id)
        {
            return _context.Phims.Any(e => e.MaPhim == id);
        }
    }
}