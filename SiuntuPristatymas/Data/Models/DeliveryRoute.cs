using SiuntuPristatymas.Data.Base;

namespace SiuntuPristatymas.Data;

public class DeliveryRoute : BaseEntity
{
    public string City { get; set; }
    public int Distance { get; set; }
    public TimeSpan AverageDuration { get; set; }
    
    public List<Delivery> Deliveries { get; set; }
}

