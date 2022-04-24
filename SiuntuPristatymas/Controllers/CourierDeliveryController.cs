using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiuntuPristatymas.Data;

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
            var deliveries = await _context.Deliveries
                .Include(x => x.Car)
                .Include(x => x.DeliveryRoute)
                .Include(x => x.Parcels)
                .ToListAsync();

            return View(deliveries);
        }

        public async Task<IActionResult> Details(int id)
        {

            var delivery = await _context.Deliveries
                .Include(x => x.Car)
                .Include(x => x.DeliveryRoute)
                .Include(x => x.Parcels)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (delivery == null)
            {
                return NotFound();
            }

            //var deliveryDto = new DeliveryDto();
            //_mapper.Map(delivery, deliveryDto);

            return View(delivery);
        }

    }
}
