using MediatR;

namespace Storage.Application.Command
{
    public class UploadFileCommand : IRequest<int>
    {
        public string FileName { get; }
        public byte[] FileContent { get; }

        public UploadFileCommand(string fileName, byte[] fileContent)
        {
            FileName = fileName;
            FileContent = fileContent;
        }
    }
}
