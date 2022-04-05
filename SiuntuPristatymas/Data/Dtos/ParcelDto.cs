namespace SiuntuPristatymas.Data.Dtos;

public class ParcelDto
{
    public int Id { get;set; } 
    public int Length { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public int Weight { get; set; }
    public ParcelStatusEnum Status { get; set; }
    
    public int? DeliveryId { get; set; }
    
    public int AddressId { get; set; }
    
    public List<ParcelHistory> ParcelHistory { get; set; }
}