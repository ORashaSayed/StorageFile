using MediatR;
using Serilog;
using Storage.Application.Dtos;
using Storage.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Application.Queries
{
    public class GetFileDetailsQueryHandler : IRequestHandler<GetFileDetailsQuery, FileDto>
    {
        private readonly IFileRepository _fileRepository;
        private readonly ILogger _logger;

        public GetFileDetailsQueryHandler(IFileRepository fileRepository, ILogger logger)
        {
            _fileRepository = fileRepository;
            _logger = logger.ForContext<GetFileDetailsQueryHandler>();
        }

        public async Task<FileDto> Handle(GetFileDetailsQuery request, CancellationToken cancellationToken)
        {
            _logger.Information("Handling get file details query for file ID: {FileId}", request.FileId);

            var file = await _fileRepository.GetByIdAsync(request.FileId);

            if (file == null)
            {
                _logger.Error("File with ID: {FileId} not found", request.FileId);
                throw new FileNotFoundException("File not found");
            }

            return new FileDto
            {
                Id = file.Id,
                FileName = file.FileName,
                ContentType = file.ContentType,
                Size = file.Size,
                UploadedAt = file.UploadedAt,
                Path = file.Path
            };
        }
    }

}
