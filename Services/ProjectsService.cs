using MongoDB.Driver;
using engine_plugin_backend.Models;
using System.Collections.Generic;
using MongoDB.Bson;

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

        public string CreateProject(string userName, ProjectModel project)
        {
            var foundUser = _userModel.Find<UserModel>((userModel) => userName == userModel.Email).FirstOrDefault();
            project.UserId = foundUser.Id;
            _projectModel.InsertOne(project);
            return project.Id;
        }

        public ProjectModel GetProject(string userName, string projectId)
        {
            var foundUser = _userModel.Find<UserModel>((userModel) => userName == userModel.Email).FirstOrDefault();
            var foundProject = _projectModel.Find<ProjectModel>((project) => project.UserId == foundUser.Id).ToList<ProjectModel>().Find((project) => project.Id == projectId);
            return foundProject;
        }

        public void UpdateTech(string tech, string projectId) {
            var foundProject = _projectModel.Find<ProjectModel>((project) => project.Id == projectId).FirstOrDefault();
            if(foundProject != null) {
                var technologies = new List<string>();
                if(foundProject.UsedTechnologies != null) {
                    foreach(var techn in foundProject.UsedTechnologies) {
                        technologies.Add(techn);
                    }
                }
                technologies.Add(tech);
                var filter = Builders<ProjectModel>.Filter.Eq("_id", projectId);
                var techArr = BsonArray.Create(technologies);
                var update = Builders<ProjectModel>.Update.Set("UsedTechnologies", techArr);
                _projectModel.FindOneAndUpdate(filter, update);
            }
        }
    }
}