using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanVeXemPhim_API.Models;

namespace WebBanVeXemPhim_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiKhoanAdminsController : ControllerBase
    {
        private readonly QuanLyBanVeXemPhimContext _context;

        public TaiKhoanAdminsController(QuanLyBanVeXemPhimContext context)
        {
            _context = context;
        }

        // GET: api/TaiKhoanAdmins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaiKhoanAdmin>>> GetTaiKhoanAdmins()
        {
            return await _context.TaiKhoanAdmins.ToListAsync();
        }

        // GET: api/TaiKhoanAdmins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaiKhoanAdmin>> GetTaiKhoanAdmin(int id)
        {
            var taiKhoanAdmin = await _context.TaiKhoanAdmins.FindAsync(id);

            if (taiKhoanAdmin == null)
            {
                return NotFound();
            }

            return taiKhoanAdmin;
        }

        // PUT: api/TaiKhoanAdmins/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaiKhoanAdmin(int id, TaiKhoanAdmin taiKhoanAdmin)
        {
            if (id != taiKhoanAdmin.MaAdmin)
            {
                return BadRequest();
            }

            _context.Entry(taiKhoanAdmin).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaiKhoanAdminExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TaiKhoanAdmins
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaiKhoanAdmin>> PostTaiKhoanAdmin(TaiKhoanAdmin taiKhoanAdmin)
        {
            _context.TaiKhoanAdmins.Add(taiKhoanAdmin);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTaiKhoanAdmin", new { id = taiKhoanAdmin.MaAdmin }, taiKhoanAdmin);
        }

        // DELETE: api/TaiKhoanAdmins/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaiKhoanAdmin(int id)
        {
            var taiKhoanAdmin = await _context.TaiKhoanAdmins.FindAsync(id);
            if (taiKhoanAdmin == null)
            {
                return NotFound();
            }

            _context.TaiKhoanAdmins.Remove(taiKhoanAdmin);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaiKhoanAdminExists(int id)
        {
            return _context.TaiKhoanAdmins.Any(e => e.MaAdmin == id);
        }
    }
}
