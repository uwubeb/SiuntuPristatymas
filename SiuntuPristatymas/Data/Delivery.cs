using System.ComponentModel.DataAnnotations;

namespace Siuntos.Data.Models;

public class Delivery
{
    //Gonna have to change to enum, still figuring out
    public string Status { get; set; }
    public int FilledCapacity { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan EstimatedLength { get; set; }
    
    public int CarId { get; set; }
    public Car Car { get; set; }
    
    public int DeliveryRouteId { get; set; }
    public DeliveryRoute DeliveryRoute { get; set; }
    
    public List<Parcel> Parcels { get; set; }
    
    
}