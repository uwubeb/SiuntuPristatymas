using Siuntos.Data.Models;
using SiuntuPristatymas.Data.Base;

namespace SiuntuPristatymas.Data;

public class Delivery : BaseEntity
{
    //Gonna have to change to enum, still figuring out
    public string Status { get; set; }
    public int FilledCapacity { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan EstimatedDuration { get; set; }
    
    public int CarId { get; set; }
    public Car Car { get; set; }
    
    public int DeliveryRouteId { get; set; }
    public DeliveryRoute DeliveryRoute { get; set; }
    
    public List<Parcel> Parcels { get; set; }
    
    
}