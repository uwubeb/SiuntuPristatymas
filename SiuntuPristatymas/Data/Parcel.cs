using Siuntos.Data.Models;
using SiuntuPristatymas.Data.Base;

namespace SiuntuPristatymas.Data;

public class Parcel : BaseEntity
{
    public int Length { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public int Weight { get; set; }
    //Change to enum
    public string Status { get; set; }
    
    public int? DeliveryId { get; set; }
    public Delivery? Delivery { get; set; }
    
    public int AddressId { get; set; }
    public Address Address { get; set; }
    
    public List<ParcelHistory> ParcelHistory { get; set; }
}