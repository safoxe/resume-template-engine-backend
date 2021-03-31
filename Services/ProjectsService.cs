using MongoDB.Driver;
using engine_plugin_backend.Models;
using System.Collections.Generic;

namespace engine_plugin_backend.Services
{
    public class ProjectsService
    {
        private readonly IMongoCollection<UserModel> _userModel;
        private readonly IMongoCollection<ProjectModel> _projectModel;

        public ProjectsService(IBaseSettingsModels settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _userModel = database.GetCollection<UserModel>(settings.CollectionName);
            _projectModel = database.GetCollection<ProjectModel>(settings.ProjectsCollection);
        }

        public List<ProjectModel> GetAllProjects(string userName)
        {
            var foundUser = _userModel.Find<UserModel>((userModel) => userName == userModel.Email).FirstOrDefault();
            var foundProjects = _projectModel.Find<ProjectModel>((project) => project.UserId == foundUser.Id).ToList<ProjectModel>();
            return foundProjects;
        }

        public void CreateProject(string userName, ProjectModel project)
        {
            var foundUser = _userModel.Find<UserModel>((userModel) => userName == userModel.Email).FirstOrDefault();
            project.UserId = foundUser.Id;
            _projectModel.InsertOne(project);
        }
    }
}