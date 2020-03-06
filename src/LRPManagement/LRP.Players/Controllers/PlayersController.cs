using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LRP.Players.Data;
using LRP.Players.Data.Players;
using LRP.Players.Models;
using Microsoft.Extensions.Logging;

namespace LRP.Players.Controllers
{
    // TODO - Authorisation

    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerRepository _repository;
        private readonly ILogger<PlayersController> _logger;

        public PlayersController(IPlayerRepository playerRepository, ILogger<PlayersController> logger)
        {
            _repository = playerRepository;
            _logger = logger;
        }

        // GET: api/Players
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayer()
        {
            return await _repository.GetAll();
        }

        // GET: api/Players/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetPlayer(int id)
        {
            var player = await _repository.GetPlayer(id);

            if (player == null)
            {
                return NotFound();
            }

            return player;
        }

        // PUT: api/Players/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayer(int id, Player player)
        {
            if (id != player.Id)
            {
                return BadRequest();
            }

            _repository.UpdatePlayer(player);

            try
            {
                _logger.LogInformation("Saving Updated Player...");
                await _repository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogError("Error occured when saving updated player.");
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Players
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Player>> PostPlayer(Player player)
        {
            // TODO - At player creation, create user in auth server

            await _repository.InsertPlayer(player);
            await _repository.Save();

            return CreatedAtAction("GetPlayer", new { id = player.Id }, player);
        }

        // DELETE: api/Players/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Player>> DeletePlayer(int id)
        {
            if (!await PlayerExists(id))
            {
                return NotFound();
            }

            var player = await _repository.GetPlayer(id);

            _repository.DeletePlayer(id);
            await _repository.Save();

            return player;
        }

        private async Task<bool> PlayerExists(int id)
        {
            var all = await _repository.GetAll();
            return all.Any(p => p.Id == id);
        }
    }
}
