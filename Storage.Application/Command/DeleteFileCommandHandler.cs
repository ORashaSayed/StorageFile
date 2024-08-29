using MediatR;
using Serilog;
using Storage.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Application.Command
{
    public class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand, Unit>
    {
        private readonly IFileRepository _fileRepository;
        private readonly IFileStorageService _fileStorageService;
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public DeleteFileCommandHandler(
            IFileRepository fileRepository,
            IFileStorageService fileStorageService,
            IMediator mediator,
            ILogger logger)
        {
            _fileRepository = fileRepository;
            _fileStorageService = fileStorageService;
            _mediator = mediator;
            _logger = logger.ForContext<DeleteFileCommandHandler>();
        }

        public async Task<Unit> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            _logger.Information("Handling delete file command for file ID: {FileId}", request.FileId);

            var file = await _fileRepository.GetByIdAsync(request.FileId);

            if (file == null)
            {
                _logger.Warning("File with ID: {FileId} not found", request.FileId);
                throw new FileNotFoundException("File not found");
            }

            await _fileStorageService.DeleteFileAsync(file.Path);

            await _fileRepository.DeleteAsync(file);

            foreach (var domainEvent in file.DomainEvents)
            {
                await _mediator.Publish(domainEvent, cancellationToken);
            }

            _logger.Information("File deleted successfully with ID: {FileId}", file.Id);

            return Unit.Value;
        }
    }

}
