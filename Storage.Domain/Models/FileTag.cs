using Storage.Domain.SeedWork;

namespace Storage.Domain.Models
{
    public class FileTag : ValueObject
    {
        public string Name { get; }

        public FileTag(string name)
        {
            Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentNullException(nameof(name));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }
}
