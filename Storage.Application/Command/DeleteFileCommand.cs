using MediatR;

namespace Storage.Application.Command
{
    public class DeleteFileCommand : IRequest<Unit>
    {
        public int FileId { get; }

        public DeleteFileCommand(int fileId)
        {
            FileId = fileId;
        }
    }

}
