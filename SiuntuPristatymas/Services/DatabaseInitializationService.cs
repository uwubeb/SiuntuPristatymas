using Microsoft.EntityFrameworkCore;
using SiuntuPristatymas.Data;
using SiuntuPristatymas.Data.Models;

namespace SiuntuPristatymas.Services
{
    public class DatabaseInitializationService : IHostedService
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger<DatabaseInitializationService> logger;

        public DatabaseInitializationService(IServiceProvider serviceProvider, ILogger<DatabaseInitializationService> logger)
        {
            this.serviceProvider = serviceProvider;
            this.logger = logger;
        }


        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = serviceProvider.CreateScope();
            var dataContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            if (!dataContext.Cars.Any())
            {
                await Seed(dataContext);
            }

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private async Task Seed(ApplicationDbContext dataContext)
        {
            // TODO: Seed database


            var cars = new Car[] {
                new Car { PlateNumber = "000 ABC",Filled=5,MaxCapacity=20 },
                new Car { PlateNumber = "001 ABA",Filled=12,MaxCapacity=20 },
                new Car { PlateNumber = "002 ABB",Filled=18,MaxCapacity=20 }};

            var addresses = new Address[] {
                new Address { City = "Vilnius", Street = "Didvyriu g.", Number = "1", PostCode = "12345" },
                new Address { City = "Kaunas", Street = "Kauno g.", Number = "2", PostCode = "12346" },
                new Address { City = "Alytus", Street = "Kernaves g.", Number = "3", PostCode= "12347" }};

            var deliveryRoutes = new DeliveryRoute[] {
                new DeliveryRoute { City = "Vilnius", Distance = 125,
                    AverageDuration = new TimeSpan(hours: 1, minutes: 47, seconds: 0) },
                new DeliveryRoute { City = "Kaunas", Distance = 67,
                    AverageDuration = new TimeSpan(hours: 0, minutes: 56, seconds: 42) },
                new DeliveryRoute { City = "Druskininkai", Distance = 112,
                    AverageDuration = new TimeSpan(hours: 1, minutes: 24, seconds: 15) }};

            var deliveries = new Delivery[] {

                new Delivery { Status=DeliveryStatusEnum.InProgress, FilledCapacity=5, Date = new DateTime(), EstimatedDuration=new TimeSpan(hours: 1, minutes: 24, seconds: 15), Car=cars[0],DeliveryRoute=deliveryRoutes[0] },
                new Delivery { Status=DeliveryStatusEnum.InProgress, FilledCapacity=20, Date = new DateTime(), EstimatedDuration=new TimeSpan(hours: 4, minutes: 11, seconds: 12), Car=cars[1],DeliveryRoute=deliveryRoutes[1] }};


            var parcels = new Parcel[]
            {
                new Parcel{Length=20,Width = 25,Height=23,Weight=14,Status=ParcelStatusEnum.InTransit,Delivery=deliveries[0],Address=addresses[0]},
                new Parcel{Length=15,Width = 20,Height=25,Weight=42,Status=ParcelStatusEnum.InTransit,Delivery=deliveries[1],Address=addresses[1]}
            };

            var parcelHistories = new ParcelHistory[]
            {

                new ParcelHistory {Time = DateTime.Now.Subtract(TimeSpan.FromHours(2)), Parcel = parcels[0], Status = ParcelStatusEnum.InTransit},
                new ParcelHistory {Time = DateTime.Now, Parcel = parcels[0], Status = ParcelStatusEnum.Delivered},


            };

            await dataContext.Addresses.AddRangeAsync(addresses);
            await dataContext.Cars.AddRangeAsync(cars);
            await dataContext.DeliveryRoutes.AddRangeAsync(deliveryRoutes);
            await dataContext.Deliveries.AddRangeAsync(deliveries);
            await dataContext.Parcels.AddRangeAsync(parcels);
            await dataContext.ParcelHistories.AddRangeAsync(parcelHistories);
            await dataContext.SaveChangesAsync();

            return;
        }

    }
}
