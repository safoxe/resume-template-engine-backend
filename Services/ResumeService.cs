using MongoDB.Driver;
using engine_plugin_backend.Models;
using System.Collections.Generic;

namespace engine_plugin_backend.Services
{
    public class ResumeService
    {
        private readonly IMongoCollection<ResumeModel> _resumeModel;
        private readonly IMongoCollection<ProjectModel> _projectModel;

        public ResumeService(IBaseSettingsModels settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _resumeModel = database.GetCollection<ResumeModel>("resume-data");
            _projectModel = database.GetCollection<ProjectModel>(settings.ProjectsCollection);
        }

        public void CreateResume(ResumeModel resume)
        {
            _resumeModel.InsertOne(resume);
        }

        public IList<ResumeModel> GetResumes(string userName, string projectId)
        {
            var foundProject = _projectModel.Find<ProjectModel>((project) => project.Id == projectId).FirstOrDefault();
            var foundResumes = _resumeModel.Find<ResumeModel>((resume) => resume.ProjectId == foundProject.Id).ToList<ResumeModel>();
            return foundResumes;
        }
    }
}