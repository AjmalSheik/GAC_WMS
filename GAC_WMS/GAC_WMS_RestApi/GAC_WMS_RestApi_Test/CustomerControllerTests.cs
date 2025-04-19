using GAC_WMS_RestApi.Controllers;
using GAC_WMS_RestApi.DatabaseConfig;
using GAC_WMS_RestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;


namespace GAC_WMS_RestApi_Test
{
    public  class CustomerControllerTests
    {

        private readonly CustomerController _controller;
        private readonly ApplicationDbContext _context;

        public CustomerControllerTests()
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
            _context.SaveChanges();

            _controller = new CustomerController(_context);
        }

        [Fact]
        public async Task GetCustomer_ReturnsCustomer_WhenCustomerExists()
        {
            var customerId = _context.Customers.First().Id;

           
            var result = await _controller.GetCustomer(customerId);

           
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Customer>(okResult.Value);
            Assert.Equal("Test Customer", returnValue.Name);
        }

        [Fact]
        public async Task CreateCustomer_ReturnsCreatedResult_WhenModelIsValid()
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                Name = "Valid Customer",
                Address = "123 Test St",
                Email = "test@example.com",
                Mobile = "+1234567890"
            };

          
            var result = await _controller.CreateCustomer(customer);

           
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal("GetCustomer", createdAtActionResult.ActionName);
            Assert.NotNull(createdAtActionResult.Value);  
        }

        
        [Fact]
        public async Task CreateCustomer_ReturnsBadRequest_WhenModelIsInvalid()
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                Name = "",  
                Address = "123 Test St",
                Email = "test@example.com",
                Mobile = "+1234567890"
            };

            
            _controller.ModelState.AddModelError("Name", "Name is required");

           
            var result = await _controller.CreateCustomer(customer);

           
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }
    }
}
