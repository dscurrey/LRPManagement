using LRP.Items.Data.Bonds;
using LRP.Items.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LRP.Items.Controllers
{
    /// <summary>
    /// Bonds controller used for interactions involving Character/Item relationship
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BondsController : ControllerBase
    {
        private readonly IBondRepository _repository;

        /// <summary>
        /// Initialises a new instance of the <see cref="BondsController"/> class
        /// </summary>
        /// <param name="repository">A repository containing bonds</param>
        public BondsController(IBondRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Bonds
        /// <summary>
        /// Gets all bonds
        /// </summary>
        /// <returns>All bonds in repository</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bond>>> GetBonds()
        {
            return await _repository.GetAll();
        }

        // GET: api/Bonds/5
        /// <summary>
        /// Gets a specified bond
        /// </summary>
        /// <param name="id">ID for the chosen bond</param>
        /// <returns>Specified bond</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Bond>> GetBond(int id)
        {
            var bond = await _repository.Get(id);

            if (bond == null) return NotFound();

            return bond;
        }

        // POST: api/Bonds
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Creates a bond in the database
        /// </summary>
        /// <param name="bond">Bond to be created</param>
        /// <returns>Created bond</returns>
        [HttpPost]
        public async Task<ActionResult<Bond>> PostBond(Bond bond)
        {
            _repository.Insert(bond);
            await _repository.Save();

            return CreatedAtAction("GetBond", new {id = bond.Id}, bond);
        }

        // DELETE: api/Bonds/5
        /// <summary>
        /// Deletes a specified bond
        /// </summary>
        /// <param name="id">Id for chosen bond</param>
        /// <returns>The deleted bond</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Bond>> DeleteBond(int id)
        {
            var bond = await _repository.Get(id);
            if (bond == null) return NotFound();

            await _repository.Delete(id);
            await _repository.Save();

            return bond;
        }
    }
}