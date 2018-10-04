using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APITest.Models;

namespace APITest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TerritoriesController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public TerritoriesController(NorthwindContext context)
        {
            _context = context;
        }

        // GET: api/Territories
        [HttpGet]
        public IEnumerable<Territories> GetTerritories()
        {
            return _context.Territories;
        }

        // GET: api/Territories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTerritories([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var territories = await _context.Territories.FindAsync(id);

            if (territories == null)
            {
                return NotFound();
            }

            return Ok(territories);
        }

        // PUT: api/Territories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTerritories([FromRoute] string id, [FromBody] Territories territories)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != territories.TerritoryId)
            {
                return BadRequest();
            }

            _context.Entry(territories).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TerritoriesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Territories
        [HttpPost]
        public async Task<IActionResult> PostTerritories([FromBody] Territories territories)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Territories.Add(territories);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TerritoriesExists(territories.TerritoryId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTerritories", new { id = territories.TerritoryId }, territories);
        }

        // DELETE: api/Territories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTerritories([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var territories = await _context.Territories.FindAsync(id);
            if (territories == null)
            {
                return NotFound();
            }

            _context.Territories.Remove(territories);
            await _context.SaveChangesAsync();

            return Ok(territories);
        }

        private bool TerritoriesExists(string id)
        {
            return _context.Territories.Any(e => e.TerritoryId == id);
        }
    }
}