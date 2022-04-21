using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SiuntuPristatymas.Data;
using SiuntuPristatymas.Data.Dtos;
using SiuntuPristatymas.Data.Models;
using SiuntuPristatymas.Repositories;

namespace SiuntuPristatymas.Controllers
{
    public class DeliveryController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IMapper _mapper;

        public DeliveryController(ApplicationDbContext context, IMapper mapper)
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

        public IActionResult Create()
        {
            ViewBag.CarId = new SelectList(_context.Set<Car>(), "Id", "Id");
            ViewBag.DeliveryRouteId = new SelectList(_context.Set<DeliveryRoute>(), "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DeliveryDto deliveryDto)
        {
            var delivery = new Delivery();
            _mapper.Map(deliveryDto, delivery);

            if (ModelState.IsValid)
            {
                await _context.Deliveries.AddAsync(delivery);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CarId = new SelectList(_context.Set<Car>(), "Id", "Id");
            ViewBag.DeliveryRouteId = new SelectList(_context.Set<DeliveryRoute>(), "Id", "Id");
            return View(deliveryDto);
        }

        public async Task<IActionResult> Edit(int id)
        {

            var delivery = await _context.Deliveries.FindAsync(id);
            if (delivery == null)
            {
                return NotFound();
            }

            var deliveryDto = new DeliveryDto();
            _mapper.Map(delivery, deliveryDto);

            ViewBag.CarId = new SelectList(_context.Set<Car>(), "Id", "Id");
            ViewBag.DeliveryRouteId = new SelectList(_context.Set<DeliveryRoute>(), "Id", "Id");

            return View(deliveryDto);

        }

        //Html form doesn't support HttpPut
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DeliveryDto deliveryDto)
        {

            if (ModelState.IsValid)
            {
                var delivery = new Delivery();
                _mapper.Map(deliveryDto, delivery);
                _context.Deliveries.Update(delivery);
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CarId = new SelectList(_context.Set<Car>(), "Id", "Id");
            ViewBag.DeliveryRouteId = new SelectList(_context.Set<DeliveryRoute>(), "Id", "Id");

            return View(deliveryDto);

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

        public async Task<IActionResult> Delete(int id)
        {
            var delivery = await _context.Deliveries.FindAsync(id);
            if (delivery == null)
            {
                return NotFound();
            }
            var deliveryDto = new DeliveryDto();
            _mapper.Map(delivery, deliveryDto);
            return View(deliveryDto);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var delivery = await _context.Deliveries.FindAsync(id);
            if (delivery == null)
            {
                return NotFound();
            }

            //should be deleted cascade automatically
            // var parcels = _context.Parcels.Where(p => p.DeliveryId == id);
            //
            // foreach (var parcel in parcels)
            // {
            //
            //     parcel.DeliveryId = null;
            //     parcel.Delivery = null;
            //
            // }

            _context.Deliveries.Remove(delivery);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
