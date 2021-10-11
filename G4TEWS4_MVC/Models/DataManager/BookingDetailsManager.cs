/*
Author: Rabea, Tiffanie 
Purpose: To Get booking details for customers
*/
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using G4TEWS4_Data;

namespace G4TEWS4_MVC.Models.DataManager
{
    public class BookingDetailsManager
    {
        public static List<BookingDetail> GetAllBookingsDetailsByCustomer(int bookingId)
        {
            var context = new TEContext();
            var bookingDetails = context.BookingDetails
                .Include(itinerary => itinerary.ItineraryNo)
                .Include(tripStart => tripStart.TripStart)
                .Include(tripEnd => tripEnd.TripEnd)
                .Include(description => description.Description)
                .Include(destination => destination.Destination)
                .Include(price => price.BasePrice)
                .Where(bk => bk.BookingId == bookingId)
                .ToList();

            return bookingDetails;

        }
    }
}
