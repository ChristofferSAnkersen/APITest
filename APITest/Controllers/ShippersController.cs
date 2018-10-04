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
    public class ShippersController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public ShippersController(NorthwindContext context)
        {
            _context = context;
        }

        // GET: api/Shippers
        [HttpGet]
        public IEnumerable<Shippers> GetShippers()
        {
            return _context.Shippers;
        }

        // GET: api/Shippers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShippers([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shippers = await _context.Shippers.FindAsync(id);

            if (shippers == null)
            {
                return NotFound();
            }

            return Ok(shippers);
        }

        // PUT: api/Shippers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShippers([FromRoute] int id, [FromBody] Shippers shippers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shippers.ShipperId)
            {
                return BadRequest();
            }

            _context.Entry(shippers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShippersExists(id))
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

        // POST: api/Shippers
        [HttpPost]
        public async Task<IActionResult> PostShippers([FromBody] Shippers shippers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Shippers.Add(shippers);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShippers", new { id = shippers.ShipperId }, shippers);
        }

        // DELETE: api/Shippers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShippers([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shippers = await _context.Shippers.FindAsync(id);
            if (shippers == null)
            {
                return NotFound();
            }

            _context.Shippers.Remove(shippers);
            await _context.SaveChangesAsync();

            return Ok(shippers);
        }

        private bool ShippersExists(int id)
        {
            return _context.Shippers.Any(e => e.ShipperId == id);
        }
    }
}