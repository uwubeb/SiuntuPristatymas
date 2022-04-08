#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SiuntuPristatymas.Data;
using SiuntuPristatymas.Data.Models;
using SiuntuPristatymas.Repositories;
using SiuntuPristatymas.Services;

namespace SiuntuPristatymas.Controllers
{
    public class ParcelController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IRepository<Parcel> _parcelRepository;

        public ParcelController(IRepository<Parcel> parcelRepository, ApplicationDbContext context)
        {
            _context = context;
            _parcelRepository = parcelRepository;
        }
        
        // GET: Parcel
        public async Task<IActionResult> Index()
        {
            var parcels = await _parcelRepository.GetAll();
            return View(parcels);
        }

        // GET: Parcel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var parcel = await _parcelRepository.GetById(id.Value);
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
        public async Task<IActionResult> Create([Bind("Length,Width,Height,Weight,Status,DeliveryId,AddressId")] Parcel parcel)
        {
            // parcel.ParcelHistory = new List<ParcelHistory>();
            if (ModelState.IsValid)
            {
                await _parcelRepository.Create(parcel);
                // _context.Add(parcel);
                // await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressId"] = new SelectList(_context.Set<Address>(), "Id", "Id", parcel.AddressId);
            ViewData["DeliveryId"] = new SelectList(_context.Set<Delivery>(), "Id", "Id", parcel.DeliveryId);
            return View(parcel);
        }

        // GET: Parcel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var parcel = await _parcelRepository.GetById(id.Value);
            // var parcel = await _context.Parcels.FindAsync(id);
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
                    await _parcelRepository.Update(parcel);
                    // _context.Update(parcel);
                    // await _context.SaveChangesAsync();
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

            var parcel = await _parcelRepository.GetById(id.Value);
            // var parcel = await _context.Parcels
            //     .Include(p => p.Address)
            //     .Include(p => p.Delivery)
            //     .FirstOrDefaultAsync(m => m.Id == id);
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
            var parcel = await _parcelRepository.GetById(id);
            await _parcelRepository.Delete(parcel);
            // var parcel = await _context.Parcels.FindAsync(id);
            // _context.Parcels.Remove(parcel);
            // await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ParcelExists(int id)
        {
            // return _context.Parcels.Any(e => e.Id == id);
            return await _parcelRepository.Exists(id);
        }
    }
}
