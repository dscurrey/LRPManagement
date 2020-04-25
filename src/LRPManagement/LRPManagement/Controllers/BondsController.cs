using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LRPManagement.Data;
using LRPManagement.Data.Bonds;
using LRPManagement.Data.Characters;
using LRPManagement.Data.Craftables;
using LRPManagement.Models;
using Polly.CircuitBreaker;

namespace LRPManagement.Controllers
{
    public class BondsController : Controller
    {
        private readonly IBondRepository _repository;
        private readonly ICharacterRepository _characterRepository;
        private readonly ICraftableRepository _itemRepository;
        private readonly IBondService _service;

        public BondsController(IBondRepository repository, ICharacterRepository charRepo, ICraftableRepository itemRepo, IBondService service)
        {
            _repository = repository;
            _characterRepository = charRepo;
            _itemRepository = itemRepo;
            _service = service;
        }

        // GET: Bonds
        public async Task<IActionResult> Index()
        {
            TempData["ItemInoperativeMsg"] = "";
            try
            {
                var bonds = await _service.Get();
                if (bonds != null)
                {
                    foreach (var bond in bonds)
                    {
                        if(await BondExists(bond.ItemId, bond.CharacterId))
                        {
                            continue;
                        }

                        _repository.Insert(bond);
                        await _repository.Save();
                    }
                }
            }
            catch (BrokenCircuitException e)
            {
                HandleBrokenCircuit();
            }


            return View(await _repository.GetAll());
        }

        // GET: Bonds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bond = await _repository.Get(id.Value);

            if (bond == null)
            {
                return NotFound();
            }

            return View(bond);
        }

        // GET: Bonds/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CharacterId"] = "";
            ViewData["ItemId"] = "";

            var characters = await _characterRepository.GetAll();
            var items = await _itemRepository.GetAll();
            if (characters.Any())
            {
                var charList = characters.Select
                (
                    s => new SelectListItem
                    {
                        Value = s.Id.ToString(),
                        Text = s.Name
                    }
                );
                ViewData["CharacterId"] = charList;
            }

            if (items.Any())
            {
                var itemList = items.Select
                (
                    s => new SelectListItem
                    {
                        Value = s.Id.ToString(),
                        Text = s.Name
                    }
                );
                ViewData["ItemId"] = itemList;
            }
            return View();
        }

        // POST: Bonds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CharacterId,ItemId")] Bond bond)
        {
            TempData["ItemInoperativeMsg"] = "";
            if (ModelState.IsValid)
            {
                try
                {
                    var resp = await _service.Create(bond);
                    if (resp == null)
                    {
                        // Unsuccessful
                        return View(bond);
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (BrokenCircuitException)
                {
                    HandleBrokenCircuit();
                }
            }

            var characters = await _characterRepository.GetAll();
            var items = await _itemRepository.GetAll();
            if (characters.Any())
            {
                var charList = characters.Select
                (
                    s => new SelectListItem
                    {
                        Value = s.Id.ToString(),
                        Text = s.Name
                    }
                );
                ViewData["CharacterId"] = charList;
            }

            if (items.Any())
            {
                var itemList = items.Select
                (
                    s => new SelectListItem
                    {
                        Value = s.Id.ToString(),
                        Text = s.Name
                    }
                );
                ViewData["ItemId"] = itemList;
            }

            return View(bond);
        }

        // GET: Bonds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bond = await _repository.Get(id.Value);
            if (bond == null)
            {
                return NotFound();
            }

            return View(bond);
        }

        // POST: Bonds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bond = await _repository.Get(id);
            await _repository.Delete(id);
            await _repository.Save();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> BondExists(int id)
        {
            var bonds = _repository.Get(id);
            if (bonds != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task<bool> BondExists(int itemId, int charId)
        {
            var bonds = await _repository.GetMatch(itemId, charId);
            return bonds != null;
        }

        private void HandleBrokenCircuit()
        {
            TempData["ItemInoperativeMsg"] = "Item Service Currently Unavailable.";
        }
    }
}
