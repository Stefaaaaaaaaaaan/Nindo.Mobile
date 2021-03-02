using System.Threading.Tasks;
using Nindo.Net;
using Nindo.Net.Models;
using Nindo.Net.Models.Enums;

namespace Nindo.Mobile.Services.Implementations
{
    public class ApiService : IApiService
    {
        private readonly NindoClient _nindoClient;

        public ApiService()
        {
            _nindoClient = new NindoClient();
        }

        public async Task<Rank[]> GetViewsScoreboardAsync(RankViewsPlatform platform, Size size)
        {
            return await _nindoClient.GetViewsScoreboardAsync(platform, size);
        }

        public async Task<Rank[]> GetLikesScoreboardAsync(RankLikesPlatform platform, Size size)
        {
            return await _nindoClient.GetLikesScoreboardAsync(platform, size);
        }

        public async Task<Rank[]> GetViewersScoreboardAsync(Size size)
        {
            return await _nindoClient.GetViewersScoreboardAsync(size);
        }

        public async Task<Viral[]> GetViralsAsync()
        {
            return await _nindoClient.GetViralsAsync();
        }

        public async Task<Milestone[]> GetMilestonesAsync()
        {
            return await _nindoClient.GetMilestonesAsync();
        }

        public async Task<Milestone[]> GetPastMilestonesAsync()
        {
            return await _nindoClient.GetPastMilestonesAsync();
        }
    }
}