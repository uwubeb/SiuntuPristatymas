using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiuntuPristatymas.Data;
using SiuntuPristatymas.Data.Models;

namespace SiuntuPristatymas.Controllers
{

    public class CourierDeliveryController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IMapper _mapper;

        public CourierDeliveryController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<Delivery> deliveries = new List<Delivery>();
            if (User.Identity.Name != null)
            {
                var users = _context.Users;
                ApplicationUser user = _context.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();

                deliveries = await _context.Deliveries.Where(d => d.UserId == user!.Id)
                .Include(x => x.Car)
                .Include(x => x.DeliveryRoute)
                .Include(x => x.Parcels)
                .ToListAsync();
            }

            return View(deliveries);
        }

        public async Task<IActionResult> Details(int Id)
        {

            var delivery = await _context.Deliveries
                .Include(x => x.Car)
                .Include(x => x.DeliveryRoute)
                .Include(x => x.Parcels)
                .Where(x => x.Id == Id)
                .FirstOrDefaultAsync();

            if (delivery == null)
            {
                return NotFound();
            }

            return View(delivery);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateDeliveryStatus(int Id, DeliveryStatusEnum Status)
        {
            if (Status == DeliveryStatusEnum.Planned)
            {
                Status = DeliveryStatusEnum.InProgress;
            }
            else if (Status == DeliveryStatusEnum.Returning)
            {
                Status = DeliveryStatusEnum.Done;
            }
            var delivery = _context.Deliveries.FirstOrDefault(x => x.Id == Id);
            delivery.Status = Status;
            _context.Deliveries.Update(delivery);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { Id = Id });


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateParcelStatus(int ParcelId, int DeliveryId, ParcelStatusEnum Status) 
        {

            var parcel = _context.Parcels.FirstOrDefault(x => x.Id == ParcelId);
            if(Status == ParcelStatusEnum.InTransit)
            {
                parcel.Status = ParcelStatusEnum.Delivered;
            }

            var delivery = _context.Deliveries.FirstOrDefault(x => x.Id == DeliveryId)!;

            _context.Parcels.Update(parcel);

            //bool anyNotDelivered = _context.Parcels.Where(p=>p.DeliveryId==DeliveryId).Any(p => p.Status != ParcelStatusEnum.Delivered);       

            var parcels = _context.Parcels.Where(p => p.DeliveryId == DeliveryId);

            bool AllDelivered = true;
            foreach(var p in parcels)
            {
                if(p.Status != ParcelStatusEnum.Delivered)
                {
                    AllDelivered = false;
                    break;
                }
            }
            //bool AnyNotDelivered = parcels.Any(p => p.Status != ParcelStatusEnum.Delivered);
            
            if (AllDelivered)
            {
                delivery.Status = DeliveryStatusEnum.Returning;
                _context.Deliveries.Update(delivery);
            }
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { Id = DeliveryId });

        }



    }
}
