#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SiuntuPristatymas.Data;
using SiuntuPristatymas.Data.Dtos;
using SiuntuPristatymas.Data.Models;
using SiuntuPristatymas.Repositories;
using SiuntuPristatymas.Services;

namespace SiuntuPristatymas.Controllers
{
    public class ParcelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParcelController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        // GET: Parcel
        public async Task<IActionResult> Index(string searchString)
        {
            if (String.IsNullOrEmpty(searchString))
            {
                var parcels = await _context.Parcels
                    .Include(p => p.Address)
                    .Include(p => p.Delivery)
                    .ToListAsync();
                return View(parcels);

            }
            else
            {
                searchString = searchString.ToLower();
                //check if any property of the object matches the search string
                var parcels = _context.Parcels
                    .Include(p => p.Address)
                    .Include(p => p.Delivery)
                    .Where(p => p.Length.ToString().Contains(searchString)
                                || p.Width.ToString().Contains(searchString)
                                || p.Height.ToString().Contains(searchString)
                                //ParcelStatusEnum get description
                                // || String.Parse(p.Status).ToString().ToLower().Contains(searchString)
                                // || p.Status.GetE<ParcelStatusEnum>().Contains(searchString)
                                // gotta figure out this
                                || p.Delivery.Id.ToString().Contains(searchString)
                                || p.AddressId.ToString().Contains(searchString))
                    .ToListAsync();
                return View(await parcels);
            }
        }
        // GET: Parcel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //find parcel by id
            var parcel = await _context.Parcels
                .Include(p => p.Address)
                .Include(p => p.Delivery)
                .Include(p => p.ParcelHistory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parcel == null)
            {
                return NotFound();
            }

            return View(parcel);
        }

        // GET: Parcel/Create
        public IActionResult Create()
        {
            ViewData["AddressId"] = new SelectList(_context.Set<Address>(), "Id", "Id");
            ViewData["DeliveryId"] = new SelectList(_context.Set<Delivery>(), "Id", "Id");
            return View();
        }

        // POST: Parcel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Length,Width,Height,Weight,Status,AddressId")] Parcel parcel)
        {
            // parcel.ParcelHistory = new List<ParcelHistory>();
            if (ModelState.IsValid)
            {
                var delivery = await AssignToDelivery(parcel);
                if (delivery != null)
                {
                    parcel.Delivery = await AssignToDelivery(parcel);
                    parcel.Status = ParcelStatusEnum.WaitingForPickup;
                }
                else
                {
                    parcel.Status = ParcelStatusEnum.NotAssigned;
                }
                //create parcel
                await _context.AddAsync(parcel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressId"] = new SelectList(_context.Set<Address>(), "Id", "Id", parcel.AddressId);
            //ViewData["DeliveryId"] = new SelectList(_context.Set<Delivery>(), "Id", "Id", parcel.DeliveryId);
            return View(parcel);
        }

        // GET: Parcel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var parcel = await _context.Parcels.FindAsync(id);
            if (parcel == null)
            {
                return NotFound();
            }
            ViewData["AddressId"] = new SelectList(_context.Set<Address>(), "Id", "Id", parcel.AddressId);
            ViewData["DeliveryId"] = new SelectList(_context.Set<Delivery>(), "Id", "Id", parcel.DeliveryId);
            return View(parcel);
        }

        // POST: Parcel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Length,Width,Height,Weight,Status,DeliveryId,AddressId,Id")] Parcel parcel)
        {
            if (id != parcel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parcel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! (await ParcelExists(parcel.Id)))
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
            ViewData["AddressId"] = new SelectList(_context.Set<Address>(), "Id", "Id", parcel.AddressId);
            ViewData["DeliveryId"] = new SelectList(_context.Set<Delivery>(), "Id", "Id", parcel.DeliveryId);
            return View(parcel);
        }

        // GET: Parcel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parcel = await _context.Parcels
            .Include(p => p.Address)
            .Include(p => p.Delivery)
            .FirstOrDefaultAsync(m => m.Id == id);
            
            if (parcel == null)
            {
                return NotFound();
            }

            return View(parcel);
        }

        // POST: Parcel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parcel = await _context.Parcels.FindAsync(id);
            _context.Parcels.Remove(parcel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        
        private async Task<Delivery> AssignToDelivery(Parcel parcel)
        {
            var delivery = await _context.Deliveries.FirstOrDefaultAsync(d => d.Status == DeliveryStatusEnum.Planned);
            return delivery;
        }

        private async Task<bool> ParcelExists(int id)
        {
            return await _context.Parcels.AnyAsync(e => e.Id == id);
        }
    }
}
