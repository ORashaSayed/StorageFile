using Storage.DependencyInjection.Registrars;
using System;
using System.Collections.Generic;

namespace Storage.Api.Registrars
{
    public class AutoMapperProfilesRegistrar : IAutoMapperProfilesRegistrar
    {
        public List<Type> ProfileTypes { get; } = new List<Type>();
        public void Add(Type type)
        {
            ProfileTypes.Add(type);
        }
    }
}
