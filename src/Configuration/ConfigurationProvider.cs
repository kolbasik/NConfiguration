using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace kolbasik.NConfiguration
{
    public abstract class ConfigurationProvider
    {
        public abstract bool TryGetValue(string name, out string value);

        public class NameValue : ConfigurationProvider
        {
            private readonly NameValueCollection source;

            public NameValue(NameValueCollection source)
            {
                if (source == null) throw new ArgumentNullException(nameof(source));
                this.source = source;
            }

            public override bool TryGetValue(string name, out string value)
            {
                for (int i = 0, il = source.Count; i < il; i++)
                {
                    if (string.Equals(name, source.GetKey(i), StringComparison.OrdinalIgnoreCase))
                    {
                        value = source.Get(i);
                        return true;
                    }
                }
                value = default(string);
                return false;
            }
        }

        public class Composite : ConfigurationProvider
        {
            public Composite(params ConfigurationProvider[] configurationProviders)
            {
                ConfigurationProviders = new List<ConfigurationProvider>(configurationProviders);
            }

            public List<ConfigurationProvider> ConfigurationProviders { get; }

            public override bool TryGetValue(string name, out string value)
            {
                foreach (var configurationProvider in ConfigurationProviders)
                {
                    string temp;
                    if (configurationProvider.TryGetValue(name, out temp))
                    {
                        value = temp;
                        return true;
                    }
                }
                value = default(string);
                return false;
            }
        }
    }
}