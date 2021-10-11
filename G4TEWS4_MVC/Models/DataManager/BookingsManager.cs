/*
Author: Rabea with help of Masoud B.
Purpose: Get bookings history for customers
*/
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using G4TEWS4_Data;

namespace G4TEWS4_MVC.Models.DataManager
{
    public class BookingsManager
    {
        public static List<Booking> GetAllBookingsByCustomer(int customerId)
        {
            var context = new TEContext();
            var bookings = context.Bookings
                .Include(customer => customer.Customer)
                .Include(trip => trip.TripType)
                .Include(package => package.Package)
                .Include(bookingDetail => bookingDetail.BookingDetails)
                .Where(booking => booking.CustomerId == customerId)
                .ToList();
                                        
            return bookings;

        }

        public static Booking Find(int id)
        {
            var context = new TEContext();

            var booking = context.Bookings
                .Include(customer => customer.Customer)
                .Include(trip => trip.TripType)
                .Include(package => package.Package)
                .Include(bookingDetail => bookingDetail.BookingDetails)
                .SingleOrDefault(bk => bk.BookingId == id);

            return booking;

        }
    }
}
