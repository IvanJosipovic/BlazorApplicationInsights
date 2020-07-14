using System.Threading.Tasks;

namespace BlazorApplicationInsights
{
    public interface IApplicationInsights
    {
        Task TrackEvent(string name);
        Task TrackTrace(string message);
        Task TrackPageView();
    }
}