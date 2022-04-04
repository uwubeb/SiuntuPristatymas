namespace Siuntos.Data.Models;

public class DeliveryRoute
{
    public string City { get; set; }
    public int Distance { get; set; }
    public TimeSpan AverageLength { get; set; }
    
    public List<Delivery> Deliveries { get; set; }
}