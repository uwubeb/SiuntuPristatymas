namespace SiuntuPristatymas.Data.Dtos;

public class ParcelCreateDto
{
    public int Length { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public int Weight { get; set; }
    
    public Address Address { get; set; }
}