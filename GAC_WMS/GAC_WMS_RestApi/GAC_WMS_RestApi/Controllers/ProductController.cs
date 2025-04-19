using GAC_WMS_RestApi.DatabaseConfig;
using GAC_WMS_RestApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace GAC_WMS_RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

       
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            return product;
        }

        
        [HttpPost("create")]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //product.Id = Guid.NewGuid();
            //product.CreatedAt = DateTime.UtcNow;
            //product.UpdatedAt = DateTime.UtcNow;

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

       
        [HttpPost("update")]
        public async Task<IActionResult> UpdateProduct([FromBody] Product updatedProduct)
        {
            var existingProduct = await _context.Products.FindAsync(updatedProduct.Id);
            if (existingProduct == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            existingProduct.ProductCode = updatedProduct.ProductCode;
            existingProduct.Title = updatedProduct.Title;
            existingProduct.Description = updatedProduct.Description;
            existingProduct.Length = updatedProduct.Length;
            existingProduct.Width = updatedProduct.Width;
            existingProduct.Weight = updatedProduct.Weight;
            existingProduct.Quantity = updatedProduct.Quantity;
            existingProduct.SKU = updatedProduct.SKU;
            existingProduct.IsActive = updatedProduct.IsActive;
            existingProduct.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(existingProduct);
        }

        
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

