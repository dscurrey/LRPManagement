using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LRP.Items.Data;
using LRP.Items.Models;

namespace LRP.Items.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CraftablesController : ControllerBase
    {
        private readonly ItemsDbContext _context;

        public CraftablesController(ItemsDbContext context)
        {
            _context = context;
        }

        // GET: api/Craftables
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Craftable>>> GetCraftables()
        {
            return await _context.Craftables.ToListAsync();
        }

        // GET: api/Craftables/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Craftable>> GetCraftable(int id)
        {
            var craftable = await _context.Craftables.FindAsync(id);

            if (craftable == null)
            {
                return NotFound();
            }

            return craftable;
        }

        // PUT: api/Craftables/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCraftable(int id, Craftable craftable)
        {
            if (id != craftable.Id)
            {
                return BadRequest();
            }

            _context.Entry(craftable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CraftableExists(id))
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

        // POST: api/Craftables
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Craftable>> PostCraftable(Craftable craftable)
        {
            _context.Craftables.Add(craftable);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCraftable", new { id = craftable.Id }, craftable);
        }

        // DELETE: api/Craftables/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Craftable>> DeleteCraftable(int id)
        {
            var craftable = await _context.Craftables.FindAsync(id);
            if (craftable == null)
            {
                return NotFound();
            }

            _context.Craftables.Remove(craftable);
            await _context.SaveChangesAsync();

            return craftable;
        }

        private bool CraftableExists(int id)
        {
            return _context.Craftables.Any(e => e.Id == id);
        }
    }
}
