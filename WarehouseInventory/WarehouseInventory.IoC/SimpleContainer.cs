using System;
using System.Collections.Generic;
using System.Linq;

namespace WarehouseInventory.IoC
{
    public class SimpleContainer
    {
        private readonly Dictionary<Type, Type> _registrations = new();

        public void Register<TInterface, TImplementation>() where TImplementation : TInterface
        {
            _registrations[typeof(TInterface)] = typeof(TImplementation);
        }

        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        private object Resolve(Type type)
        {
            if (type.IsInterface)
            {
                if (_registrations.ContainsKey(type))
                {
                    return Resolve(_registrations[type]);
                }
                throw new Exception($"Интерфейс {type.Name} не зарегистрирован!");
            }

            var constructor = type.GetConstructors().FirstOrDefault();
            if (constructor == null)
                throw new Exception($"У класса {type.Name} нет публичного конструктора.");

            var parameters = constructor.GetParameters();
            var args = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                args[i] = Resolve(parameters[i].ParameterType);
            }

            return Activator.CreateInstance(type, args);
        }
    }
}