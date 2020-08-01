using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Customer.Microservice.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Customer.Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IApplicationDbContext _context;
        public CustomerController(IApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Add(Models.Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChanges();
            return Ok(customer.Id);
        }

        [HttpGet("GetAll")]  //Path is added because swagger was throughing error because of same path defination for two methods(GetAll and GetById)
        public async Task<IActionResult> GetAll()
        {
            var customers = await _context.Customers.ToListAsync();
            if (customers == null) return NotFound();
            return Ok(customers);

        }

        [HttpGet("GetById/{id}")] //Path is added because swagger was throughing error because of same path defination for two methods(GetAll and GetById)
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _context.Customers.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (customer == null) return NotFound();

            return Ok(customer);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _context.Customers.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (customer == null) return NotFound();

            _context.Customers.Remove(customer);
            await _context.SaveChanges();

            return Ok(customer.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, Models.Customer customerUpdate)
        {
            var customer = await _context.Customers.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (customer == null) return NotFound();

            customer.FirstName = customerUpdate.FirstName;
            customer.LastName = customerUpdate.LastName;
            customer.Address = customerUpdate.Address;

            await _context.SaveChanges();
            return Ok(id);
        }
    }
}
