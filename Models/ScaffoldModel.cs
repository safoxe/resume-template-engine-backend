using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace engine_plugin_backend.Models
{
    public class ScaffoldModel
    {
        [Key]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Domain { get; set; }
        public string PositionType { get; set; }
        public string SeniorityLevel { get; set; }
        public string MainTechnology { get; set; }
        public List<string> AdditionalTechnologies { get; set; }
    }
}