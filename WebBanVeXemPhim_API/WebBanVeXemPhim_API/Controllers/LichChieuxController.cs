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
    public class LichChieuxController : ControllerBase
    {
        private readonly QuanLyBanVeXemPhimContext _context;

        public LichChieuxController(QuanLyBanVeXemPhimContext context)
        {
            _context = context;
        }

        // GET: api/LichChieux
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LichChieu>>> GetLichChieus()
        {
            return await _context.LichChieus.ToListAsync();
        }

        // GET: api/LichChieux/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LichChieu>> GetLichChieu(int id)
        {
            var lichChieu = await _context.LichChieus.FindAsync(id);

            if (lichChieu == null)
            {
                return NotFound();
            }

            return lichChieu;
        }

        // PUT: api/LichChieux/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLichChieu(int id, LichChieu lichChieu)
        {
            if (id != lichChieu.MaLichChieu)
            {
                return BadRequest();
            }

            _context.Entry(lichChieu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LichChieuExists(id))
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

        // POST: api/LichChieux
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LichChieu>> PostLichChieu(LichChieu lichChieu)
        {
            _context.LichChieus.Add(lichChieu);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLichChieu", new { id = lichChieu.MaLichChieu }, lichChieu);
        }

        // DELETE: api/LichChieux/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLichChieu(int id)
        {
            var lichChieu = await _context.LichChieus.FindAsync(id);
            if (lichChieu == null)
            {
                return NotFound();
            }

            _context.LichChieus.Remove(lichChieu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LichChieuExists(int id)
        {
            return _context.LichChieus.Any(e => e.MaLichChieu == id);
        }
    }
}
