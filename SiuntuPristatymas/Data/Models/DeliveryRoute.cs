using SiuntuPristatymas.Data.Base;

namespace SiuntuPristatymas.Data.Models;

public class DeliveryRoute : BaseEntity
{
    public string City { get; set; }
    public int Distance { get; set; }
    public TimeSpan AverageDuration { get; set; }
    
    public virtual ICollection<Delivery> Deliveries { get; set; }
    
    public DeliveryRoute()
    {
        Deliveries = new List<Delivery>();
    }
}

