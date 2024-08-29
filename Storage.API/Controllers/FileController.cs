using MediatR;
using Microsoft.AspNetCore.Mvc;
using Storage.Application.Command;
using Storage.Application.Queries;

namespace FileStorageAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is empty.");

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            var command = new UploadFileCommand(file.FileName, memoryStream.ToArray());
            var fileId = await _mediator.Send(command);

            return Ok(new { FileId = fileId });
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetFileDetails(int id)
        {
            var query = new GetFileDetailsQuery(id);
            var fileDetails = await _mediator.Send(query);

            if (fileDetails == null)
                return NotFound();

            return Ok(fileDetails);
        }

        [HttpGet("download/{id:int}")]
        public async Task<IActionResult> DownloadFile(int id)
        {
            var query = new DownloadFileQuery(id);
            var fileContent = await _mediator.Send(query);

            if (fileContent == null)
                return NotFound();

            var fileDetailsQuery = new GetFileDetailsQuery(id);
            var fileDetails = await _mediator.Send(fileDetailsQuery);

            return File(fileContent, fileDetails.ContentType, fileDetails.FileName);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteFile(int id)
        {
            var command = new DeleteFileCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
