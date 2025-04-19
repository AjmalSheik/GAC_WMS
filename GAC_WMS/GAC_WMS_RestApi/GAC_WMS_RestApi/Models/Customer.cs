using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GAC_WMS_RestApi.Models
{
    public class Customer
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [EmailAddress, MaxLength(100)]
        public string Email { get; set; }

        [Phone, MaxLength(20)]
        public string Mobile { get; set; }

        [Required, MaxLength(200)]
        public string Address { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        //public ICollection<PurchaseOrderHeader> PurchaseOrders { get; set; }
        //public ICollection<SalesOrderHeader> SalesOrders { get; set; }
    }
}
