using System.Threading.Tasks;
using engine_plugin_backend.Models;

namespace engine_plugin_backend.Interfaces
{
    public interface IWebScrapper
    {
        ScaffoldModel GetParsedData(string url);
    }
}