using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DTO;
using LRPManagement.Data;
using LRPManagement.Data.Characters;
using Polly.CircuitBreaker;

namespace LRPManagement.Controllers
{
    public class CharactersController : Controller
    {
        private readonly ICharacterService _characterService;

        public CharactersController(ICharacterService characterService)
        {
            _characterService = characterService;
            TempData["CharInoperativeMsg"] = "";
        }

        // GET: Characters
        public async Task<IActionResult> Index()
        {
            try
            {
                var characters = await _characterService.GetAll();
                return View(characters);
            }
            catch (BrokenCircuitException)
            {
                HandleBrokenCircuit();
            }
            return View();
        }

        // GET: Characters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var character = await _characterService.GetCharacter(id.Value);
                if (character == null)
                {
                    return NotFound();
                }

                return View(character);
            }
            catch (BrokenCircuitException)
            {
                HandleBrokenCircuit();
            }
            return View();
        }

        //// GET: Characters/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Characters/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,PlayerId,Name,IsActive,IsRetired")] CharacterDTO characterDTO)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(characterDTO);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(characterDTO);
        //}

        //// GET: Characters/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var characterDTO = await _context.CharacterDTO.FindAsync(id);
        //    if (characterDTO == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(characterDTO);
        //}

        //// POST: Characters/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,PlayerId,Name,IsActive,IsRetired")] CharacterDTO characterDTO)
        //{
        //    if (id != characterDTO.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(characterDTO);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!CharacterDTOExists(characterDTO.Id))
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
        //    return View(characterDTO);
        //}

        //// GET: Characters/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var characterDTO = await _context.CharacterDTO
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (characterDTO == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(characterDTO);
        //}

        //// POST: Characters/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var characterDTO = await _context.CharacterDTO.FindAsync(id);
        //    _context.CharacterDTO.Remove(characterDTO);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private async Task<bool> CharacterDTOExists(int id)
        {
            var character = await _characterService.GetCharacter(id);
            return character != null;
        }

        private void HandleBrokenCircuit()
        {
            TempData["CharInoperativeMsg"] = "Character Service Currently Unavailable.";
        }
    }
}
