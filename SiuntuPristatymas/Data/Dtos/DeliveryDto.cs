namespace SiuntuPristatymas.Data.Dtos
{
    public class DeliveryDto
    {
        public int Id { get; set; }
        public DeliveryStatusEnum Status { get; set; }
        public int FilledCapacity { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan EstimatedDuration { get; set; }

        public int CarId { get; set; }
        public int DeliveryRouteId { get; set; }
        
        public List<int> ParcelsIds { get; set; }

        public DeliveryDto()
        {
            ParcelsIds = new List<int>();
        }
    }
}
