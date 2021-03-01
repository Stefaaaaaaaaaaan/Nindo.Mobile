using System;
using System.Globalization;
using FluentAssertions;
using Nindo.Mobile.Converters;
using NUnit.Framework;

namespace Nindo.Mobile.Tests.Converters
{
    [TestFixture]
    public class MetricConverterTests
    {
        [TestCase(1433979L, "1.4M")]
        [TestCase(1394552L, "1.4M")]
        [TestCase(1267407L, "1.3M")]
        [TestCase(489640L, "489.6k")]
        [TestCase(344647L, "344.6k")]
        [TestCase(334830L, "334.8k")]
        [TestCase(542133L, "542.1k")]
        [TestCase(283650L, "283.7k")]
        [TestCase(264567L, "264.6k")]
        [TestCase(9793L, "9.8k")]
        [TestCase(9664L, "9.7k")]
        [TestCase(8827L, "8.8k")]
        [TestCase(67126L, "67.1k")]
        [TestCase(56806L, "56.8k")]
        [TestCase(30171L, "30.2k")]
        public void Convert_CorrectResult(long value, string result)
        {
            // Arrange
            var converter = new MetricConverter();

            // Act
            var convertedValue = converter.Convert(value, null, null, CultureInfo.CurrentCulture);

            // Assert
            convertedValue.Should().Be(result);
        }

        [TestCase("1.4M", 1400000.0D)]
        [TestCase("1.4M", 1400000.0D)]
        [TestCase("1.3M", 1300000.0D)]
        [TestCase("489.6k", 489600.0D)]
        [TestCase("344.6k", 344600.0D)]
        [TestCase("334.8k", 334800.0D)]
        [TestCase("542.1k", 542100.0D)]
        [TestCase("283.7k", 283700.0D)]
        [TestCase("264.6k", 264600.0D)]
        [TestCase("9.8k", 9800.0D)]
        [TestCase("9.7k", 9700.0D)]
        [TestCase("8.8k", 8800.0D)]
        [TestCase("67.1k", 67100.0D)]
        [TestCase("56.8k", 56800.0D)]
        [TestCase("30.2k", 30200.0D)]
        public void ConvertBack_CorrectResult(string value, double result)
        {
            // Arrange
            var converter = new MetricConverter();

            // Act
            var convertedValue = converter.ConvertBack(value, null, null, CultureInfo.CurrentCulture);

            // Assert
            convertedValue.Should().Be(result);
        }

        [TestCase("string")]
        [TestCase(123)]
        [TestCase(123.32)]
        [TestCase(123.0D)]
        [TestCase(-34464.7)]
        [TestCase(33483.0)]
        [TestCase(-1)]
        public void Convert_InvalidCast(object value)
        {
            // Arrange
            var converter = new MetricConverter();

            Assert.Throws<InvalidCastException>(() => converter.Convert(value, null, null, CultureInfo.CurrentCulture));
        }

        [TestCase(1.4)]
        [TestCase(334.8)]
        [TestCase(123121L)]
        public void ConvertBack_InvalidCast(object value)
        {
            // Arrange
            var converter = new MetricConverter();

            Assert.Throws<InvalidCastException>(() => converter.ConvertBack(value, null, null, CultureInfo.CurrentCulture));
        }
    }
}