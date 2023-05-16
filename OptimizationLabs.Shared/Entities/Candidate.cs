using System.Text.Json.Serialization;

namespace OptimizationLabs.Shared.Entities
{
    public class Candidate : BaseEntity
    {
        public string FullName { get; set; }
        
        public int NumberInContest { get; set; }
        
        [JsonIgnore]
        public virtual ICollection<Grade>? Grades { get; set; }
    }
}