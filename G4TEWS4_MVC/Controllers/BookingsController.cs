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


        /* Author: Gilmar Castillo
         * 
         */
        // GET: Bookings/Create
        public ActionResult Create(int pkgId)
        {
            var loginCust = HttpContext.Session.GetObject<Customer>("login");
            Package package = new();

            package = _context.Packages.Find(pkgId);

            List<TripType> tripTypes = _context.TripTypes.ToList();
            List<Customer> customers = _context.Customers.ToList();
            ViewBag.TripTypes = tripTypes;
            ViewBag.PackageiId = pkgId;
            ViewBag.PackageName = package.PkgName;
            ViewBag.PackageDesc = package.PkgDesc;
            ViewBag.PackageBasePrice = package.PkgBasePrice;
            ViewBag.PackageSD = package.PkgStartDate;
            ViewBag.PackageED = package.PkgEndDate;
            ViewBag.CustomerId = loginCust.CustomerId;
            ViewBag.CustFullName = CustomerPackageMgr.CustFullName(loginCust.CustomerId);
            //ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustUserName");
            //ViewData["PackageId"] = new SelectList(_context.Packages, "PackageId", "PkgName");
            //ViewData["TripTypeId"] = new SelectList(_context.TripTypes, "TripTypeId", "TripTypeId");
            ViewBag.AddOrUpdate = "Add";
            return View("Edit", new BookingBookingDetailModel());
        }

        public ActionResult Edit(int id)
        {
            List<TripType> tripTypes = _context.TripTypes.ToList();
            ViewBag.TripTypes = tripTypes;
            BookingBookingDetailModel bbdm = new();
            Booking booking = _context.Bookings.Find(id);
            Package package = _context.Packages.Find(booking.PackageId);
            BookingDetail bookingDetail = _context.BookingDetails.Find(id);
            bbdm.BookingDate = booking.BookingDate;
            bbdm.BookingId = booking.BookingId.ToString();
            bbdm.CustomerId = booking.CustomerId.ToString();
            bbdm.PackageId = booking.PackageId.ToString();
            bbdm.BookingNo = booking.BookingNo;
            bbdm.TravelerCount = booking.TravelerCount;
            bbdm.TripTypeId = booking.TripTypeId;
            bbdm.BasePrice = package.PkgBasePrice;
            bbdm.Total = (decimal)((package.PkgBasePrice * Convert.ToDecimal(booking.TravelerCount)) + package.PkgAgencyCommission);
            bbdm.PkgDesc = package.PkgDesc;
            bbdm.StartDate = package.PkgStartDate;
            bbdm.EndDate = package.PkgEndDate;
            ViewBag.AddOrUpdate = "Update";
            return View("Edit", bbdm);
        }

        /* Author: Gilmar Castillo
         * Method: [HttpPost] Create of Booking record.  Booking Details will not be saved.
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, BookingBookingDetailModel bbdm)
        {
            var loginCust = HttpContext.Session.GetObject<Customer>("login");
            int CustID = loginCust.CustomerId;

            if (ModelState.IsValid)
            {
                try
                {
                    if(Convert.ToInt32(bbdm.BookingId) == 0) // create scenario
                    {
                        // Creae a new booking
                        Booking booking = new();
                        booking.BookingDate = bbdm.BookingDate;
                        booking.BookingNo = bbdm.BookingNo;
                        booking.CustomerId = Convert.ToInt32(bbdm.CustomerId);
                        booking.PackageId = Convert.ToInt32(bbdm.PackageId);
                        booking.TravelerCount = Convert.ToDouble(bbdm.TravelerCount);
                        booking.TripTypeId = bbdm.TripTypeId;
                        _context.Add(booking);
                        _context.SaveChanges();

                        //// Create the corresponding details for the booking
                        //BookingDetail bookingDetail = new();
                        //bookingDetail.AgencyCommission = bbdm.BasePrice * 0.1m;
                        //bookingDetail.BasePrice = bbdm.BasePrice;
                        //bookingDetail.TripStart = bbdm.StartDate;
                        //bookingDetail.TripEnd = bbdm.EndDate;

                        //// Hard coded (default) foreign key values
                        //bookingDetail.ClassId = "SNG"; // Single
                        //bookingDetail.FeeId = "BK"; // Booking charge
                        //bookingDetail.ProductSupplierId = 1918; //MARKET SQUARE TOURS
                        //bookingDetail.RegionId = "EU"; // Europe & UK
                        //bookingDetail.ItineraryNo = 855; //855
                        //_context.Add(bookingDetail);
                        //_context.SaveChanges();
                        TempData["Message"] = "New Booking successfully created.";
                    }
                    else // otherwise, it is an update scenario
                    {
                        Booking oldBooking = _context.Bookings.Find(Convert.ToInt32(bbdm.BookingId));
                        oldBooking.BookingDate = bbdm.BookingDate;
                        oldBooking.BookingNo = bbdm.BookingNo;
                        oldBooking.TravelerCount = bbdm.TravelerCount;
                        oldBooking.TripTypeId = bbdm.TripTypeId;
                        _context.SaveChanges();

                        //BookingDetail oldBD = _context.BookingDetails.Find(bbdm.BookingDetailId);
                        //oldBD.AgencyCommission = bbdm.Price * 0.1m;
                        //oldBD.BasePrice = bbdm.BasePrice;
                        //oldBD.TripStart = bbdm.TripStart;
                        //oldBD.TripEnd = bbdm.TripEnd;
                        //_context.SaveChanges();
                        TempData["Message"] = "Booking successfully updated.";
                    }
                    TempData["BGColor"] = "bg-success";
                    return RedirectToAction("CustomerHistory", "Customers", CustID);

                }
                catch
                {
                    TempData["Message"] = $"Ane error ocurred while booking!!!";
                    TempData["BGColor"] = "bg-danger";
                    return RedirectToAction("CustomerHistory", "Customers", CustID);

                }

            }
            else
            {
                return View();
            }

        }

        // GET: Bookings/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var bookings = await _context.Bookings.FindAsync(id);
        //    if (bookings == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustAddress", bookings.CustomerId);
        //    ViewData["PackageId"] = new SelectList(_context.Packages, "PackageId", "PkgName", bookings.PackageId);
        //    ViewData["TripTypeId"] = new SelectList(_context.TripTypes, "TripTypeId", "TripTypeId", bookings.TripTypeId);
        //    return View(bookings);
        //}

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("BookingId,BookingDate,BookingNo,TravelerCount,CustomerId,TripTypeId,PackageId")] Booking bookings)
        //{
        //    if (id != bookings.BookingId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(bookings);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!BookingsExists(bookings.BookingId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction("CustomerHistory", "Customers");
        //    }
        //    ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustAddress", bookings.CustomerId);
        //    ViewData["PackageId"] = new SelectList(_context.Packages, "PackageId", "PkgName", bookings.PackageId);
        //    ViewData["TripTypeId"] = new SelectList(_context.TripTypes, "TripTypeId", "TripTypeId", bookings.TripTypeId);
        //    //return View(bookings);
        //    return RedirectToAction("CustomerHistory", "Customers");
        //}

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
            TempData["Message"] = "Booking successfully updated.";
            TempData["BGColor"] = "bg-success";
            return RedirectToAction(nameof(Index));
        }

        private bool BookingsExists(int id)
        {
            return _context.Bookings.Any(e => e.BookingId == id);
        }
    }
}
