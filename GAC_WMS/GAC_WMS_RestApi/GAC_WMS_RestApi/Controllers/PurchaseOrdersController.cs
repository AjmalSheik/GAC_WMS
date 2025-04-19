using GAC_WMS_RestApi.DatabaseConfig;
using GAC_WMS_RestApi.Dto;
using GAC_WMS_RestApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GAC_WMS_RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrdersController : ControllerBase
    {

        private readonly ApplicationDbContext _context;


        public PurchaseOrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseOrderHeader>> GetPOById(Guid id)
        {
            var po = await _context.PurchaseOrderHeaders
                .Include(p => p.Customer)
                .Include(p => p.Lines)
                    .ThenInclude(l => l.Product)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (po == null) return NotFound();

            return Ok(po);
        }


        [HttpPost("create")]
        public async Task<ActionResult<PurchaseOrderHeader>> CreatePO([FromBody] CreatePurchaseOrderDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var poHeader = new PurchaseOrderHeader
            {
                Id = Guid.NewGuid(),
                CustomerId = dto.CustomerId,
                ProcessingDate = dto.ProcessingDate,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Lines = dto.Lines.Select(l => new PurchaseOrderLine
                {
                    Id = Guid.NewGuid(),
                    ProductId = l.ProductId,
                    Quantity = l.Quantity
                }).ToList()
            };

            _context.PurchaseOrderHeaders.Add(poHeader);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPOById), new { id = poHeader.Id }, poHeader);

            //return Ok(poHeader);
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeletePO(Guid id)
        {
            var po = await _context.PurchaseOrderHeaders
                .Include(p => p.Lines)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (po == null) return NotFound();

            _context.PurchaseOrderLines.RemoveRange(po.Lines);
            _context.PurchaseOrderHeaders.Remove(po);

            await _context.SaveChangesAsync();
            return Ok("Deleted");
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdatePurchaseOrder([FromBody] PurchaseOrderHeader updatedOrder)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingOrder = await _context.PurchaseOrderHeaders
                .Include(p => p.Lines)
                .FirstOrDefaultAsync(p => p.Id == updatedOrder.Id);

            if (existingOrder == null)
                return NotFound("Purchase Order not found");

            // Update header fields
            existingOrder.ProcessingDate = updatedOrder.ProcessingDate;
            existingOrder.CustomerId = updatedOrder.CustomerId;
            existingOrder.UpdatedAt = DateTime.UtcNow;

            // Update lines: Remove existing, Add new
            _context.PurchaseOrderLines.RemoveRange(existingOrder.Lines);
            existingOrder.Lines = updatedOrder.Lines.Select(line => new PurchaseOrderLine
            {
                Id = Guid.NewGuid(),
                ProductId = line.ProductId,
                Quantity = line.Quantity,
                PurchaseOrderHeaderId = existingOrder.Id
            }).ToList();

            await _context.SaveChangesAsync();
            return Ok(existingOrder);
        }


    }
}
