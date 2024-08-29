using Microsoft.EntityFrameworkCore;

namespace Storage.DependencyInjection.Registrars
{
    public interface IIdentityRegistrar
    {
        void AddIdentity<TUser, TRole, TContext>()
            where TUser : class
            where TRole : class
            where TContext : DbContext;
    }
}
