

using Microsoft.EntityFrameworkCore;
using Storage.DependencyInjection;
using Storage.DependencyInjection.Registrars;
using Storage.Domain.Interfaces;
using Storage.Domain.SeedWork;
using Storage.Infrastructure.Repository;
using System.ComponentModel.Composition;

namespace Storage.Infrastructure
{
    [Export(typeof(IExport))]
    public class ModuleRegistration : IModule<IModuleRegistrar>,IModule<IDatabaseRegistrar>
    {
        public void Initialize(IModuleRegistrar registrar)
        {
            registrar.Register<IFileRepository,FileRepository>(Lifetime.Scoped);
            registrar.Register<IUnitOfWork, UnitOfWork>(Lifetime.Scoped);
            registrar.Register<IFileStorageService, IFileStorageService>(Lifetime.Scoped);
            registrar.Register<IDatabaseMigrator, DatabaseMigrator>(Lifetime.Scoped);

        }

        public void Initialize(IDatabaseRegistrar databaseRegistrar)
        {
            databaseRegistrar.RegisterDbContext<ApplicationDbContext>((options, connectionString) =>
            {
                options.UseSqlServer(connectionString,
                    builder => builder.MigrationsAssembly(typeof(ModuleRegistration).Assembly.FullName));
            });
        }
       
    }
}
