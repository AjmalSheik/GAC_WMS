namespace GAC_WMS_RestApi.Dto
{
    public class CreatePurchaseOrderDto
    {

      
            public Guid CustomerId { get; set; }
            public DateTime ProcessingDate { get; set; }
            public List<PurchaseOrderLineDto> Lines { get; set; }
       

       
    }

    public class PurchaseOrderLineDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
