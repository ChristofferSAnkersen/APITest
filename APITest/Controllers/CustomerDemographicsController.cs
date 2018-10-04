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
    public class CustomerDemographicsController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public CustomerDemographicsController(NorthwindContext context)
        {
            _context = context;
        }

        // GET: api/CustomerDemographics
        [HttpGet]
        public IEnumerable<CustomerDemographics> GetCustomerDemographics()
        {
            return _context.CustomerDemographics;
        }

        // GET: api/CustomerDemographics/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerDemographics([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customerDemographics = await _context.CustomerDemographics.FindAsync(id);

            if (customerDemographics == null)
            {
                return NotFound();
            }

            return Ok(customerDemographics);
        }

        // PUT: api/CustomerDemographics/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerDemographics([FromRoute] string id, [FromBody] CustomerDemographics customerDemographics)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customerDemographics.CustomerTypeId)
            {
                return BadRequest();
            }

            _context.Entry(customerDemographics).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerDemographicsExists(id))
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

        // POST: api/CustomerDemographics
        [HttpPost]
        public async Task<IActionResult> PostCustomerDemographics([FromBody] CustomerDemographics customerDemographics)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CustomerDemographics.Add(customerDemographics);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CustomerDemographicsExists(customerDemographics.CustomerTypeId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCustomerDemographics", new { id = customerDemographics.CustomerTypeId }, customerDemographics);
        }

        // DELETE: api/CustomerDemographics/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerDemographics([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customerDemographics = await _context.CustomerDemographics.FindAsync(id);
            if (customerDemographics == null)
            {
                return NotFound();
            }

            _context.CustomerDemographics.Remove(customerDemographics);
            await _context.SaveChangesAsync();

            return Ok(customerDemographics);
        }

        private bool CustomerDemographicsExists(string id)
        {
            return _context.CustomerDemographics.Any(e => e.CustomerTypeId == id);
        }
    }
}