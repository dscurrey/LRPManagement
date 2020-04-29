using LRPManagement.Data.Craftables;
using LRPManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Polly.CircuitBreaker;
using System.Linq;
using System.Threading.Tasks;

namespace LRPManagement.Controllers
{
    public class CraftablesController : Controller
    {
        private readonly ICraftableRepository _itemRepository;
        private readonly ICraftableService _itemService;

        public CraftablesController(ICraftableRepository repo, ICraftableService serv)
        {
            _itemService = serv;
            _itemRepository = repo;
        }

        // GET: Craftables
        public async Task<IActionResult> Index()
        {
            TempData["ItemInoperativeMsg"] = "";

            try
            {
                return View(await _itemRepository.GetAll());
            }
            catch (BrokenCircuitException)
            {
                HandleBrokenCircuit();
            }

            //await UpdateDb();

            return View();
        }

        // GET: Craftables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var craftable = await _itemRepository.GetCraftable(id.Value);

            if (craftable == null)
            {
                return NotFound();
            }

            return View(craftable);
        }

        // GET: Craftables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Craftables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Form,Requirement,Effect,Materials")]
            Craftable craftable)
        {
            if (ModelState.IsValid)
            {
                _itemRepository.InsertCraftable(craftable);
                await _itemRepository.Save();

                return RedirectToAction(nameof(Index));
            }

            return View(craftable);
        }

        // GET: Craftables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var craftable = await _itemRepository.GetCraftable(id.Value);

            if (craftable == null)
            {
                return NotFound();
            }

            return View(craftable);
        }

        // POST: Craftables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,Name,Form,Requirement,Effect,Materials")]
            Craftable craftable)
        {
            if (id != craftable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _itemRepository.UpdateCraftable(craftable);
                    await _itemRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CraftableExists(craftable.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(craftable);
        }

        // GET: Craftables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var craftable = await _itemRepository.GetCraftable(id.Value);

            if (craftable == null)
            {
                return NotFound();
            }

            return View(craftable);
        }

        // POST: Craftables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _itemRepository.DeleteCraftable(id);
            await _itemRepository.Save();

            return RedirectToAction(nameof(Index));
        }

        private void HandleBrokenCircuit()
        {
            TempData["ItemInoperativeMsg"] = "Item Service Currently Unavailable.";
        }

        private async Task<bool> CraftableExists(int id)
        {
            var items = await _itemRepository.GetAll();
            return items.Any();
        }

        private async Task UpdateDb()
        {
            try
            {
                var items = await _itemService.GetAll();
                if (items != null)
                {
                    foreach (var item in items)
                    {
                        if (await _itemRepository.GetCraftable(item.Id) != null)
                        {
                            continue;
                        }

                        var newItem = new Craftable
                        {
                            Name = item.Name,
                            Requirement = item.Requirement,
                            Materials = item.Materials,
                            Effect = item.Effect,
                            Form = item.Form
                        };
                        _itemRepository.InsertCraftable(newItem);
                        await _itemRepository.Save();
                    }
                }
            }
            catch (BrokenCircuitException)
            {
                HandleBrokenCircuit();
            }
        }
    }
}
