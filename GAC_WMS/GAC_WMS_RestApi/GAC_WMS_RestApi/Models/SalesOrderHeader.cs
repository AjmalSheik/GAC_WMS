using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GAC_WMS_RestApi.Models
{
    public class SalesOrderHeader
    {

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public DateTime ProcessingDate { get; set; }

        // Foreign Key for Customer
        [Required]
        public Guid CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; }

        // Foreign Key for Shipment Address
        [Required]
        public Guid ShipmentAddressId { get; set; }

        [ForeignKey(nameof(ShipmentAddressId))]
        public ShipmentAddress ShipmentAddress { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

       
        public ICollection<SalesOrderLine> SalesOrderLines { get; set; }
    }
}
