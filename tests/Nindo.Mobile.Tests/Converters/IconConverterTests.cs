using System;
using System.Globalization;
using FluentAssertions;
using Nindo.Mobile.Converters;
using NUnit.Framework;

namespace Nindo.Mobile.Tests.Converters
{
    [TestFixture]
    public class IconConverterTests
    {
        [TestCase("twitch", "twitchGrey.png")]
        [TestCase("instagram", "instagramGrey.png")]
        [TestCase("twitter", "twitterGrey.png")]
        [TestCase("tiktok", "tiktokGrey.png")]
        [TestCase("youtube", "youtubeGrey.png")]
        [TestCase("blupblap", "youtubeGrey.png")]
        public void Convert_CorrectResult(string platform, string imageLocation)
        {
            // Arrange
            var converter = new IconConverter();

            // Act
            var convertedValue = converter.Convert(platform, null, null, CultureInfo.CurrentCulture);

            // Assert
            convertedValue.Should().Be(imageLocation);
        }

        [Test]
        public void ConvertBack_NotImplemented()
        {
            // Arrange
            var converter = new IconConverter();

            Assert.Throws<NotImplementedException>(() =>
                converter.ConvertBack(1, null, null, CultureInfo.CurrentCulture));
        }

        [TestCase(123123)]
        [TestCase(678.83)]
        [TestCase(-1243.561)]
        [TestCase(-156756)]
        public void Convert_InvalidCast(object platform)
        {
            // Arrange
            var converter = new IconConverter();

            Assert.Throws<InvalidCastException>(() =>
                converter.Convert(platform, null, null, CultureInfo.CurrentCulture));
        }
    }
}