using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace engine_plugin_backend.Models
{
    public class ProjectModel
    {
        [Key]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string UserId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string[] UsedTechnologies { get; set; }

        public string AssignedTo { get; set; }

        public string Location { get; set; }
    }
}