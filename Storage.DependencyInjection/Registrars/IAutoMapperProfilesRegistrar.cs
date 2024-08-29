using System;

namespace Storage.DependencyInjection.Registrars
{
    public interface IAutoMapperProfilesRegistrar
    {
        void Add(Type type);
    }
}
