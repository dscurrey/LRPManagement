using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DTO;
using LRPManagement.Data;
using LRPManagement.Data.Skills;
using LRPManagement.Models;
using Polly.CircuitBreaker;

namespace LRPManagement.Controllers
{
    public class SkillsController : Controller
    {
        private readonly ISkillService _skillService;
        private readonly ISkillRepository _skillRepository;

        public SkillsController(ISkillService skillService, ISkillRepository skillRepository)
        {
            _skillService = skillService;
            _skillRepository = skillRepository;
        }

        // GET: Skills
        public async Task<IActionResult> Index()
        {
            TempData["SkillInoperativeMsg"] = "";

            try
            {
                var skills = await _skillService.GetAll();
                if (skills != null)
                {
                    foreach (var skill in skills)
                    {
                        if (await _skillRepository.GetSkillRef(skill.Id) != null)
                        {
                            continue;
                        }
                        var newSkill = new Skill
                        {
                            SkillRef = skill.Id,
                            Name = skill.Name
                        };
                        _skillRepository.InsertSkill(newSkill);
                    }

                    await _skillRepository.Save();
                }
            }
            catch (BrokenCircuitException e)
            {
                Console.WriteLine(e);
            }

            try
            {
                var skills = await _skillRepository.GetAll();
                var skillList = skills.Select
                (
                    s => new SkillDTO
                    {
                        Name = s.Name
                    }
                );
                return View(skillList);
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

        // GET: Skills/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Skills/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] SkillDTO skillDTO)
        {
            TempData["SkillInoperativeMsg"] = "";
            try
            {
                var resp = await _skillService.CreateSkill(skillDTO);
                if (resp == null)
                {
                    // Unsuccessful/Error
                    return View(skillDTO);
                }
            }
            catch (BrokenCircuitException)
            {
                HandleBrokenCircuit();
            }

            var newSkill = new Skill
            {
                Name = skillDTO.Name
            };
            
            _skillRepository.InsertSkill(newSkill);
            await _skillRepository.Save();

            return RedirectToAction(nameof(Index));
        }

        // GET: Skills/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var skill = await _skillService.GetSkill(id.Value);
                if (skill != null)
                {
                    return View(skill);
                }
            }
            catch (BrokenCircuitException)
            {
                HandleBrokenCircuit();
            }

            return NotFound();
        }

        // POST: Skills/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] SkillDTO skillDTO)
        {
            if (id != skillDTO.Id)
            {
                return NotFound();
            }

            try
            {
                var resp = await _skillService.UpdateSkill(skillDTO);
                if (resp != null)
                {
                    return RedirectToAction(nameof(Index));
                }

                var updSkill = new Skill
                {
                    Name = skillDTO.Name
                };

                _skillRepository.UpdateSkill(updSkill);
                await _skillRepository.Save();
            }
            catch (BrokenCircuitException)
            {
                HandleBrokenCircuit();
            }

            return View(skillDTO);
        }

        // GET: Skills/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var skill = await _skillService.GetSkill(id.Value);
                if (skill != null)
                {
                    return View(skill);
                }
            }
            catch (BrokenCircuitException)
            {
                HandleBrokenCircuit();
            }

            return NotFound();
        }

        // POST: Skills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            TempData["SkillInoperativeMsg"] = "";
            try
            {
                await _skillService.DeleteSkill(id);

                _skillRepository.DeleteSkill(id);
                await _skillRepository.Save();
            }
            catch (BrokenCircuitException)
            {
                HandleBrokenCircuit();
            }

            return RedirectToAction(nameof(Index));
        }

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
