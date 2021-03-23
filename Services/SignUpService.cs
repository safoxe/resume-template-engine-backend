using MongoDB.Driver;
using engine_plugin_backend.Models;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using engine_plugin_backend.Interfaces;
using Microsoft.Extensions.Configuration;

namespace engine_plugin_backend.Services
{
    public class SignUpService: BackgroundService
    {
        private readonly IMongoCollection<UserModel> _userModel;

        public SignUpService(IBaseSettingsModels settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _userModel = database.GetCollection<UserModel>(settings.CollectionName);
        }

        public string SignUpUser(UserModel user)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(user.Password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            user.Password = System.Text.Encoding.ASCII.GetString(data);
            _userModel.InsertOne(user);
            return user.Id;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) => await Task.Delay(1000, stoppingToken).ConfigureAwait(false);
    }
}