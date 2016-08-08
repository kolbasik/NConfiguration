using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;

namespace kolbasik.NConfiguration
{
    public static class ConfigurationContainerExtensions
    {
        public static ConfigurationContainer RegisterDefaults(this ConfigurationContainer configurationContainer)
        {
            return configurationContainer.RegisterAppEnv().RegisterAppConfig();
        }

        public static ConfigurationContainer RegisterAppConfig(this ConfigurationContainer configurationContainer)
        {
            return configurationContainer.Register(ConfigurationManager.AppSettings);
        }

        public static ConfigurationContainer RegisterAppEnv(this ConfigurationContainer configurationContainer,
            EnvironmentVariableTarget environmentVariableTarget = EnvironmentVariableTarget.Process)
        {
            var source = new NameValueCollection();
            var environmentVariables = Environment.GetEnvironmentVariables(environmentVariableTarget);
            foreach (DictionaryEntry environmentVariable in environmentVariables)
            {
                source.Set(Convert.ToString(environmentVariable.Key), Convert.ToString(environmentVariable.Value));
            }
            return configurationContainer.Register(source);
        }

        public static ConfigurationContainer Register(this ConfigurationContainer configurationContainer,
            NameValueCollection source)
        {
            return configurationContainer.Register(new ConfigurationProvider.NameValue(source));
        }
    }
}