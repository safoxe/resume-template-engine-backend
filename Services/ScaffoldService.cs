using MongoDB.Driver;
using engine_plugin_backend.Models;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using engine_plugin_backend.Interfaces;
using Microsoft.Extensions.Configuration;

namespace engine_plugin_backend.Services
{
    public class ScaffoldService: BackgroundService
    {
        private readonly IMongoCollection<ScaffoldModel> _scaffoldData;
        private readonly IWebScrapper _webScrapper;

        private readonly IConfiguration _configuration;

        public ScaffoldService(IBaseSettingsModels settings, IWebScrapper webScrapper, IConfiguration configuration)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _scaffoldData = database.GetCollection<ScaffoldModel>(settings.CollectionName);
            _webScrapper = webScrapper;
            _configuration = configuration;
        }

        public string AddScaffoldedData(ScaffoldModel data)
        {
            var resume = _webScrapper.GetParsedData(_configuration.GetValue<string>("CompanyMockSite"));
            resume.PositionType = data.PositionType;
            resume.SeniorityLevel = data.SeniorityLevel;
            _scaffoldData.InsertOne(resume);
            return resume.Id;
        }

        public ScaffoldModel GetScaffoldedData(string id)
        {
            return _scaffoldData.Find(data => data.Id == id).FirstOrDefault();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) => await Task.Delay(1000, stoppingToken).ConfigureAwait(false);
    }
}