using Storage.DependencyInjection;
using Storage.DependencyInjection.Registrars;
using System.ComponentModel.Composition;

namespace Storage.Domain.SeedWork
{
    [Export(typeof(IExport))]

    internal class ModuleRegistration : IModule<IModuleRegistrar>
    {
        public void Initialize(IModuleRegistrar registrar)
        {
        }
    }
}
