/*
Purpose: Get booking details for customers
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace G4TEWS4_MVC.Models
{
    /// <summary>
    /// Booking details model.
    /// </summary>
    /// Not used. TODO: Nuke
    public class BookingDetailsModel
    {

        public double? ItineraryNo { get; set; }
        [DataType(DataType.Date), DisplayName("Trip Start")]

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? TripStart { get; set; }
        [DataType(DataType.Date), DisplayName("Trip End")]

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? TripEnd { get; set; }
        public string Description { get; set; }
        public string Destination { get; set; }
        [DataType(DataType.Currency), DisplayName("Price")]
        public decimal? BasePrice { get; set; }
    }
}
