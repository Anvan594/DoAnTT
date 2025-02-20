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
    public class PhimsController : ControllerBase
    {
        private readonly QuanLyBanVeXemPhimContext _context;

        public PhimsController(QuanLyBanVeXemPhimContext context)
        {
            _context = context;
        }

        // GET: api/Phims
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Phim>>> GetPhims()
        {
            return await _context.Phims.ToListAsync();
        }

        // GET: api/Phims/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Phim>> GetPhim(int id)
        {
            var phim = await _context.Phims.FindAsync(id);

            if (phim == null)
            {
                return NotFound();
            }

            return phim;
        }

        // PUT: api/Phims/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhim(int id, Phim phim)
        {
            if (id != phim.MaPhim)
            {
                return BadRequest();
            }

            _context.Entry(phim).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhimExists(id))
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

        // POST: api/Phims
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Phim>> PostPhim(Phim phim)
        {
            _context.Phims.Add(phim);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPhim", new { id = phim.MaPhim }, phim);
        }

        // DELETE: api/Phims/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhim(int id)
        {
            var phim = await _context.Phims.FindAsync(id);
            if (phim == null)
            {
                return NotFound();
            }

            _context.Phims.Remove(phim);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PhimExists(int id)
        {
            return _context.Phims.Any(e => e.MaPhim == id);
        }
    }
}
