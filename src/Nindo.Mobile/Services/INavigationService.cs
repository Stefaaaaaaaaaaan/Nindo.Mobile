using System.Threading.Tasks;
using Nindo.Net.Models;

namespace Nindo.Mobile.Services
{
    public interface INavigationService
    {
        Task OpenViralDetailPage(Viral viralEntry);
    }
}