using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GAC_WMS_RestApi.Models
{
    public class SalesOrderLine
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        
        [Required]
        public Guid SalesOrderHeaderId { get; set; }

        [ForeignKey(nameof(SalesOrderHeaderId))]
        public SalesOrderHeader SalesOrderHeader { get; set; }

        
        [Required]
        public Guid ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        [Required]
        public int Quantity { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}
