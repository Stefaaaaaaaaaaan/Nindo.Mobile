using System.Threading.Tasks;
using Nindo.Net.Models;
using Nindo.Net.Models.Enums;

namespace Nindo.Mobile.Services
{
    public interface IApiService
    {
        public Task<Rank[]> GetViewsScoreboardAsync(RankViewsPlatform platform, Size size);

        public Task<Rank[]> GetLikesScoreboardAsync(RankLikesPlatform platform, Size size);

        public Task<Rank[]> GetViewersScoreboardAsync(Size size);

        public Task<Viral[]> GetViralsAsync();
    }
}