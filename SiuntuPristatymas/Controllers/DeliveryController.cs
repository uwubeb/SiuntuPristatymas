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
        private readonly Data.ApplicationDbContext _context;
        private readonly IRepository<Delivery> _deliveryRepository;

        private readonly IMapper _mapper;

        public DeliveryController(IRepository<Delivery> deliveryRepository, ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _deliveryRepository = deliveryRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _deliveryRepository.GetAll());
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
                await _deliveryRepository.Create(delivery);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CarId = new SelectList(_context.Set<Car>(), "Id", "Id");
            ViewBag.DeliveryRouteId = new SelectList(_context.Set<DeliveryRoute>(), "Id", "Id");
            return View(deliveryDto);
        }

        public async Task<IActionResult> Edit(int id)
        {

            var delivery = await _deliveryRepository.GetById(id);
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
                try
                {
                    await _deliveryRepository.Update(delivery);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await _deliveryRepository.Exists(deliveryDto.Id)))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CarId = new SelectList(_context.Set<Car>(), "Id", "Id");
            ViewBag.DeliveryRouteId = new SelectList(_context.Set<DeliveryRoute>(), "Id", "Id");

            return View(deliveryDto);

        }

        public async Task<IActionResult> Details(int id)
        {

            var delivery = await _deliveryRepository.GetById(id);
            if (delivery == null)
            {
                return NotFound();
            }

            var deliveryDto = new DeliveryDto();
            _mapper.Map(delivery, deliveryDto);

            return View(deliveryDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var delivery = await _deliveryRepository.GetById(id);
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
            var delivery = await _deliveryRepository.GetById(id);
            if (delivery == null)
            {
                return NotFound();
            }

            var parcels = _context.Parcels.Where(p => p.DeliveryId == id);

            foreach (var parcel in parcels)
            {

                parcel.DeliveryId = null;
                parcel.Delivery = null;

            }

            await _deliveryRepository.Delete(delivery);
            return RedirectToAction(nameof(Index));
        }

    }
}
