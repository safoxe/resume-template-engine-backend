namespace engine_plugin_backend.Models
{
    public class UserDatabaseSettingsModel : IBaseSettingsModels
    {
        public string CollectionName { get; set; }
        public string ProjectsCollection { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IBaseSettingsModels
    {
        string CollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string ProjectsCollection { get; set; }
    }
}