using System;
using Microsoft.EntityFrameworkCore;

namespace Storage.DependencyInjection.Registrars
{
    public interface IDatabaseRegistrar
    {
        void RegisterDbContext<TContext>(Action<DbContextOptionsBuilder, string> options) where TContext : DbContext;
    }
}
