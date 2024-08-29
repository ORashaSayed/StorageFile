using File = Storage.Domain.Models.File;
namespace Storage.Domain.Interfaces
{
    public interface IFileRepository
    {
        public Task<File> AddAsync(File file);
        public Task<File> GetByIdAsync(int id);
        public Task DeleteAsync(File file);
        public Task UpdateAsync(File file);
    }
}
