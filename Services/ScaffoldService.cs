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

        public ScaffoldModel AddScaffoldedData(ScaffoldModel data)
        {
            _scaffoldData.InsertOne(data);
            return data;
        }

        public ScaffoldModel GetScaffoldedData(string projectName)
        {
            // TO-DO Add ok search by project name(?)
            var data = _scaffoldData.Find<ScaffoldModel>(data => data.Id == "5ff6d922434c4e763d4abd01").FirstOrDefault();
            return data;
        }
    }
}