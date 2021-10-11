/*
Author: Rabea with help of Masoud B.
Purpose: Get booking history from customers
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using G4TEWS4_MVC.Models;
using G4TEWS4_MVC.Models.DataManager;
using G4TEWS4_Data;
using Microsoft.AspNetCore.Authorization;

namespace G4TEWS4_MVC.Controllers
{
    
    public class BookingsController : Controller
    {
        private readonly TEContext _context;

        public BookingsController(TEContext context)
        {
            _context = context;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            var TEContext = _context.Bookings.Include(b => b.Customer).Include(b => b.Package).Include(b => b.TripType);
            return View(await TEContext.ToListAsync());
        }

        // GET: Bookings by customer (Customer Booking History)
        // This has been moved to CustomersController
        public ActionResult CustomerHistory()
        {
            // Change this to Id retrieved from sessions
            int id = 143;

            var bookings = BookingsManager.GetAllBookingsByCustomer(id)
                .Select(bk => new BookingsModel
                {
                    BookingId = bk.BookingId,
                    BookingDate = bk.BookingDate,
                    BookingNo = bk.BookingNo,
                    TravelerCount = bk.TravelerCount,
                    CustomerId = bk.Customer.CustFirstName,
                    TripTypeId = bk.TripType.Ttname,
                    PackageId = bk.Package.PkgName
                }).ToList();

            return View(bookings);

        }

        public decimal TotalOwing(decimal amount)
        {
            decimal total = 0;

            total += amount;

            return total;
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookings = await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Package)
                .Include(b => b.TripType)
                .FirstOrDefaultAsync(m => m.BookingId == id);
            if (bookings == null)
            {
                return NotFound();
            }

            return View(bookings);
        }



        // GET: Bookings/Create
        public IActionResult Create()
        {
            var loginCust = HttpContext.Session.GetObject<Customer>("login");

            List<Package> packages = _context.Packages.ToList();
            List<TripType> tripTypes = _context.TripTypes.ToList();
            List<Customer> customers = _context.Customers.ToList();
            ViewBag.Packages = packages;
            ViewBag.TripTypes = tripTypes;
            ViewBag.Customers = customers;
            //ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustUserName");
            //ViewData["PackageId"] = new SelectList(_context.Packages, "PackageId", "PkgName");
            //ViewData["TripTypeId"] = new SelectList(_context.TripTypes, "TripTypeId", "TripTypeId");
            return View("Create", new Booking());
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingId,BookingDate,BookingNo,TravelerCount,CustomerId,TripTypeId,PackageId")] Booking bookings)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookings);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustAddress", bookings.CustomerId);
            ViewData["PackageId"] = new SelectList(_context.Packages, "PackageId", "PkgName", bookings.PackageId);
            ViewData["TripTypeId"] = new SelectList(_context.TripTypes, "TripTypeId", "TripTypeId", bookings.TripTypeId);
            return View(bookings);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookings = await _context.Bookings.FindAsync(id);
            if (bookings == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustAddress", bookings.CustomerId);
            ViewData["PackageId"] = new SelectList(_context.Packages, "PackageId", "PkgName", bookings.PackageId);
            ViewData["TripTypeId"] = new SelectList(_context.TripTypes, "TripTypeId", "TripTypeId", bookings.TripTypeId);
            return View(bookings);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingId,BookingDate,BookingNo,TravelerCount,CustomerId,TripTypeId,PackageId")] Booking bookings)
        {
            if (id != bookings.BookingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookings);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingsExists(bookings.BookingId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("CustomerHistory", "Customers");
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustAddress", bookings.CustomerId);
            ViewData["PackageId"] = new SelectList(_context.Packages, "PackageId", "PkgName", bookings.PackageId);
            ViewData["TripTypeId"] = new SelectList(_context.TripTypes, "TripTypeId", "TripTypeId", bookings.TripTypeId);
            //return View(bookings);
            return RedirectToAction("CustomerHistory", "Customers");
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookings = await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Package)
                .Include(b => b.TripType)
                .FirstOrDefaultAsync(m => m.BookingId == id);
            if (bookings == null)
            {
                return NotFound();
            }

            return View(bookings);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookings = await _context.Bookings.FindAsync(id);
            _context.Bookings.Remove(bookings);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingsExists(int id)
        {
            return _context.Bookings.Any(e => e.BookingId == id);
        }
    }
}
