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
    public class BondsController : ControllerBase
    {
        private readonly ItemsDbContext _context;

        public BondsController(ItemsDbContext context)
        {
            _context = context;
        }

        // GET: api/Bonds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bond>>> GetBonds()
        {
            return await _context.Bonds.ToListAsync();
        }

        // GET: api/Bonds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bond>> GetBond(int id)
        {
            var bond = await _context.Bonds.FindAsync(id);

            if (bond == null)
            {
                return NotFound();
            }

            return bond;
        }

        // PUT: api/Bonds/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBond(int id, Bond bond)
        {
            if (id != bond.Id)
            {
                return BadRequest();
            }

            _context.Entry(bond).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BondExists(id))
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

        // POST: api/Bonds
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Bond>> PostBond(Bond bond)
        {
            _context.Bonds.Add(bond);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBond", new { id = bond.Id }, bond);
        }

        // DELETE: api/Bonds/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Bond>> DeleteBond(int id)
        {
            var bond = await _context.Bonds.FindAsync(id);
            if (bond == null)
            {
                return NotFound();
            }

            _context.Bonds.Remove(bond);
            await _context.SaveChangesAsync();

            return bond;
        }

        private bool BondExists(int id)
        {
            return _context.Bonds.Any(e => e.Id == id);
        }
    }
}
