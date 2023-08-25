using BlazorApplicationInsights.Models;
using System.Threading.Tasks;

namespace BlazorApplicationInsights.Interfaces
{
    public interface IPropertiesPlugin
    {
        Task<TelemetryContext> Context();
    }
}
