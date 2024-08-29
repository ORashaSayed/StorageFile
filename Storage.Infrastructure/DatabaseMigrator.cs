using Microsoft.EntityFrameworkCore;

namespace Storage.Infrastructure
{
    internal class DatabaseMigrator : IDatabaseMigrator
    {
        private readonly ApplicationDbContext _dbContext;
        public DatabaseMigrator(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Migrate()
        {
            _dbContext.Database.Migrate();
        }
    }
}
