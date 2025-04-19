using GAC_WMS_RestApi.Dto;

namespace GAC_WMS_RestApi.PollingJob.HelperMethods
{
    public class PurchaseOrderParser
    {
        public CreatePurchaseOrderDto ParseXml(string xmlFilePath)
        {
            try
            {
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(CreatePurchaseOrderDto));
                using (var stream = new FileStream(xmlFilePath, FileMode.Open))
                {
                    var purchaseOrder = (CreatePurchaseOrderDto)serializer.Deserialize(stream);
                    return new CreatePurchaseOrderDto
                    {
                        CustomerId = purchaseOrder.CustomerId,
                        ProcessingDate = purchaseOrder.ProcessingDate,
                        Lines = purchaseOrder.Lines.Select(l => new PurchaseOrderLineDto
                        {
                            ProductId = l.ProductId,
                            Quantity = l.Quantity
                        }).ToList()
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing XML: {ex.Message}");
                return null;
            }
        }
    }
}
