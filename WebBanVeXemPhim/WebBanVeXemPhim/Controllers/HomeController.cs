
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebBanVeXemPhim.Models;

namespace WebBanVeXemPhim.Controllers
{
    public class HomeController : Controller
    {
        private readonly QuanLyBanVeXemPhimContext _context;

        public HomeController(QuanLyBanVeXemPhimContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> IndexAsync(string searchString)
        {
            // Lấy danh sách phim
            var query = _context.Phims.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(p => p.TenPhim.Contains(searchString));
            }

            var danhSachPhim = await query.OrderBy(p => p.MaPhim).ToListAsync();

            ViewBag.SearchString = searchString;

            var danhSachTrailer = await _context.Trailers.ToListAsync(); // Lấy danh sách trailer

            ViewBag.DanhSachTrailer = danhSachTrailer; // Gửi dữ liệu trailer qua ViewBag
            ViewBag.SearchString = searchString;

            return View(danhSachPhim);
        }
        public async Task<IActionResult> Home(string searchString)
        {
            // Lấy danh sách phim
            var query = _context.Phims.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(p => p.TenPhim.Contains(searchString));
            }

            var danhSachPhim = await query.OrderBy(p => p.MaPhim).ToListAsync();

            ViewBag.SearchString = searchString;

            var danhSachTrailer = await _context.Trailers.ToListAsync(); // Lấy danh sách trailer

            ViewBag.DanhSachTrailer = danhSachTrailer; // Gửi dữ liệu trailer qua ViewBag
            ViewBag.SearchString = searchString;

            return View(danhSachPhim);
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
