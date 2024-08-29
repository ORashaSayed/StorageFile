using MediatR;
using Serilog;
using Storage.Domain.Interfaces;
using File = Storage.Domain.Models.File;
namespace Storage.Application.Command
{
    public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, int>
    {
        private readonly IFileRepository _fileRepository;
        private readonly IFileStorageService _fileStorageService;
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public UploadFileCommandHandler(
            IFileRepository fileRepository,
            IFileStorageService fileStorageService,
            IMediator mediator,
            ILogger logger)
        {
            _fileRepository = fileRepository;
            _fileStorageService = fileStorageService;
            _mediator = mediator;
            _logger = logger.ForContext<UploadFileCommandHandler>();
        }

        public async Task<int> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            _logger.Information("Handling upload file command for file: {FileName}", request.FileName);

            var path = await _fileStorageService.UploadFileAsync(request.FileName, request.FileContent);

            var file = new File(request.FileName, GetContentType(request.FileName), request.FileContent.Length, path);

            await _fileRepository.AddAsync(file);

            await _mediator.Publish(file.DomainEvents);

            _logger.Information("File uploaded successfully with ID: {FileId}", file.Id);

            return file.Id;
        }

        private string GetContentType(string fileName)
        {
            _logger.Information("Resolving content type for file: {FileName}", fileName);
            // Logic to determine the content type based on the file extension
            return "application/octet-stream"; // Default MIME type
        }
    }

}
