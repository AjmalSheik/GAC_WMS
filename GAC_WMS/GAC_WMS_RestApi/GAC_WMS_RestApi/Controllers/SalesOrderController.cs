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
    public class SalesOrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SalesOrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public async Task<ActionResult<SalesOrderHeader>> CreateSalesOrder([FromBody] SalesOrderDto salesOrderDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var shipmentAddress = new ShipmentAddress
            {
                Id = Guid.NewGuid(),
                AddressLine = salesOrderDto.ShipmentAddress.AddressLine,
                City = salesOrderDto.ShipmentAddress.City,
                State = salesOrderDto.ShipmentAddress.State,
                ZipCode = salesOrderDto.ShipmentAddress.ZipCode,
                Country = salesOrderDto.ShipmentAddress.Country,
                CustomerId = salesOrderDto.ShipmentAddress.CustomerId,
            };

            var salesOrder = new SalesOrderHeader
            {
                Id = Guid.NewGuid(),
                ProcessingDate = salesOrderDto.ProcessingDate,
                CustomerId = salesOrderDto.CustomerId,
                ShipmentAddressId = shipmentAddress.Id,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                SalesOrderLines = salesOrderDto.Lines.Select(line => new SalesOrderLine
                {
                    Id = Guid.NewGuid(),
                    ProductId = line.ProductId,
                    Quantity = line.Quantity,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }).ToList()
            };

            _context.ShipmentAddress.Add(shipmentAddress);
            _context.SalesOrderHeaders.Add(salesOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSalesOrderHeader), new { id = salesOrder.Id }, salesOrder);
        }

        [HttpGet("get-header/{id}")]
        public async Task<ActionResult<SalesOrderHeader>> GetSalesOrderHeader(Guid id)
        {
            var salesOrderHeader = await _context.SalesOrderHeaders
                .Include(so => so.Customer)
                .Include(so => so.ShipmentAddress)
                .Include(so => so.SalesOrderLines)
                .FirstOrDefaultAsync(so => so.Id == id);

            if (salesOrderHeader == null)
                return NotFound();

            return Ok(salesOrderHeader);
        }

        [HttpPost("get-all")]
        public async Task<ActionResult<IEnumerable<SalesOrderHeader>>> GetAllSalesOrders()
        {
            var salesOrders = await _context.SalesOrderHeaders
                .Include(so => so.Customer)
                .Include(so => so.ShipmentAddress)
                .Include(so => so.SalesOrderLines)
                .ToListAsync();

            return Ok(salesOrders);
        }

        [HttpPost("update/{id}")]
        public async Task<IActionResult> UpdateSalesOrder(Guid id, [FromBody] SalesOrderDto salesOrderDto)
        {
            if (id != salesOrderDto.Id)
                return BadRequest("Sales Order ID mismatch.");

            var salesOrderHeader = await _context.SalesOrderHeaders
                .Include(so => so.SalesOrderLines)
                .FirstOrDefaultAsync(so => so.Id == id);

            if (salesOrderHeader == null)
                return NotFound();

            salesOrderHeader.ProcessingDate = salesOrderDto.ProcessingDate;
            salesOrderHeader.CustomerId = salesOrderDto.CustomerId;
            salesOrderHeader.ShipmentAddressId = salesOrderDto.ShipmentAddressId;
            salesOrderHeader.UpdatedAt = DateTime.UtcNow;

            _context.SalesOrderLines.RemoveRange(salesOrderHeader.SalesOrderLines);

            foreach (var lineDto in salesOrderDto.Lines)
            {
                var salesOrderLine = new SalesOrderLine
                {
                    Id = Guid.NewGuid(),
                    SalesOrderHeaderId = salesOrderHeader.Id,
                    ProductId = lineDto.ProductId,
                    Quantity = lineDto.Quantity,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.SalesOrderLines.Add(salesOrderLine);
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteSalesOrder(Guid id)
        {
            var salesOrderHeader = await _context.SalesOrderHeaders
                .Include(so => so.SalesOrderLines)
                .FirstOrDefaultAsync(so => so.Id == id);

            if (salesOrderHeader == null)
                return NotFound();

            _context.SalesOrderLines.RemoveRange(salesOrderHeader.SalesOrderLines);
            _context.SalesOrderHeaders.Remove(salesOrderHeader);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
