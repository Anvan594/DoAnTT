using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebBanVeXemPhim.Models;

namespace WebBanVeXemPhim.Areas.admins.Controllers
{
    public class LichChieusController : BaseController
    {
        private readonly QuanLyBanVeXemPhimContext _context;

        public LichChieusController(QuanLyBanVeXemPhimContext context)
        {
            _context = context;
        }

        // Hiển thị danh sách lịch chiếu (có tìm kiếm, phân trang)
        public async Task<IActionResult> Index(string searchString, int page = 1)
        {
            int pageSize = 5; // Số bản ghi mỗi trang
            var query = _context.LichChieus.Include(l => l.MaPhimNavigation).Include(l => l.MaPhongNavigation).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(l => l.MaPhimNavigation.TenPhim.Contains(searchString));
            }

            var danhSachLichChieu = await query.OrderBy(l => l.NgayChieu)
                                               .Skip((page - 1) * pageSize)
                                               .Take(pageSize)
                                               .ToListAsync();

            int totalRecords = await query.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.SearchString = searchString;

            return View(danhSachLichChieu);
        }

        // Hiển thị chi tiết lịch chiếu
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var lichChieu = await _context.LichChieus.Include(l => l.MaPhimNavigation).Include(l => l.MaPhongNavigation)
                                                     .FirstOrDefaultAsync(m => m.MaLichChieu == id);
            if (lichChieu == null) return NotFound();

            return PartialView("Details", lichChieu);
        }

        // GET: Thêm lịch chiếu
        public IActionResult Create()
        {
            ViewBag.MaPhim = new SelectList(_context.Phims, "MaPhim", "TenPhim");
            ViewBag.MaPhong = new SelectList(_context.Phongs, "MaPhong", "TenPhong");
            return PartialView("Create");
        }

        // POST: Thêm lịch chiếu
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LichChieu lichChieu)
        {
            if (!ModelState.IsValid) return Json(new { success = false, message = "Dữ liệu không hợp lệ!" });

            _context.LichChieus.Add(lichChieu);
            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Thêm lịch chiếu thành công!" });
        }

        // GET: Chỉnh sửa lịch chiếu
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var lichChieu = await _context.LichChieus.FindAsync(id);
            if (lichChieu == null) return NotFound();
            List<string> gioChieuList = (lichChieu.MaPhong % 2 == 0)
        ? new List<string> { "06:00", "08:00", "10:00", "12:00", "14:00", "16:00", "18:00", "20:00", "22:00" }
        : new List<string> { "07:30", "09:30", "11:30", "13:30", "15:30", "17:30", "19:30", "21:30", "23:30" };


            ViewBag.MaPhim = new SelectList(_context.Phims, "MaPhim", "TenPhim", lichChieu.MaPhim);
            ViewBag.MaPhong = new SelectList(_context.Phongs, "MaPhong", "TenPhong", lichChieu.MaPhong);
            return PartialView("Edit", lichChieu);
        }

        // POST: Chỉnh sửa lịch chiếu
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LichChieu lichChieu)
        {
            var existingLichChieu = await _context.LichChieus.FindAsync(id);
            if (existingLichChieu == null) return NotFound();

            existingLichChieu.NgayChieu = lichChieu.NgayChieu;
            existingLichChieu.GioChieu = lichChieu.GioChieu;
            existingLichChieu.GiaVe = lichChieu.GiaVe;
            existingLichChieu.TrangThai = lichChieu.TrangThai;
            existingLichChieu.MaPhong = lichChieu.MaPhong;
            existingLichChieu.MaPhim = lichChieu.MaPhim;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.LichChieus.Any(e => e.MaLichChieu == id)) return NotFound();
                else throw;
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult CheckDuplicate(int maPhong, DateOnly ngayChieu, TimeOnly gioChieu, int? maLichChieu)
        {
            var isDuplicate = _context.LichChieus.Any(lc =>
                lc.MaPhong == maPhong &&
                lc.NgayChieu == ngayChieu &&
                lc.GioChieu == gioChieu &&
                (maLichChieu == null || lc.MaLichChieu != maLichChieu) // Nếu là cập nhật thì bỏ qua chính nó
            );

            return Json(new { isDuplicate });
        }




        // Xóa lịch chiếu
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lichChieu = await _context.LichChieus.FindAsync(id);
            if (lichChieu == null) return Json(new { success = false, message = "Không tìm thấy lịch chiếu!" });

            _context.LichChieus.Remove(lichChieu);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Xóa lịch chiếu thành công!" });
        }
    }
}
