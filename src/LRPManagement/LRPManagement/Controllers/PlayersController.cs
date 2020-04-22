using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DTO;
using LRPManagement.Data;
using LRPManagement.Data.Players;
using LRPManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Polly.CircuitBreaker;

namespace LRPManagement.Controllers
{
    [Authorize]
    public class PlayersController : Controller
    {
        private readonly IPlayerService _playerService;
        private readonly IPlayerRepository _playerRepository;

        public PlayersController(IPlayerService playerService, IPlayerRepository playerRepository)
        {
            _playerService = playerService;
            _playerRepository = playerRepository;
        }

        // GET: Players
        [Authorize(Policy = "StaffOnly")]
        public async Task<IActionResult> Index()
        {
            TempData["PlayInoperativeMsg"] = "";

            await UpdateDb();

            try
            {
                var players = await _playerRepository.GetAll();

                var playerList = players.Select
                (
                    p => new PlayerDTO
                    {
                        AccountRef = p.AccountRef,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        Id = p.Id
                    }
                );

                return View(playerList);
            }
            catch (BrokenCircuitException)
            {
                HandleBrokenCircuit();
            }

            return View();
        }

        // GET: Players/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            TempData["PlayInoperativeMsg"] = "";
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var player = await _playerRepository.GetPlayer(id.Value);
                if (player == null)
                {
                    return NotFound();
                }

                if (player.AccountRef == User.Identity.Name || User.IsInRole("Admin") || User.IsInRole("Referee"))
                {
                    return View(player);
                }
                else
                {
                    return View(nameof(Index));
                }
            }
            catch
            {
                HandleBrokenCircuit();
            }

            return View();
        }

        [Authorize]
        // GET: Players/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Players/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,DateJoined")] PlayerDTO playerDTO)
        {
            TempData["PlayInoperativeMsg"] = "";
            try
            {
                // Tie to account
                playerDTO.AccountRef = User.Identity.Name;
                playerDTO.DateJoined = DateTime.Now;

                var resp = await _playerService.CreatePlayer(playerDTO);
                if (resp == null)
                {
                    // Unsuccessful/Error
                    return View(playerDTO);
                }
            }
            catch (BrokenCircuitException)
            {
                HandleBrokenCircuit();
            }

            await UpdateDb();

            return RedirectToAction(nameof(Index));
        }

        // GET: Players/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var player = await _playerService.GetPlayer(id.Value);
                if (player != null)
                {
                    return View(player);
                }
            }
            catch (BrokenCircuitException)
            {
                HandleBrokenCircuit();
            }

            return NotFound();
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,DateJoined")] PlayerDTO playerDTO)
        {
            if (id != playerDTO.Id)
            {
                return NotFound();
            }

            try
            {
                var resp = await _playerService.UpdatePlayer(playerDTO);
                if (resp != null)
                {
                    return RedirectToAction(nameof(Index));
                }

                var updPlayer = new Player
                {
                    Id = playerDTO.Id,
                    FirstName = playerDTO.FirstName,
                    LastName = playerDTO.LastName
                };

                _playerRepository.UpdatePlayer(updPlayer);
                await _playerRepository.Save();
            }
            catch (BrokenCircuitException)
            {
                HandleBrokenCircuit();
            }

            return View(playerDTO);
        }

        // GET: Players/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var player = await _playerService.GetPlayer(id.Value);
                if (player != null)
                {
                    return View(player);
                }
            }
            catch (BrokenCircuitException)
            {
                HandleBrokenCircuit();
            }

            return NotFound();
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            TempData["PlayInoperativeMsg"] = "";
            try
            {
                await _playerService.DeletePlayer(id);

                await _playerRepository.DeletePlayer(id);
                await _playerRepository.Save();
            }
            catch (BrokenCircuitException)
            {
                HandleBrokenCircuit();
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> PlayerExists(int id)
        {
            var player = await _playerService.GetPlayer(id);
            return player != null;
        }

        private void HandleBrokenCircuit()
        {
            TempData["PlayInoperativeMsg"] = "Player Service Currently Unavailable";
        }

        private async Task UpdateDb()
        {
            try
            {
                var players = await _playerService.GetAll();
                if (players != null)
                {
                    foreach (var player in players)
                    {
                        if (await _playerRepository.GetPlayerRef(player.Id) != null)
                        {
                            continue;
                        }

                        var newPlayer = new Player
                        {
                            PlayerRef = player.Id,
                            FirstName = player.FirstName,
                            LastName = player.LastName,
                            AccountRef = player.AccountRef
                        };
                        _playerRepository.InsertPlayer(newPlayer);
                    }

                    await _playerRepository.Save();
                }
            }
            catch (BrokenCircuitException e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
