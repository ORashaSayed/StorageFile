namespace Storage.Domain.SeedWork;

public interface IUnitOfWork
{
    Task<bool> SaveChangesAsync();
}
