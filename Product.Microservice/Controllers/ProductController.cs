using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Product.Microservice.Contexts;

namespace Product.Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IApplicationDbContext _context;
        public ProductController(IApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Add(Models.Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChanges();
            return Ok(product.Id);
        }

        [HttpGet("GetAll")]  //Path is added because swagger was throughing error because of same path defination for two methods(GetAll and GetById)
        public async Task<IActionResult> GetAll()
        {
            var products = await _context.Products.ToListAsync();
            if (products == null) return NotFound();
            return Ok(products);
        }

        [HttpGet("GetById/{id}")] //Path is added because swagger was throughing error because of same path defination for two methods(GetAll and GetById)
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _context.Products.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (product == null) return NotFound();
            _context.Products.Remove(product);
            await _context.SaveChanges();
            return Ok(product.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, Models.Product productUpdate)
        {
            var product = await _context.Products.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (product == null) return NotFound();
            product.Name = productUpdate.Name;
            product.Quantity = productUpdate.Quantity;
            product.Category = productUpdate.Category;
            await _context.SaveChanges();
            return Ok(product.Id);
        }
    }
}
