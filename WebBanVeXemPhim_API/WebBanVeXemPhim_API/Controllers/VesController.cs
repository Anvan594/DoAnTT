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
    public class VesController : ControllerBase
    {
        private readonly QuanLyBanVeXemPhimContext _context;

        public VesController(QuanLyBanVeXemPhimContext context)
        {
            _context = context;
        }

        // GET: api/Ves
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ve>>> GetVes()
        {
            return await _context.Ves.ToListAsync();
        }

        // GET: api/Ves/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ve>> GetVe(int id)
        {
            var ve = await _context.Ves.FindAsync(id);

            if (ve == null)
            {
                return NotFound();
            }

            return ve;
        }

        // PUT: api/Ves/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVe(int id, Ve ve)
        {
            if (id != ve.MaVe)
            {
                return BadRequest();
            }

            _context.Entry(ve).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VeExists(id))
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

        // POST: api/Ves
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ve>> PostVe(Ve ve)
        {
            _context.Ves.Add(ve);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVe", new { id = ve.MaVe }, ve);
        }

        // DELETE: api/Ves/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVe(int id)
        {
            var ve = await _context.Ves.FindAsync(id);
            if (ve == null)
            {
                return NotFound();
            }

            _context.Ves.Remove(ve);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VeExists(int id)
        {
            return _context.Ves.Any(e => e.MaVe == id);
        }
    }
}
