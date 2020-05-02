using LRPManagement.Data.Bonds;
using LRPManagement.Data.Characters;
using LRPManagement.Data.Craftables;
using LRPManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Polly.CircuitBreaker;
using System.Linq;
using System.Threading.Tasks;

namespace LRPManagement.Controllers
{
    /// <summary>
    /// Bonds controller used for interactions involving Character/Item relationship.
    /// Staff Only (referees/admin)
    /// </summary>
    [Authorize(Policy = "StaffOnly")]
    public class BondsController : Controller
    {
        private readonly IBondRepository _repository;
        private readonly ICharacterRepository _characterRepository;
        private readonly ICraftableRepository _itemRepository;
        private readonly IBondService _service;

        /// <summary>
        /// Creates a new instance of the <see cref="BondsController"/> class
        /// </summary>
        /// <param name="repository">Repository containing bonds and access methods</param>
        /// <param name="charRepo">Repository containing characters and access methods</param>
        /// <param name="itemRepo">Repository containing items and access methods</param>
        /// <param name="service">Class containing methods to call the items API</param>
        public BondsController(IBondRepository repository,
            ICharacterRepository charRepo,
            ICraftableRepository itemRepo,
            IBondService service)
        {
            _repository = repository;
            _characterRepository = charRepo;
            _itemRepository = itemRepo;
            _service = service;
        }

        // GET: Bonds
        /// <summary>
        /// For displaying an index of all bonds
        /// </summary>
        /// <returns>Index view containing bonds in repository</returns>
        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetAll());
        }

        // GET: Bonds/Details/5
        /// <summary>
        /// Displays details of a bond
        /// </summary>
        /// <param name="id">ID of the chosen bond</param>
        /// <returns>View, containing details of a bond</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var bond = await _repository.Get(id.Value);

            if (bond == null) return NotFound();

            return View(bond);
        }

        // GET: Bonds/Create
        /// <summary>
        /// Create populates information in the create form
        /// </summary>
        /// <returns>View containing data required to populate form</returns>
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
        /// <summary>
        /// Takes the POST from create form
        /// </summary>
        /// <param name="bond">The bond to be created</param>
        /// <returns>Redirects to index</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CharacterId,ItemId")] Bond bond)
        {
            TempData["ItemInoperativeMsg"] = "";
            if (ModelState.IsValid)
                try
                {
                    var resp = await _service.Create(bond);
                    if (resp == null)
                        // Unsuccessful
                        return View(bond);

                    return RedirectToAction(nameof(Index));
                }
                catch (BrokenCircuitException)
                {
                    HandleBrokenCircuit();
                    return View();
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
        /// <summary>
        /// Populates confirm delete view with chosen bond
        /// </summary>
        /// <param name="id">ID of delete target</param>
        /// <returns>Confirm deletion page</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var bond = await _repository.Get(id.Value);
            if (bond == null) return NotFound();

            return View(bond);
        }

        // POST: Bonds/Delete/5
        /// <summary>
        /// Deletes a chosen bond
        /// </summary>
        /// <param name="id">Id for the chosen bond</param>
        /// <returns>Redirect to index</returns>
        [HttpPost]
        [ActionName("Delete")]
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
            var bonds = await _repository.Get(id);
            if (bonds != null)
                return true;
            else
                return false;
        }

        private async Task<bool> BondExists(int itemId, int charId)
        {
            var bonds = await _repository.GetMatch(charId, itemId);
            return bonds != null;
        }

        private void HandleBrokenCircuit()
        {
            ViewBag.BondsError = "Items Service Unavailable";
        }
    }
}