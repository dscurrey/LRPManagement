using DTO;
using LRP.Players.Data.Players;
using LRP.Players.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRP.Players.Controllers
{
    /// <summary>
    /// Bonds controller used for interactions involving Players.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerRepository _repository;
        private readonly ILogger<PlayersController> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="playerRepository"></param>
        /// <param name="logger"></param>
        public PlayersController(IPlayerRepository playerRepository, ILogger<PlayersController> logger)
        {
            _repository = playerRepository;
            _logger = logger;
        }

        // GET: api/Players
        /// <summary>
        /// Gets all players
        /// </summary>
        /// <returns>A list of all players</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerDTO>>> GetPlayer()
        {
            var players = await _repository.GetAll();
            return Ok
            (
                players.Select
                (
                    p => new PlayerDTO
                    {
                        Id = p.Id,
                        DateJoined = p.DateJoined,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        AccountRef = p.AccountRef
                    }
                ).ToList()
            );
        }

        // GET: api/Players/5
        /// <summary>
        /// Gets a specified player
        /// </summary>
        /// <param name="id">Id for the chosen player</param>
        /// <returns>Chosen player</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerDTO>> GetPlayer(int id)
        {
            var player = await _repository.GetPlayer(id);

            if (player == null) return NotFound();

            var playDto = new PlayerDTO
            {
                DateJoined = player.DateJoined,
                FirstName = player.FirstName,
                Id = player.Id,
                LastName = player.LastName,
                AccountRef = player.AccountRef
            };

            return Ok(playDto);
        }

        // PUT: api/Players/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Updates a specified player
        /// </summary>
        /// <param name="id">Id for the specified player</param>
        /// <param name="player">Updated player</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayer(int id, PlayerDTO player)
        {
            if (id != player.Id) return BadRequest();

            var exPlayer = await _repository.GetPlayer(id);

            if (!await PlayerExists(id)) return NotFound();

            var updPlayer = new Player
            {
                DateJoined = player.DateJoined,
                FirstName = player.FirstName,
                LastName = player.LastName,
                AccountRef = exPlayer.AccountRef
            };

            _repository.UpdatePlayer(updPlayer);

            try
            {
                _logger.LogInformation("Saving Updated Player...");
                await _repository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await PlayerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogError("Error occured when saving updated player.");
                    return BadRequest();
                }
            }

            return NoContent();
        }

        // POST: api/Players
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Creates a new player
        /// </summary>
        /// <param name="player">New player</param>
        /// <returns>Created <see cref="Player"/> object</returns>
        [HttpPost]
        public async Task<ActionResult<Player>> PostPlayer([FromBody] PlayerDTO player)
        {
            if (string.IsNullOrEmpty(player.FirstName) || string.IsNullOrEmpty(player.LastName))
                return BadRequest("Requires First and Last Names");

            var dbPlayer = await _repository.GetPlayer(player.Id);
            if (dbPlayer != null)
            {
                player.AccountRef = dbPlayer.AccountRef;
                _repository.UpdatePlayer(player);
                await _repository.Save();
                return Ok(player);
            }

            var newPlayer = new Player
            {
                DateJoined = DateTime.Now.Date,
                FirstName = player.FirstName,
                Id = player.Id,
                IsActive = true,
                LastName = player.LastName,
                AccountRef = player.AccountRef
            };

            _repository.InsertPlayer(newPlayer);
            await _repository.Save();

            return CreatedAtAction("GetPlayer", new {id = player.Id}, player);
        }

        // DELETE: api/Players/5
        /// <summary>
        /// Deletes a specified player
        /// </summary>
        /// <param name="id">Id of the player to be deleted</param>
        /// <returns>Deleted player</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Player>> DeletePlayer(int id)
        {
            if (!await PlayerExists(id)) return NotFound();

            var player = await _repository.GetPlayer(id);

            await _repository.DeletePlayer(id);
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