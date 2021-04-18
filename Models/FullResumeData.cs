using System.Collections.Generic;

namespace engine_plugin_backend.Models
{
    public class FullResumeData
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IList<string> UsedTechnologies { get; set; }

        public string AssignedTo { get; set; }

        public string Location { get; set; }

        public string ProfessionType { get; set; }

        public string SeniorityLevel { get; set; }
    }
}