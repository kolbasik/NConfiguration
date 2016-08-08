using System;
using System.Configuration;

namespace kolbasik.NConfiguration
{
    public sealed class ConfigurationContainer : ConfigurationProvider.Composite
    {
        public ConfigurationContainer Register(ConfigurationProvider configurationProvider)
        {
            if (configurationProvider == null) throw new ArgumentNullException(nameof(configurationProvider));
            ConfigurationProviders.Add(configurationProvider);
            return this;
        }

        public T GetRequiredValue<T>(string name)
        {
            string value;
            if (TryGetValue(name, out value))
            {
                return ConfigurationConverter.ConvertFrom<T>(value);
            }
            throw new ConfigurationErrorsException($"The '{name}' is required.");
        }

        public T GetOptionalValue<T>(string name, T defaultValue = default(T))
        {
            string value;
            if (TryGetValue(name, out value))
            {
                return ConfigurationConverter.ConvertFrom<T>(value);
            }
            return defaultValue;
        }
    }
}