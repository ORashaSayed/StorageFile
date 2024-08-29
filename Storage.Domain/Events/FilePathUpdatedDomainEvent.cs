using MediatR;
namespace Storage.Domain.Events
{
    public class FilePathUpdatedDomainEvent : INotification
    {
        public Models.File File { get; }

        public FilePathUpdatedDomainEvent(Models.File file)
        {
            File = file ?? throw new ArgumentNullException(nameof(file));
        }
    }
}