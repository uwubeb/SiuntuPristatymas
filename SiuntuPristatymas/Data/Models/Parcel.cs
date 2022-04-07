using SiuntuPristatymas.Data.Base;

namespace SiuntuPristatymas.Data.Models;

public class Parcel : BaseEntity
{
    public int Length { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public int Weight { get; set; }
    public ParcelStatusEnum Status { get; set; }

    public int? DeliveryId { get; set; }
    public Delivery? Delivery { get; set; }
    
    public int AddressId { get; set; }
    public virtual Address Address { get; set; }
    
    public virtual ICollection<ParcelHistory> ParcelHistory { get; set; }
    
    
    public Parcel()
    {
        ParcelHistory = new List<ParcelHistory>();
    }

}