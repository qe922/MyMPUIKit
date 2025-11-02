using System;
using System.Collections.Generic;

namespace EasyUIFramework
{
    /// <summary>
    /// 简单的DI容器
    /// </summary>
    public class UIServiceContainer
    {
        private static UIServiceContainer _instance;
        public static UIServiceContainer Instance => _instance ??= new UIServiceContainer();

        private Dictionary<Type, object> _services = new Dictionary<Type, object>();

        public void Register<TInterface, TImplementation>() where TImplementation : TInterface, new()
        {
            _services[typeof(TInterface)] = new TImplementation();
        }

        public void Register<TInterface>(TInterface instance)
        {
            _services[typeof(TInterface)] = instance;
        }

        public T Resolve<T>() where T : class
        {
            if (_services.TryGetValue(typeof(T), out object service))
            {
                return service as T;
            }
            throw new Exception($"未找到服务{typeof(T).Name}");
        }

        public bool TryResolve<T>(out T service) where T : class
        {
            if (_services.TryGetValue(typeof(T), out object obj))
            {
                service = obj as T;
                return true;
            }
            service = null;
            return false;
        }
    }
}