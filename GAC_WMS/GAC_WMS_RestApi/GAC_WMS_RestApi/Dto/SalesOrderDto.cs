using System.ComponentModel.DataAnnotations;

namespace GAC_WMS_RestApi.Dto
{
    public class SalesOrderDto
    {
        public Guid Id { get; set; }
        public DateTime ProcessingDate { get; set; }

        [Required]
        public Guid CustomerId { get; set; }
        public Guid ShipmentAddressId { get; set; }

        [Required]
        public ShipmentAddressDto ShipmentAddress { get; set; }

        public List<SalesOrderLineDto> Lines { get; set; }
    }

    public class SalesOrderLineDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class ShipmentAddressDto
    {
        
        public string AddressLine { get; set; }

        
        public string City { get; set; }

       
        public string State { get; set; }

        
        public string ZipCode { get; set; }

       
        public string Country { get; set; }

       
        public Guid CustomerId { get; set; }
    }
}
