namespace Storage.Infrastructure
{
    public interface IDatabaseMigrator
    {
        void Migrate();
    }
}