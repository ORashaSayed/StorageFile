using Storage.Domain.Events;
using Storage.Domain.SeedWork;
using System.ComponentModel.DataAnnotations;

namespace Storage.Domain.Models
{
    public class File : Entity, IAggregateRoot
    {
        [Required]
        public string FileName { get; private set; }

        [Required]
        public string ContentType { get; private set; }

        [Required]
        public long Size { get; private set; }

        [Required]
        public string Path { get; private set; }

        [Required]
        public DateTime UploadedAt { get; private set; }

        private List<FileTag> _tags;
        public IReadOnlyCollection<FileTag> Tags => _tags.AsReadOnly();

        protected File()
        {
            _tags = new List<FileTag>();
        }

        public File(string fileName, string contentType, long size, string path) : this()
        {
            FileName = !string.IsNullOrWhiteSpace(fileName) ? fileName : throw new ArgumentNullException(nameof(fileName));
            ContentType = !string.IsNullOrWhiteSpace(contentType) ? contentType : throw new ArgumentNullException(nameof(contentType));
            Size = size > 0 ? size : throw new ArgumentOutOfRangeException(nameof(size));
            Path = !string.IsNullOrWhiteSpace(path) ? path : throw new ArgumentNullException(nameof(path));
            UploadedAt = DateTime.UtcNow;
        }

        public void UpdateFilePath(string newPath)
        {
            if (string.IsNullOrWhiteSpace(newPath))
                throw new ArgumentNullException(nameof(newPath));

            Path = newPath;
            AddDomainEvent(new FilePathUpdatedDomainEvent(this));
        }

        public void AddTag(string tagName)
        {
            if (string.IsNullOrWhiteSpace(tagName))
                throw new ArgumentNullException(nameof(tagName));

            var existingTag = _tags.SingleOrDefault(t => t.Name.Equals(tagName, StringComparison.OrdinalIgnoreCase));

            if (existingTag == null)
            {
                var tag = new FileTag(tagName);
                _tags.Add(tag);
                AddDomainEvent(new FileTagAddedDomainEvent(this, tag));
            }
        }

        public void RemoveTag(string tagName)
        {
            var tag = _tags.SingleOrDefault(t => t.Name.Equals(tagName, StringComparison.OrdinalIgnoreCase));

            if (tag != null)
            {
                _tags.Remove(tag);
                AddDomainEvent(new FileTagRemovedDomainEvent(this, tag));
            }
        }

    }
}