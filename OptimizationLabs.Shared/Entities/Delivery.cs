using System.Text.Json.Serialization;

namespace OptimizationLabs.Shared.Entities
{
    public class Delivery : BaseEntity
    {
        public Guid CarId { get; set; }
        
        public virtual Car? Car { get; set; }
        
        public Guid ItemId { get; set; }
        
        [JsonIgnore]
        public virtual Item? Item { get; set; }
    }
}