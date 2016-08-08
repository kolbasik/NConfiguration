using System;
using System.ComponentModel;
using Ploeh.AutoFixture;
using Xunit;

namespace kolbasik.NConfiguration.Tests
{
    public sealed class ConfigurationConverterTests
    {
        private Fixture fixture = new Fixture();

        [Fact]
        public void ConvertFrom_should_convert_value()
        {
            // arrange
            var expected = fixture.Create<int>();

            // act
            var actual = ConfigurationConverter.ConvertFrom<int>(expected.ToString());

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ConvertFrom_should_convert_value_using_type()
        {
            // arrange
            var expected = fixture.Create<int>();
            var type = typeof(int);

            // act
            var actual = ConfigurationConverter.ConvertFrom<IConvertible>(expected.ToString(), type);

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ConvertFrom_should_convert_value_using_converter()
        {
            // arrange
            var expected = fixture.Create<int>();
            var converter = TypeDescriptor.GetConverter(typeof(int));

            // act
            var actual = ConfigurationConverter.ConvertFrom<IConvertible>(expected.ToString(), converter);

            // assert
            Assert.Equal(expected, actual);
        }
    }
}