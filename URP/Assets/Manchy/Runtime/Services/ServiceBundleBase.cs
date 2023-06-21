namespace Manchy
{
    public abstract class ServiceBundleBase : IServiceBundle
    {
        IServiceContainer _container;
        public ServiceBundleBase(IServiceContainer container)
        {
            _container = container;
        }
        public void Start()
        {
            OnStart(_container);
        }
        protected abstract void OnStart(IServiceContainer container);
        public void Stop()
        {
            OnStop(_container);
        }
        protected abstract void OnStop(IServiceContainer container);
    }
}