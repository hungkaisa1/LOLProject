using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LOLProject.Data;
using LOLProject.Models;

namespace LOLProject.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChampionsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChampionsApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Champion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Champion>>> GetChampions()
        {
            return await _context.Champions.ToListAsync();
        }

        // GET: api/Champion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Champion>> GetChampion(int id)
        {
            var champion = await _context.Champions.FindAsync(id);

            if (champion == null)
            {
                return NotFound();
            }

            return champion;
        }

        // PUT: api/Champion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChampion(int id, Champion champion)
        {
            if (id != champion.ChampionId)
            {
                return BadRequest();
            }

            _context.Entry(champion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChampionExists(id))
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

        // POST: api/Champion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Champion>> PostChampion(Champion champion)
        {
            _context.Champions.Add(champion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChampion", new { id = champion.ChampionId }, champion);
        }

        // DELETE: api/Champion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChampion(int id)
        {
            var champion = await _context.Champions.FindAsync(id);
            if (champion == null)
            {
                return NotFound();
            }

            _context.Champions.Remove(champion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChampionExists(int id)
        {
            return _context.Champions.Any(e => e.ChampionId == id);
        }
    }
}
