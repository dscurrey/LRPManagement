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
    public class CraftablesController : Controller
    {
        private readonly LrpDbContext _context;

        public CraftablesController(LrpDbContext context)
        {
            _context = context;
        }

        // GET: Craftables
        public async Task<IActionResult> Index()
        {
            return View(await _context.Craftables.ToListAsync());
        }

        // GET: Craftables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var craftable = await _context.Craftables
                .FirstOrDefaultAsync(m => m.Id == id);
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
        public async Task<IActionResult> Create([Bind("Id,Name,Form,Requirement,Effect,Materials")] Craftable craftable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(craftable);
                await _context.SaveChangesAsync();
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

            var craftable = await _context.Craftables.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Form,Requirement,Effect,Materials")] Craftable craftable)
        {
            if (id != craftable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(craftable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CraftableExists(craftable.Id))
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

            var craftable = await _context.Craftables
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var craftable = await _context.Craftables.FindAsync(id);
            _context.Craftables.Remove(craftable);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CraftableExists(int id)
        {
            return _context.Craftables.Any(e => e.Id == id);
        }
    }
}
