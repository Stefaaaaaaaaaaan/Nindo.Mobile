using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Moq;
using Nindo.Mobile.Models;
using Nindo.Mobile.Services;
using Nindo.Mobile.ViewModels;
using Nindo.Net.Models;
using NUnit.Framework;

namespace Nindo.Mobile.Tests.ViewModels
{
    [TestFixture]
    public class ViralViewModelTests
    {
        [TestCase(ViralTypes.TwitterLikes)]
        [TestCase(ViralTypes.TwitterRetweets)]
        [TestCase(ViralTypes.TiktokLikes)]
        [TestCase(ViralTypes.TiktokComments)]
        [TestCase(ViralTypes.TiktokViews)]
        [TestCase(ViralTypes.YoutubeLikes)]
        [TestCase(ViralTypes.YoutubeComments)]
        [TestCase(ViralTypes.YoutubeViews)]
        [TestCase(ViralTypes.InstagramLikes)]
        [TestCase(ViralTypes.InstagramComments)]
        public void OpenDetailPage_EnumProvided_NavigateToDetailPage(ViralTypes type)
        {
            // Arrange
            var sut = new ViralViewModel();
            var navigation = Mock.Of<INavigationService>();

            // Act
            sut.OpenDetailPageCommand.ExecuteAsync(type);

            // Assert
            Mock.Get(navigation).Verify(m => m.OpenViralDetailPage(It.IsAny<Viral>()), Times.Once);
        }
    }
}