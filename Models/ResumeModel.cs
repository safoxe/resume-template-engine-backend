using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace engine_plugin_backend.Models
{
    public class ResumeModel
    {
        [Key]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string ProjectId { get; set; }

        public string ProfessionType { get; set; }

        public string SeniorityLevel { get; set; }
    }
}