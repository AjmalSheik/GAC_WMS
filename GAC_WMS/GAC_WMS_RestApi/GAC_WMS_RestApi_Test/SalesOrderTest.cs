using GAC_WMS_RestApi.Controllers;
using GAC_WMS_RestApi.DatabaseConfig;
using GAC_WMS_RestApi.Dto;
using GAC_WMS_RestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAC_WMS_RestApi_Test
{
    public class SalesOrderTest
    {

        private readonly SalesOrderController _controller;
        private readonly ApplicationDbContext _context;

        public SalesOrderTest()
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
                ProductCode="545456test",
                SKU="TEST-CREATE-AJMAL",
                Title="Test Product",
                Description="Test Aj product"
                
            });

            _context.SaveChanges();

            _controller = new SalesOrderController(_context);
        }
        [Fact]
        public async Task CreateSalesOrder_ReturnsCreatedAtActionResult()
        {
            var dto = new SalesOrderDto
            {
                CustomerId = _context.Customers.FirstAsync().Result.Id,
                ProcessingDate = DateTime.UtcNow,
                ShipmentAddress = new ShipmentAddressDto
                {
                    AddressLine = "456 Test Ln",
                    City = "Testville",
                    State = "TS",
                    ZipCode = "12345",
                    Country = "Testland",
                    CustomerId = _context.Customers.FirstAsync().Result.Id
                },
                Lines = new List<SalesOrderLineDto>
            {
                new SalesOrderLineDto { ProductId = _context.Products.FirstAsync().Result.Id, Quantity = 10 },
                new SalesOrderLineDto { ProductId = _context.Products.FirstAsync().Result.Id, Quantity = 5 }
            }
            };

            var result = await _controller.CreateSalesOrder(dto);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<SalesOrderHeader>(createdResult.Value);
            Assert.Equal(dto.CustomerId, returnValue.CustomerId);
            Assert.Equal(2, returnValue.SalesOrderLines.Count);
        }

        
        
    }



}

