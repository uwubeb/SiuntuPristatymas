using SiuntuPristatymas.Data.Base;

namespace SiuntuPristatymas.Data.Models;

public class Car : BaseEntity
{
    
    public string PlateNumber { get; set; }
    public int Filled { get; set; }
    public int MaxCapacity { get; set; }
    
    public virtual ICollection<Delivery> Deliveries { get; set; }
    
    public Car()
    {
        Deliveries = new List<Delivery>();
    }
    
}