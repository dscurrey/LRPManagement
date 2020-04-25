using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LRP.Items.Data;
using LRP.Items.Data.Bonds;
using LRP.Items.Models;

namespace LRP.Items.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BondsController : ControllerBase
    {
        private readonly IBondRepository _repository;

        public BondsController(IBondRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Bonds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bond>>> GetBonds()
        {
            return await _repository.GetAll();
        }

        // GET: api/Bonds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bond>> GetBond(int id)
        {
            var bond = await _repository.Get(id);

            if (bond == null)
            {
                return NotFound();
            }

            return bond;
        }

        // POST: api/Bonds
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Bond>> PostBond(Bond bond)
        {
            _repository.Insert(bond);
            await _repository.Save();

            return CreatedAtAction("GetBond", new { id = bond.Id }, bond);
        }

        // DELETE: api/Bonds/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Bond>> DeleteBond(int id)
        {
            var bond = await _repository.Get(id);
            if (bond == null)
            {
                return NotFound();
            }

            await _repository.Delete(id);
            await _repository.Save();

            return bond;
        }
    }
}
