using System.Reflection;

namespace Backend.DependecyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterAssemblyByConvention(
            this IServiceCollection collection,
            Assembly assembly
        )
        {
            collection.RegisterAllAs<ITransientDependency>(assembly, ServiceLifetime.Transient);
            collection.RegisterAllAs<IScopedDependency>(assembly, ServiceLifetime.Scoped);
            collection.RegisterAllAs<ISingletonDependency>(assembly, ServiceLifetime.Singleton);

            return collection;
        }

        private static void RegisterAllAs<T>(
            this IServiceCollection collection,
            Assembly assembly,
            ServiceLifetime serviceLifetime
        )
        {
            var dependencies = assembly
                .GetTypes()
                .Where(IsBasedOn<T>)
                .Where(type => !type.IsAbstract)
                .Where(type => !type.GetTypeInfo().IsGenericTypeDefinition)
                .ToArray();

            foreach (var dependency in dependencies)
            {
                collection.RegisterAs(dependency, dependency, serviceLifetime);

                foreach (var dependencyInterface in dependency.GetInterfaces())
                {
                    collection.RegisterAs(dependencyInterface, dependency, serviceLifetime);
                }
            }
        }

        private static void RegisterAs(
            this IServiceCollection collection,
            Type serviceType,
            Type implementationType,
            ServiceLifetime serviceLifetime
        )
        {
            collection.Add(
                ServiceDescriptor.Describe(serviceType, implementationType, serviceLifetime)
            );
        }

        private static bool IsBasedOn<TBaseType>(Type potentialBase)
        {
            var type = typeof(TBaseType);

            if (potentialBase == type)
            {
                return false;
            }

            if (type.GetTypeInfo().IsAssignableFrom(potentialBase))
            {
                return true;
            }
            else if (potentialBase.GetTypeInfo().IsGenericTypeDefinition)
            {
                if (
                    potentialBase.GetTypeInfo().IsInterface
                    && IsBasedOnGenericInterface<TBaseType>(potentialBase)
                )
                {
                    return true;
                }

                if (IsBasedOnGenericClass<TBaseType>(potentialBase))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsBasedOnGenericClass<TBaseType>(Type potentialBase)
        {
            var type = typeof(TBaseType);

            for (; type != null; type = type.GetTypeInfo().BaseType)
            {
                if (
                    type.GetTypeInfo().IsGenericType
                    && type.GetGenericTypeDefinition() == potentialBase
                )
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsBasedOnGenericInterface<TBaseType>(Type potentialBase)
        {
            var type = typeof(TBaseType);

            foreach (var interfaceType in type.GetInterfaces())
            {
                if (
                    interfaceType.GetTypeInfo().IsGenericType
                    && interfaceType.GetGenericTypeDefinition() == potentialBase
                )
                {
                    return true;
                }
            }

            return false;
        }
    }
}
