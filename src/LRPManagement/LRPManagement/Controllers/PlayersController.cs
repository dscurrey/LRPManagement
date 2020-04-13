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
using Polly.CircuitBreaker;

namespace LRPManagement.Controllers
{
    public class PlayersController : Controller
    {
        private readonly IPlayerService _playerService;

        public PlayersController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        // GET: Players
        public async Task<IActionResult> Index()
        {
            TempData["PlayInoperativeMsg"] = "";
            try
            {
                var players = await _playerService.GetAll();
                return View(players);
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
                var player = await _playerService.GetPlayer(id.Value);
                if (player == null)
                {
                    return NotFound();
                }

                return View(player);
            }
            catch
            {
                HandleBrokenCircuit();
            }

            return View();
        }

        // GET: Players/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Players/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,DateJoined")] PlayerDTO playerDTO)
        {
            TempData["PlayInoperativeMsg"] = "";
            try
            {
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
    }
}
