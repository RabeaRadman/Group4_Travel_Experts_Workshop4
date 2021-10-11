using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

/*
 * Author: Gilmar Castillo
 * This model will gather all information necesary for CRUD operations of a booking record
 * 
 */
namespace G4TEWS4_MVC.Models
{
    public class BookingBookingDetailModel
    {
        // Booking fields
        public string BookingId { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date), DisplayName("Booking Date")]
        public DateTime? BookingDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date), DisplayName("Start Date")]
        public DateTime? StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date), DisplayName("End Date")]
        public DateTime? EndDate { get; set; }

        [DisplayName("Trip Description")]
        public string PkgDesc { get; set; }

        [DisplayName("Booking Number")]
        public string BookingNo { get; set; }

        [DisplayName("Number of Travellers")]
        public double? TravelerCount { get; set; }

        [DisplayName("Customer")]
        public string CustomerId { get; set; }

        [DisplayName("Trip Type")]
        public string TripTypeId { get; set; }

        [DisplayName("Package Booked")]
        public string PackageId { get; set; }

        [DataType(DataType.Currency), DisplayName("Base Price (CAD$)")]
        public decimal? BasePrice { get; set; }

        [DisplayName("Total Owing (CAD$) - Includes Commission")]
        public decimal? Total { get; set; }
    }
}
