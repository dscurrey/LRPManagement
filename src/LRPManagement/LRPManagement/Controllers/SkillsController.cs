using DTO;
using LRPManagement.Data.Skills;
using LRPManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polly.CircuitBreaker;
using System.Linq;
using System.Threading.Tasks;

namespace LRPManagement.Controllers
{
    [Authorize(Policy = "StaffOnly")]
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
                var skills = await _skillRepository.GetAll();
                var skillList = skills.Select
                (
                    s => new SkillDTO
                    {
                        Name = s.Name,
                        XpCost = s.XpCost
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
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Skills/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
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
                    ViewBag.SkillError = "An Error Occurred";
                    return View(skillDTO);
                }
            }
            catch (BrokenCircuitException)
            {
                HandleBrokenCircuit();
            }

            return View(skillDTO);
        }

        // GET: Skills/Edit/5
        [Authorize(Roles = "Admin")]
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
                return View();
            }

            return NotFound();
        }

        // POST: Skills/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
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
                    Name = skillDTO.Name,
                    XpCost = skillDTO.XpCost
                };

                _skillRepository.UpdateSkill(updSkill);
                await _skillRepository.Save();
            }
            catch (BrokenCircuitException)
            {
                HandleBrokenCircuit();
                return View();
            }

            return View(skillDTO);
        }

        // GET: Skills/Delete/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            TempData["SkillInoperativeMsg"] = "";
            try
            {
                await _skillService.DeleteSkill(id);

                await _skillRepository.DeleteSkill(id);
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
            ViewBag.SkillError = "Skill Service is Currently Unavailable";
        }
    }
}
