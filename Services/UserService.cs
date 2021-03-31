using MongoDB.Driver;
using engine_plugin_backend.Models;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;

namespace engine_plugin_backend.Services
{
    public class UserService : BackgroundService
    {
        private readonly IMongoCollection<UserModel> _userModel;

        public UserService(IBaseSettingsModels settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _userModel = database.GetCollection<UserModel>(settings.CollectionName);
        }

        public string SignUpUser(UserModel user)
        {
            if (CheckUserExists(user))
            {
                throw new System.Exception("User already exists");
            }
            // BAD-BAD practice, at least add salt
            // or better: migrate to Okta
            byte[] data = System.Text.Encoding.ASCII.GetBytes(user.Password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            user.Password = System.Text.Encoding.ASCII.GetString(data);
            _userModel.InsertOne(user);
            return user.Id;
        }

        private bool CheckUserExists(UserModel user)
        {
            var foundUser = _userModel.Find<UserModel>((userModel) => user.Email == userModel.Email).FirstOrDefault();
            return foundUser != null;
        }

        public UserModel GetUser(UserModel user)
        {
            var foundUser = _userModel.Find<UserModel>((userModel) => user.Email == userModel.Email).FirstOrDefault();
            byte[] data = System.Text.Encoding.ASCII.GetBytes(user.Password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            string sentPwd = System.Text.Encoding.ASCII.GetString(data);
            return sentPwd == foundUser?.Password ? foundUser : null;
        }

        public UserModel GetUser(string email, string password)
        {
            var foundUser = _userModel.Find<UserModel>((userModel) => email == userModel.Email).FirstOrDefault();
            byte[] data = System.Text.Encoding.ASCII.GetBytes(password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            string sentPwd = System.Text.Encoding.ASCII.GetString(data);
            return sentPwd == foundUser?.Password ? foundUser : null;
        }

        public ClaimsIdentity GetIdentity(string email, string password) {
            var user = GetUser(email, password);
            if(user == null) {
                return null;
            }
            var claim = new List<Claim> {new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email)};
            return new ClaimsIdentity(claim, "Token", ClaimsIdentity.DefaultNameClaimType, null);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) => await Task.Delay(1000, stoppingToken).ConfigureAwait(false);
    }
}