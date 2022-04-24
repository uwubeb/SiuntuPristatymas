using SiuntuPristatymas.Data.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiuntuPristatymas.Data.Models;

public class Delivery : BaseEntity
{
    public DeliveryStatusEnum Status { get; set; }
    public int FilledCapacity { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan EstimatedDuration { get; set; }
    
    public int CarId { get; set; }
    public virtual Car Car { get; set; }
    
    public int DeliveryRouteId { get; set; }
    public virtual DeliveryRoute DeliveryRoute { get; set; }
    
    public virtual ICollection<Parcel> Parcels { get; set; }

    public string? UserId { get; set; }
    public virtual ApplicationUser? User { get; set; }
    
    public Delivery()
    {
        Parcels = new List<Parcel>();
    }
    
    
}