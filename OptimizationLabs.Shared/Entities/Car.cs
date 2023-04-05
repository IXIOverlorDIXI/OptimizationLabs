using System.Text.Json.Serialization;

namespace OptimizationLabs.Shared.Entities
{
    public class Car : BaseEntity
    {
        public double DeliveryCost { get; set; }
        
        public double CarWeightLimit { get; set; }
        
        public double CarVolumeLimit { get; set; }
        
        [JsonIgnore]
        public virtual ICollection<Delivery>? Deliveries { get; set; }
    }
}