using System;
namespace Manchy
{
    public interface IServiceContainer
    {
        void Register(string name,object target);
        void Register(Type type,object target);
        void Register<T>(T target);

        void Register<T>(string name,Func<T> factory);
        void Register<T>(Type type,Func<T> factory);
        void Register<T>(Func<T> factory);

        void Unregister(string name);
        void Unregister(Type type);
        void Unregister<T>();

        object Resolve(string name);
        T Resolve<T>(string name);

        object Resolve(Type type);
        T Resolve<T>(Type type);
        T Resolve<T>();
    }
}