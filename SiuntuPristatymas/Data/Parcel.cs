namespace Siuntos.Data.Models;

public class Parcel
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