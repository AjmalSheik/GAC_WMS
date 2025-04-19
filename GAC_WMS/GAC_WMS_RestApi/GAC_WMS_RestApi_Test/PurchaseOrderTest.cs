using GAC_WMS_RestApi.Controllers;
using GAC_WMS_RestApi.DatabaseConfig;
using GAC_WMS_RestApi.Dto;
using GAC_WMS_RestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace GAC_WMS_RestApi_Test
{
    public class PurchaseOrderTest
    {
        private readonly PurchaseOrdersController _controller;
        private readonly ApplicationDbContext _context;

        public PurchaseOrderTest()
        {

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationDbContext(options);


            _context.Customers.Add(new Customer
            {
                Id = Guid.NewGuid(),
                Name = "Test Customer",
                Address = "123 Test St",
                Email = "test@example.com",
                Mobile = "+1234567890"
            });

            _context.Products.Add(new Product
            {
                ProductCode = "545456test",
                SKU = "TEST-CREATE-AJMAL",
                Title = "Test Product",
                Description = "Test Aj product"

            });

            _context.SaveChanges();

            _controller = new PurchaseOrdersController(_context);
        }

        [Fact]
        public async Task CreatePurchaseOrder_ReturnsCreatedAtActionResult()
        {
            var dto = new CreatePurchaseOrderDto
            {
                CustomerId = _context.Customers.FirstAsync().Result.Id,
                ProcessingDate = DateTime.UtcNow,

                Lines = new List<PurchaseOrderLineDto> 
    {
        new PurchaseOrderLineDto { ProductId = _context.Products.FirstAsync().Result.Id, Quantity = 10 },
        new PurchaseOrderLineDto { ProductId = _context.Products.FirstAsync().Result.Id, Quantity = 5 }
    }
            };

            var result = await _controller.CreatePO(dto);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<PurchaseOrderHeader>(createdResult.Value);
            Assert.Equal(dto.CustomerId, returnValue.CustomerId);
            Assert.Equal(2, returnValue.Lines.Count);
        }
    }
}
