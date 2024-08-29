namespace Storage.DependencyInjection
{
    public interface IModule<TRegistrar>
        where TRegistrar : class
    {
        void Initialize(TRegistrar registrar);
    }
}
