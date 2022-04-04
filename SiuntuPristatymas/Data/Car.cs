namespace Siuntos.Data.Models;

public class Car
{
    public string Number { get; set; }
    public int Filled { get; set; }
    public int MaxCapacity { get; set; }
    
    public List<Delivery> Deliveries { get; set; }
    
}