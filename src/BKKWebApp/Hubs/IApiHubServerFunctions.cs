using System.Threading.Tasks;

namespace BKKWebApp.Hubs
{
    public interface IApiHubServerFunctions
    {
        Task GetArrivalsAndDeparturesForLocation(float lat, float lng);
        Task GetStopsForLocation(float lat, float lng);
        Task GetArrivalsAndDeparturesForStop(string stopId);
    }
}