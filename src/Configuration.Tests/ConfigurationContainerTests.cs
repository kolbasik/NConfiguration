using System;
using System.Collections.Specialized;
using System.Configuration;
using Xunit;

namespace kolbasik.NConfiguration.Tests
{
    public sealed class ConfigurationContainerTests
    {
        [Fact]
        public void RegisterDefaults_should_register_app_env_and_app_config()
        {
            // arrange
            var config = new ConfigurationContainer();

            // act
            config.RegisterDefaults();

            // assert
            Assert.NotNull(config.GetRequiredValue<string>("OS"));
            Assert.NotNull(config.GetRequiredValue<string>("USERNAME"));
        }

        [Fact]
        public void GetRequiredValue_should_return_value_if_defined()
        {
            // arrange
            var expected = TimeSpan.FromMinutes(30);
            var config = new ConfigurationContainer();
            config.Register(new NameValueCollection {{"Timeout", expected.ToString()}});

            // act
            var actual = config.GetRequiredValue<TimeSpan>("Timeout");

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetRequiredValue_should_throw_configuration_exception_if_undefined()
        {
            // arrange
            var config = new ConfigurationContainer();

            // act & assert
            Assert.Throws<ConfigurationErrorsException>(() => config.GetRequiredValue<TimeSpan>("Timeout"));
        }

        [Fact]
        public void GetOptionalValue_should_return_value_if_defined()
        {
            // arrange
            var expected = DateTime.Now;
            var config = new ConfigurationContainer();
            config.Register(new NameValueCollection { { "UTC", expected.ToString("O") } });

            // act
            var actual = config.GetOptionalValue<DateTime?>("UTC");

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetOptionalValue_should_return_default_value_if_undefined()
        {
            // arrange
            var expected = DateTime.UtcNow;
            var config = new ConfigurationContainer();

            // act
            var actual = config.GetOptionalValue<DateTime>("UTC", expected);

            // assert
            Assert.Equal(expected, actual);
        }
    }
}