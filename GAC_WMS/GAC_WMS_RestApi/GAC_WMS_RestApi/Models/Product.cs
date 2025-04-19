using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GAC_WMS_RestApi.Models
{
    public class Product
    {

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required, MaxLength(50)]
        public string ProductCode { get; set; }

        [Required, MaxLength(150)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public double Length { get; set; }
        public double Width { get; set; }
        public double Weight { get; set; }

        public int Quantity { get; set; }

        [Required, MaxLength(100)]
       
        public string SKU { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        //public ICollection<PurchaseOrderLine> PurchaseOrderLines { get; set; }
        //public ICollection<SalesOrderLine> SalesOrderLines { get; set; }
    }
}
