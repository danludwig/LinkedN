using System;
using System.Collections.Generic;
using System.Linq;

namespace LinkedN
{
    /// <summary>
    /// This type is responsible for providing limited type resolution services in the absence of an inversion of control container.
    /// </summary>
    public class BruteForceLinkedInClientServiceProvider : IServiceProvider
    {
        private readonly DefaultLinkedInClient _client;

        public BruteForceLinkedInClientServiceProvider(DefaultLinkedInClient client)
        {
            _client = client;
        }

        public object GetService(Type serviceType)
        {
            // scan assembly
            var definedTypes = GetType().Assembly.GetTypes();
            foreach (var definedType in definedTypes)
            {
                if (!definedType.IsAssignableFrom(serviceType)) continue;

                //if (!definedType.ImplementedInterfaces.Contains(serviceType )) continue;

                var constructors = definedType.GetConstructors().ToArray();
                if (constructors.Length != 1)
                    throw new NotSupportedException(string.Format(
                        "Construction of type '{0}' requires a single constructor.",
                            definedType));
                var constructor = constructors.Single();

                var parameters = constructor.GetParameters().ToArray();
                var paramValues = new List<object>();
                if (parameters.Any())
                {
                    foreach (var parameter in parameters)
                    {
                        if (parameter.ParameterType.IsInstanceOfType(_client.Secrets))
                            paramValues.Add(_client.Secrets);

                        else if (parameter.ParameterType.IsInstanceOfType(_client.Tokens))
                            paramValues.Add(_client.Tokens);

                        else if (parameter.ParameterType.IsInstanceOfType(_client.ServiceProvider))
                            paramValues.Add(_client.ServiceProvider);

                        else throw new InvalidOperationException(string.Format(
                            "Unable to determine instance of '{0}' to use as constructor argument for '{1}'.",
                            parameter.ParameterType, definedType));
                    }
                }

                var instance = constructor.Invoke(paramValues.ToArray());
                return instance;
            }

            throw new NotSupportedException(string.Format(
                "The Resource '{0}' is currently not supported. Please fork LinkedN on github to support.",
                    serviceType));
        }
    }
}
