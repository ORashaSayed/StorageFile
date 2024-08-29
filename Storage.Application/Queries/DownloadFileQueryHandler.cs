using MediatR;
using Serilog;
using Storage.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Application.Queries
{
    public class DownloadFileQueryHandler : IRequestHandler<DownloadFileQuery, byte[]>
    {
        private readonly IFileRepository _fileRepository;
        private readonly IFileStorageService _fileStorageService;
        private readonly ILogger _logger;

        public DownloadFileQueryHandler(IFileRepository fileRepository, IFileStorageService fileStorageService, ILogger logger)
        {
            _fileRepository = fileRepository;
            _fileStorageService = fileStorageService;
            _logger = logger.ForContext<DownloadFileQueryHandler>();
        }

        public async Task<byte[]> Handle(DownloadFileQuery request, CancellationToken cancellationToken)
        {
            _logger.Information("Handling download file query for file ID: {FileId}", request.FileId);

            var file = await _fileRepository.GetByIdAsync(request.FileId);

            if (file == null)
            {
                _logger.Error("File with ID: {FileId} not found", request.FileId);
                throw new FileNotFoundException("File not found");
            }

            var fileContent = await _fileStorageService.DownloadFileAsync(file.Path);

            _logger.Information("File downloaded successfully with ID: {FileId}", file.Id);

            return fileContent;
        }
    }

}
