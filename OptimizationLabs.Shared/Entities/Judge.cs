using System.Collections;
using System.Text.Json.Serialization;

namespace OptimizationLabs.Shared.Entities
{
    public class Judge : BaseEntity
    {
        public string FullName { get; set; }
        
        public int ExpertLevel { get; set; }
        
        [JsonIgnore]
        public virtual ICollection<Grade>? Grades { get; set; }
    }
}