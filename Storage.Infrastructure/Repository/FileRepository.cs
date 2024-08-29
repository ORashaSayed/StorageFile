using Microsoft.EntityFrameworkCore;
using Serilog;
using Storage.Domain.Interfaces;
using File = Storage.Domain.Models.File;
namespace Storage.Infrastructure.Repository
{
    public class FileRepository : IFileRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public FileRepository(ApplicationDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger.ForContext<FileRepository>();

        }

        public async Task<File> AddAsync(File file)
        {
            _logger.Information("Adding file metadata to the database with ID: {FileId}", file.Id);

            await _context.Files.AddAsync(file);
            await _context.SaveChangesAsync();

            _logger.Information("File metadata added to the database with ID: {FileId}", file.Id);

            return file;
        }

        public async Task<File> GetByIdAsync(int id)
        {
            _logger.Information("Retrieving file metadata from the database with ID: {FileId}", id);

            var file = await _context.Files
                .Include(f => f.Tags) // Including tags if any
                .SingleOrDefaultAsync(f => f.Id == id);

            if (file == null)
            {
                _logger.Error("File with ID: {FileId} not found in the database", id);
            }

            return file;
        }

        public async Task UpdateAsync(File file)
        {
            _logger.Information("Updating file metadata in the database with ID: {FileId}", file.Id);

            _context.Files.Update(file);
            await _context.SaveChangesAsync();

            _logger.Information("File metadata updated in the database with ID: {FileId}", file.Id);
        }

        public async Task DeleteAsync(File file)
        {
            _logger.Information("Deleting file metadata from the database with ID: {FileId}", file.Id);

            _context.Files.Remove(file);
            await _context.SaveChangesAsync();

            _logger.Information("File metadata deleted from the database with ID: {FileId}", file.Id);
        }
    }


}
