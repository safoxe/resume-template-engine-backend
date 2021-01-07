using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace engine_plugin_backend.Models
{
    public class ScaffoldModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }

        //ADD ENUMS
        public string Domain { get; set; }
        //ADD ENUMS
        public string PositionType { get; set; }
        //ADD ENUMS
        public string SeniorityLevel { get; set; }
    }
}