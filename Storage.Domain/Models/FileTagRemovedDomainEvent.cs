using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Domain.Models
{
    public class FileTagRemovedDomainEvent : INotification
    {
        public File File { get; }
        public FileTag Tag { get; }

        public FileTagRemovedDomainEvent(File file, FileTag tag)
        {
            File = file ?? throw new ArgumentNullException(nameof(file));
            Tag = tag ?? throw new ArgumentNullException(nameof(tag));
        }
    }

}
