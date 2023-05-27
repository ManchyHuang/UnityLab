using System;
using System.Collections.Generic;
namespace Manchy
{
    public interface IFactory:IDisposable
    {
        bool isSingleton { get; }
        object Create();
    }

    public class SingletonFactory : IFactory
    {
        object _target;
        bool _disposed;
        public SingletonFactory(object target)
        {
            _target = target;
        }
        ~SingletonFactory()
        {
            if (!_disposed)
            {
                _disposed = true;
                Dispose(false);
            }
        }

        public bool isSingleton => true;
        public object Create() => _target;
        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                GC.SuppressFinalize(this);
                Dispose(true);
            }
        }

        protected void Dispose(bool disposing)
        {
            if (!disposing)
                return;

            if (_target is IDisposable disposable)
                disposable.Dispose();
            _target = null;
        }
    }

    public class GenericFactory<T>: IFactory
    {
        Func<T> _func;
        public GenericFactory(Func<T> func)
        {
            _func = func;
        }

        public bool isSingleton => false;
        public object Create() => _func();
        public void Dispose()
        {
        }
    }

    public class ServiceContainer : IServiceContainer, IDisposable
    {
        Dictionary<string, IFactory> _services = new Dictionary<string, IFactory>();
        bool _disposed;
        ~ServiceContainer()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            _disposed = true;
            if (!disposing)
                return;

            foreach (var kv in _services)
                kv.Value.Dispose();
            _services.Clear();
        }

        public virtual void Register(string name, object target)
        {
            if (_services.ContainsKey(name))
                throw new DuplicateRegisterServiceException($"Service with key {name} already exists.");
            _services.Add(name, new SingletonFactory(target));
        }

        public virtual void Register(Type type, object target)
        {
            Register(type.Name, target);
        }

        public virtual void Register<T>(T target)
        {
            Register(typeof(T).Name, target);
        }

        public virtual void Register<T>(string name, Func<T> factory)
        {
            if (_services.ContainsKey(name))
                return;
            _services.Add(name, new GenericFactory<T>(factory));
        }

        public virtual void Register<T>(Type type, Func<T> factory)
        {
            Register(type.Name, factory);
        }

        public virtual void Register<T>(Func<T> factory)
        {
            Register(typeof(T).Name, factory);
        }

        public virtual void Unregister(string name)
        {
            if (!_services.Remove(name, out var factory))
                return;
            factory.Dispose();
        }

        public virtual void Unregister(Type type)
        {
            this.Unregister(type.Name);
        }

        public virtual void Unregister<T>()
        {
            this.Unregister(typeof(T).Name);
        }

        public virtual object Resolve(string name)
        {
            return Resolve<object>(name);
        }
        public virtual T Resolve<T>(string name)
        {
            if (!_services.TryGetValue(name, out var factory))
                return default(T);
            return (T)factory.Create();
        }

        public virtual object Resolve(Type type)
        {
            return Resolve<object>(type.Name);
        }
        public virtual T Resolve<T>(Type type)
        {
            return Resolve<T>(type.Name);
        }
        public virtual T Resolve<T>()
        {
            return Resolve<T>(typeof(T).Name);
        }

    }
}