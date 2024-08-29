using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Serilog;
using Storage.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Storage.Infrastructure.Services
{
    public class AzureBlobStorageService : IFileStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName;
        private readonly ILogger _logger;

        public AzureBlobStorageService(IConfiguration configuration, ILogger logger)
        {
            _blobServiceClient = new BlobServiceClient(configuration["AzureBlobStorage:ConnectionString"]);
            _containerName = configuration["AzureBlobStorage:ContainerName"];
            _logger = logger.ForContext<AzureBlobStorageService>();

        }

        public async Task<string> UploadFileAsync(string fileName, byte[] fileContent)
        {
            _logger.Information("Uploading file to Azure Blob Storage: {FileName}", fileName);

            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = containerClient.GetBlobClient(fileName);

            using (var memoryStream = new MemoryStream(fileContent))
            {
                await blobClient.UploadAsync(memoryStream, true);
            }

            _logger.Information("File uploaded to Azure Blob Storage: {BlobUri}", blobClient.Uri);

            return blobClient.Uri.ToString();
        }

        public async Task<byte[]> DownloadFileAsync(string path)
        {
            _logger.Information("Downloading file from Azure Blob Storage: {BlobUri}", path);

            var blobClient = new BlobClient(new Uri(path));

            BlobDownloadInfo download = await blobClient.DownloadAsync();

            using (var memoryStream = new MemoryStream())
            {
                await download.Content.CopyToAsync(memoryStream);
                _logger.Information("File downloaded from Azure Blob Storage: {BlobUri}", path);
                return memoryStream.ToArray();
            }
        }

        public async Task DeleteFileAsync(string path)
        {
            _logger.Information("Deleting file from Azure Blob Storage: {BlobUri}", path);

            var blobClient = new BlobClient(new Uri(path));
            await blobClient.DeleteAsync();

            _logger.Information("File deleted from Azure Blob Storage: {BlobUri}", path);
        }
    }

}
