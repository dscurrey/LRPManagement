﻿using DTO;
using LRP.Items.Data.Craftables;
using LRP.Items.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRP.Items.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CraftablesController : ControllerBase
    {
        private readonly ICraftableRepository _craftRepository;

        public CraftablesController(ICraftableRepository craftRepository)
        {
            _craftRepository = craftRepository;
        }

        // GET: api/Craftables
        /// <summary>
        /// Action to get all items/craftables
        /// </summary>
        /// <returns>List of all craftables</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Craftable>>> GetCraftables()
        {
            return await _craftRepository.GetAll();
        }

        // GET: api/Craftables/5
        /// <summary>
        /// Gets a single craftable with a unique ID
        /// </summary>
        /// <param name="id">Character ID</param>
        /// <returns>Character with chosen ID</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CraftableDTO>> GetCraftable(int id)
        {
            var craftable = await _craftRepository.GetCraftable(id);
            var dto = new CraftableDTO
            {
                Id = craftable.Id,
                Effect = craftable.Effect,
                Form = craftable.Form,
                Materials = craftable.Materials,
                Name = craftable.Name,
                Requirement = craftable.Requirement
            };

            if (craftable == null) return NotFound();

            return Ok(dto);
        }

        // PUT: api/Craftables/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Updates a craftable
        /// </summary>
        /// <param name="id">Id of the craftable</param>
        /// <param name="craftable">The craftable to be updated</param>
        /// <returns>NoContent if successful</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCraftable(int id, Craftable craftable)
        {
            if (id != craftable.Id) return BadRequest();

            _craftRepository.UpdateCraftable(craftable);

            try
            {
                await _craftRepository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CraftableExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // POST: api/Craftables
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Creates a new craftable in the database/repository
        /// </summary>
        /// <param name="craftable">The craftable to be inserted</param>
        /// <returns>The inserted craftable</returns>
        [HttpPost]
        public async Task<ActionResult<Craftable>> PostCraftable(Craftable craftable)
        {
            if (string.IsNullOrEmpty(craftable.Name) || string.IsNullOrEmpty
                (craftable.Requirement) || string.IsNullOrEmpty(craftable.Materials) || string.IsNullOrEmpty
                (craftable.Form) || string.IsNullOrEmpty(craftable.Effect))
                return BadRequest();

            if (await _craftRepository.GetCraftable(craftable.Id) != null)
            {
                _craftRepository.UpdateCraftable(craftable);
                await _craftRepository.Save();
            }

            _craftRepository.InsertCraftable(craftable);
            await _craftRepository.Save();

            return CreatedAtAction("GetCraftable", new {id = craftable.Id}, craftable);
        }

        // DELETE: api/Craftables/5
        /// <summary>
        /// Deletes a craftable
        /// </summary>
        /// <param name="id">The unique ID of the chosen craftable</param>
        /// <returns>The deleted craftable</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Craftable>> DeleteCraftable(int id)
        {
            var craftable = await _craftRepository.GetCraftable(id);
            if (craftable == null) return NotFound();

            await _craftRepository.DeleteCraftable(id);
            await _craftRepository.Save();

            return Ok(craftable);
        }

        private async Task<bool> CraftableExists(int id)
        {
            var items = await _craftRepository.GetAll();
            return items.Any(e => e.Id == id);
        }
    }
}