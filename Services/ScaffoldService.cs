using MongoDB.Driver;
using engine_plugin_backend.Models;

namespace engine_plugin_backend.Services
{
    public class ScaffoldService
    {
        private readonly IMongoCollection<ScaffoldModel> _scaffoldData;

        public ScaffoldService(IBaseSettingsModels settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _scaffoldData = database.GetCollection<ScaffoldModel>(settings.CollectionName);
        }

        public string AddScaffoldedData(ScaffoldModel data)
        {
            _scaffoldData.InsertOne(data);
            return data.Id;
        }

        public ScaffoldModel GetScaffoldedData(string id)
        {
            return _scaffoldData.Find<ScaffoldModel>(data => data.Id == id).FirstOrDefault();
        }
    }
}