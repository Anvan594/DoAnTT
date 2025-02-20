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
    public class ThongBaosController : ControllerBase
    {
        private readonly QuanLyBanVeXemPhimContext _context;

        public ThongBaosController(QuanLyBanVeXemPhimContext context)
        {
            _context = context;
        }

        // GET: api/ThongBaos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ThongBao>>> GetThongBaos()
        {
            return await _context.ThongBaos.ToListAsync();
        }

        // GET: api/ThongBaos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ThongBao>> GetThongBao(int id)
        {
            var thongBao = await _context.ThongBaos.FindAsync(id);

            if (thongBao == null)
            {
                return NotFound();
            }

            return thongBao;
        }

        // PUT: api/ThongBaos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutThongBao(int id, ThongBao thongBao)
        {
            if (id != thongBao.MaThongBao)
            {
                return BadRequest();
            }

            _context.Entry(thongBao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ThongBaoExists(id))
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

        // POST: api/ThongBaos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ThongBao>> PostThongBao(ThongBao thongBao)
        {
            _context.ThongBaos.Add(thongBao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetThongBao", new { id = thongBao.MaThongBao }, thongBao);
        }

        // DELETE: api/ThongBaos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteThongBao(int id)
        {
            var thongBao = await _context.ThongBaos.FindAsync(id);
            if (thongBao == null)
            {
                return NotFound();
            }

            _context.ThongBaos.Remove(thongBao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ThongBaoExists(int id)
        {
            return _context.ThongBaos.Any(e => e.MaThongBao == id);
        }
    }
}
