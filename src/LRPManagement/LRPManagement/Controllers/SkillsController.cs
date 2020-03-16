using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DTO;
using LRPManagement.Data;
using LRPManagement.Data.Skills;
using Polly.CircuitBreaker;

namespace LRPManagement.Controllers
{
    public class SkillsController : Controller
    {
        private readonly ISkillService _skillService;

        public SkillsController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        // GET: Skills
        public async Task<IActionResult> Index()
        {
            TempData["SkillInoperativeMsg"] = "";
            try
            {
                var skills = await _skillService.GetAll();
                return View(skills);
            }
            catch (BrokenCircuitException)
            {
                HandleBrokenCircuit();
            }

            return View();
        }

        // GET: Skills/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            TempData["SkillInoperativeMsg"] = "";
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var skill = await _skillService.GetSkill(id.Value);
                if (skill == null)
                {
                    return NotFound();
                }

                return View(skill);
            }
            catch (BrokenCircuitException)
            {
                HandleBrokenCircuit();
            }

            return View();
        }

        //// GET: Skills/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Skills/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Name")] SkillDTO skillDTO)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(skillDTO);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(skillDTO);
        //}

        //// GET: Skills/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var skillDTO = await _context.SkillDTO.FindAsync(id);
        //    if (skillDTO == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(skillDTO);
        //}

        //// POST: Skills/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] SkillDTO skillDTO)
        //{
        //    if (id != skillDTO.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(skillDTO);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!SkillDTOExists(skillDTO.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(skillDTO);
        //}

        //// GET: Skills/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var skillDTO = await _context.SkillDTO
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (skillDTO == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(skillDTO);
        //}

        //// POST: Skills/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var skillDTO = await _context.SkillDTO.FindAsync(id);
        //    _context.SkillDTO.Remove(skillDTO);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private async Task<bool> SkillExists(int id)
        {
            var skill = await _skillService.GetSkill(id);
            return skill != null;
        }

        private void HandleBrokenCircuit()
        {
            TempData["SkillInoperativeMsg"] = "Skill Service Currently Unavailable.";
        }
    }
}
