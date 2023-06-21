using System;
using System.Collections.Generic;
namespace Manchy
{
    public class Context:IDisposable
    {
        Context _parent;
        IServiceContainer _container;
        Dictionary<string, object> _kvs;
        bool _isInnerContainer;


        public Context() : this(null, null)
        {
        }

        public Context(IServiceContainer container, Context parent)
        {
            _parent = parent;
            _isInnerContainer = container == null;
            _container = _isInnerContainer ? new ServiceContainer(): container;
            _kvs = new Dictionary<string, object>();
        }
        ~Context()
        {

        }

        public void Dispose()
        {

        }

        public bool Contains(string name,bool cascade)
        {
            if (_kvs.ContainsKey(name))
                return true;
            if(cascade && _parent != null && _parent.Contains(name,cascade))
                return true;
            return false;
        }

        public bool Contains(Type type, bool cascade)
        {
            return Contains(type.Name, cascade);
        }

        public bool Contains<T>(bool cascade)
        {
            return Contains(typeof(T).Name,cascade);
        }

        public void Set(string name,object value)
        {
            _kvs[name] = value;
        }

        public void Set(Type type, object value)
        {
            Set(type.Name, value);
        }

        public void Set<T>(T value)
        {
            Set(typeof(T).Name, value);
        }

        public bool Get<T>(string name,out T value, bool cascade=true)
        {
            if (_kvs.TryGetValue(name, out var v))
            {
                value = (T)v;
                return true;
            }

            if (cascade && _parent != null && _parent.Get(name, out value, cascade))
                return true;

            value = default(T);
            return false;
        }

        public T Get<T>(string name,bool cascade=true)
        {
            Get<T>(name, out var value,cascade);
            return value;
        }

        public object Get(string name, bool cascade = true)
        {
            Get<object>(name, out var value, cascade);
            return value;
        }

        public T Get<T>(Type type, bool cascade = true)
        {
            Get<T>(type.Name, out var value, cascade);
            return value;
        }

        public object Get(Type type, bool cascade = true)
        {
            Get<object>(type.Name, out var value, cascade);
            return value;
        }

        public T Get<T>(bool cascade = true)
        {
            Get<T>(typeof(T).Name, out var value, cascade);
            return value;
        }

        public bool Remove(string name)
        {
            return _kvs.Remove(name);
        }

        public bool Remove(Type type)
        {
            return _kvs.Remove(type.Name);
        }

        public bool Remove<T>()
        {
            return _kvs.Remove(typeof(T).Name);
        }

        public void Clear()
        {
            _kvs.Clear();
        }
    }
}