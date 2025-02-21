using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebBanVeXemPhim.Models;

namespace WebBanVeXemPhim.Areas.admins.Controllers
{
    public class VesController : BaseController
    {
        private readonly QuanLyBanVeXemPhimContext _context;

        public VesController(QuanLyBanVeXemPhimContext context)
        {
            _context = context;
        }

        // GET: admins/Ves
        public async Task<IActionResult> Index()
        {
            var quanLyBanVeXemPhimContext = _context.Ves
                .Include(v => v.MaGheNavigation)
                .Include(v => v.MaKhachHangNavigation)
                .Include(v => v.MaLichChieuNavigation)
                .ThenInclude(lc => lc.MaPhimNavigation);

            return View(await quanLyBanVeXemPhimContext.ToListAsync());
        }
        private bool VeExists(int id)
        {
            return _context.Ves.Any(e => e.MaVe == id);
        }
    }
}
