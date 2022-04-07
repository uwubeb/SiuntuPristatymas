using SiuntuPristatymas.Data.Base;

namespace SiuntuPristatymas.Data.Models;

public class Car : BaseEntity
{
    
    public string PlateNumber { get; set; }
    public int Filled { get; set; }
    public int MaxCapacity { get; set; }
    
    public List<Delivery> Deliveries { get; set; }
    
}