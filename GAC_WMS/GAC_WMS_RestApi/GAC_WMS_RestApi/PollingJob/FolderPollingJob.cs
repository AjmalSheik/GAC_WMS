using GAC_WMS_RestApi.Dto;
using GAC_WMS_RestApi.PollingJob.HelperMethods;
using Quartz;
using System.Text.Json;
using System.Text;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http.HttpResults;

namespace GAC_WMS_RestApi.PollingJob
{
    [DisallowConcurrentExecution]
    public class FolderPollingJob : IJob
    {
        //private readonly PurchaseOrderParser _parser;

        private readonly IConfiguration _configuration;
        private readonly string _folderPath;
        private readonly string _apiUrl;
        private readonly ILogger<FolderPollingJob> _logger;


        public FolderPollingJob(IConfiguration configuration, ILogger<FolderPollingJob> logger)
        {
            _configuration = configuration;
            _folderPath = _configuration["QuartzConfig:DirectoryPath"];
            _apiUrl = _configuration["QuartzConfig:ApiUrl"];
            //_parser = parser;
            _logger = logger;


        }
        public Task Execute(IJobExecutionContext context)
        {
            PollAndProcessXmlFilesAsync();


            return Task.CompletedTask;
        }

        public async Task PollAndProcessXmlFilesAsync()
        {
            _logger.LogInformation("First execution started");

            if (!Directory.Exists(_folderPath))
            {
                _logger.LogWarning("Directory not found: {Path}", _folderPath);
                return;
            }

            var xmlFiles = Directory.GetFiles(_folderPath, "*.xml");

            foreach (var file in xmlFiles)
            {
                try
                {
                    var dto = ParseXmlToDto(file);
                    await PostToApi(dto);
                    
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to process file: {File}", file);
                }
            }
        }

        private CreatePurchaseOrderDto ParseXmlToDto(string filePath)
        {
            var xdoc = XDocument.Load(filePath);
            var root = xdoc.Element("PurchaseOrder");

            var dto = new CreatePurchaseOrderDto
            {
                CustomerId = Guid.Parse(root.Element("CustomerId").Value),
                ProcessingDate = DateTime.Parse(root.Element("ProcessingDate").Value),
                Lines = new List<PurchaseOrderLineDto>()
            };

            var lines = root.Element("Lines").Elements("Line");
            foreach (var line in lines)
            {
                dto.Lines.Add(new PurchaseOrderLineDto
                {
                    ProductId = Guid.Parse(line.Element("ProductId").Value),
                    Quantity = int.Parse(line.Element("Quantity").Value)
                });
            }

            return dto;
        }
        private async Task PostToApi(CreatePurchaseOrderDto dto)
        {
            using var client = new HttpClient();
                
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            //api/PurchaseOrders/create

            var response = await client.PostAsync(_apiUrl+"api/PurchaseOrders/create", content);
            response.EnsureSuccessStatusCode();

            Console.WriteLine($"Posted PO to API: {response.StatusCode}");
        }
    }
}
