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
    public class TrailersController : ControllerBase
    {
        private readonly QuanLyBanVeXemPhimContext _context;

        public TrailersController(QuanLyBanVeXemPhimContext context)
        {
            _context = context;
        }

        // GET: api/Trailers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trailer>>> GetTrailers()
        {
            return await _context.Trailers.ToListAsync();
        }

        // GET: api/Trailers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Trailer>> GetTrailer(int id)
        {
            var trailer = await _context.Trailers.FindAsync(id);

            if (trailer == null)
            {
                return NotFound();
            }

            return trailer;
        }

        // PUT: api/Trailers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrailer(int id, Trailer trailer)
        {
            if (id != trailer.MaTrailer)
            {
                return BadRequest();
            }

            _context.Entry(trailer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrailerExists(id))
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

        // POST: api/Trailers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Trailer>> PostTrailer(Trailer trailer)
        {
            _context.Trailers.Add(trailer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrailer", new { id = trailer.MaTrailer }, trailer);
        }

        // DELETE: api/Trailers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrailer(int id)
        {
            var trailer = await _context.Trailers.FindAsync(id);
            if (trailer == null)
            {
                return NotFound();
            }

            _context.Trailers.Remove(trailer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrailerExists(int id)
        {
            return _context.Trailers.Any(e => e.MaTrailer == id);
        }
    }
}
