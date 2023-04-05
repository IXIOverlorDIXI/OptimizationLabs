using System.Text.Json.Serialization;

namespace OptimizationLabs.Shared.Entities
{
    public class Item : BaseEntity
    {
        public double ItemPrice { get; set; }
        
        public double ItemWeight { get; set; }
        
        public double ItemVolume { get; set; }
        
        [JsonIgnore]
        public virtual ICollection<Delivery>? Deliveries { get; set; }
    }
}