using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LRPManagement.Data;
using LRPManagement.Models;

namespace LRPManagement.Controllers
{
    public class BondsController : Controller
    {
        private readonly LrpDbContext _context;

        public BondsController(LrpDbContext context)
        {
            _context = context;
        }

        // GET: Bonds
        public async Task<IActionResult> Index()
        {
            var lrpDbContext = _context.Bond.Include(b => b.Character).Include(b => b.Item);
            return View(await lrpDbContext.ToListAsync());
        }

        // GET: Bonds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bond = await _context.Bond
                .Include(b => b.Character)
                .Include(b => b.Item)
                .FirstOrDefaultAsync(m => m.CharacterId == id);

            if (bond == null)
            {
                return NotFound();
            }

            return View(bond);
        }

        // GET: Bonds/Create
        public IActionResult Create()
        {
            ViewData["CharacterId"] = new SelectList(_context.Characters, "Id", "Name");
            ViewData["ItemId"] = new SelectList(_context.Craftables, "Id", "Id");
            return View();
        }

        // POST: Bonds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CharacterId,ItemId")] Bond bond)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bond);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CharacterId"] = new SelectList(_context.Characters, "Id", "Name", bond.CharacterId);
            ViewData["ItemId"] = new SelectList(_context.Craftables, "Id", "Id", bond.ItemId);
            return View(bond);
        }

        // GET: Bonds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bond = await _context.Bond.FindAsync(id);
            if (bond == null)
            {
                return NotFound();
            }
            ViewData["CharacterId"] = new SelectList(_context.Characters, "Id", "Name", bond.CharacterId);
            ViewData["ItemId"] = new SelectList(_context.Craftables, "Id", "Id", bond.ItemId);
            return View(bond);
        }

        // POST: Bonds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CharacterId,ItemId")] Bond bond)
        {
            if (id != bond.CharacterId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bond);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BondExists(bond.CharacterId))
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
            ViewData["CharacterId"] = new SelectList(_context.Characters, "Id", "Name", bond.CharacterId);
            ViewData["ItemId"] = new SelectList(_context.Craftables, "Id", "Id", bond.ItemId);
            return View(bond);
        }

        // GET: Bonds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bond = await _context.Bond
                .Include(b => b.Character)
                .Include(b => b.Item)
                .FirstOrDefaultAsync(m => m.CharacterId == id);
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
            var bond = await _context.Bond.FindAsync(id);
            _context.Bond.Remove(bond);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BondExists(int id)
        {
            return _context.Bond.Any(e => e.CharacterId == id);
        }
    }
}
