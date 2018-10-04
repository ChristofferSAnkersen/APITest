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
    public class CustomerCustomerDemoesController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public CustomerCustomerDemoesController(NorthwindContext context)
        {
            _context = context;
        }

        // GET: api/CustomerCustomerDemoes
        [HttpGet]
        public IEnumerable<CustomerCustomerDemo> GetCustomerCustomerDemo()
        {
            return _context.CustomerCustomerDemo;
        }

        // GET: api/CustomerCustomerDemoes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerCustomerDemo([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customerCustomerDemo = await _context.CustomerCustomerDemo.FindAsync(id);

            if (customerCustomerDemo == null)
            {
                return NotFound();
            }

            return Ok(customerCustomerDemo);
        }

        // PUT: api/CustomerCustomerDemoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerCustomerDemo([FromRoute] string id, [FromBody] CustomerCustomerDemo customerCustomerDemo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customerCustomerDemo.CustomerId)
            {
                return BadRequest();
            }

            _context.Entry(customerCustomerDemo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerCustomerDemoExists(id))
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

        // POST: api/CustomerCustomerDemoes
        [HttpPost]
        public async Task<IActionResult> PostCustomerCustomerDemo([FromBody] CustomerCustomerDemo customerCustomerDemo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CustomerCustomerDemo.Add(customerCustomerDemo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CustomerCustomerDemoExists(customerCustomerDemo.CustomerId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCustomerCustomerDemo", new { id = customerCustomerDemo.CustomerId }, customerCustomerDemo);
        }

        // DELETE: api/CustomerCustomerDemoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerCustomerDemo([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customerCustomerDemo = await _context.CustomerCustomerDemo.FindAsync(id);
            if (customerCustomerDemo == null)
            {
                return NotFound();
            }

            _context.CustomerCustomerDemo.Remove(customerCustomerDemo);
            await _context.SaveChangesAsync();

            return Ok(customerCustomerDemo);
        }

        private bool CustomerCustomerDemoExists(string id)
        {
            return _context.CustomerCustomerDemo.Any(e => e.CustomerId == id);
        }
    }
}