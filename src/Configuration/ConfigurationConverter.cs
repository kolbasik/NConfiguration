using System;
using System.ComponentModel;

namespace kolbasik.NConfiguration
{
    public static class ConfigurationConverter
    {
        public static T ConvertFrom<T>(string text)
        {
            return ConvertFrom<T>(text, typeof (T));
        }

        public static T ConvertFrom<T>(string text, Type type)
        {
            if (!typeof(T).IsAssignableFrom(type))
            {
                var message = $"The '{typeof(T).FullName}' is not assignable from '{type.FullName}'.";
                throw new InvalidCastException(message);
            }
            return ConvertFrom<T>(text, TypeDescriptor.GetConverter(type));
        }

        public static T ConvertFrom<T>(string text, TypeConverter converter)
        {
            if (converter == null) throw new ArgumentNullException(nameof(converter));
            try
            {
                return (T)converter.ConvertFromInvariantString(text);
            }
            catch (Exception ex)
            {
                var message = $"Cannot convert {typeof(T).FullName} from 'string' using {converter.GetType().FullName} converter.";
                throw new InvalidCastException(message, ex);
            }
        }
    }
}