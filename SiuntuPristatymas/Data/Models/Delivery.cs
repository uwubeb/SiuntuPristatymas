using SiuntuPristatymas.Data.Base;

namespace SiuntuPristatymas.Data.Models;

public class Delivery : BaseEntity
{
    public DeliveryStatusEnum Status { get; set; }
    public int FilledCapacity { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan EstimatedLength { get; set; }
    
    public int CarId { get; set; }
    public Car Car { get; set; }
    
    public int DeliveryRouteId { get; set; }
    public DeliveryRoute DeliveryRoute { get; set; }
    
    public List<Parcel> Parcels { get; set; }
    
    
}