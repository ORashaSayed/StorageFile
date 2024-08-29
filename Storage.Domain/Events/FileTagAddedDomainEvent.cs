using MediatR;
using Storage.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = Storage.Domain.Models.File;
namespace Storage.Domain.Events
{
    public class FileTagAddedDomainEvent : INotification
    {
        public File File { get; }
        public FileTag Tag { get; }

        public FileTagAddedDomainEvent(File file, FileTag tag)
        {
            File = file ?? throw new ArgumentNullException(nameof(file));
            Tag = tag ?? throw new ArgumentNullException(nameof(tag));
        }
    }
}