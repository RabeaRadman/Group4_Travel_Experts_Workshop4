using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace G4TEWS4_Data
{
    [Index(nameof(CustomerId), Name = "BookingsCustomerId")]
    [Index(nameof(CustomerId), Name = "CustomersBookings")]
    [Index(nameof(PackageId), Name = "PackageId")]
    [Index(nameof(PackageId), Name = "PackagesBookings")]
    [Index(nameof(TripTypeId), Name = "TripTypesBookings")]
    public partial class Booking
    {
        public Booking()
        {
            BookingDetails = new HashSet<BookingDetail>();
        }

        [Key]
        public int BookingId { get; set; }
        [Column(TypeName = "DateTime")]
        [DataType(DataType.Date)]
        [DisplayName("Booking Date")]
        public DateTime? BookingDate { get; set; }
        [StringLength(50)]
        [DisplayName("Booking No")]
        public string BookingNo { get; set; }
        [DisplayName("Traveler Count")]
        public double? TravelerCount { get; set; }
        [DisplayName("Customer ID")]
        public int? CustomerId { get; set; }
        [StringLength(1)]
        [DisplayName("Triptype ID")]
        public string TripTypeId { get; set; }
        [DisplayName("Package ID")]
        public int? PackageId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        [InverseProperty("Bookings")]
        public virtual Customer Customer { get; set; }
        [ForeignKey(nameof(PackageId))]
        [InverseProperty("Bookings")]
        public virtual Package Package { get; set; }
        [ForeignKey(nameof(TripTypeId))]
        [InverseProperty("Bookings")]
        public virtual TripType TripType { get; set; }
        [InverseProperty(nameof(BookingDetail.Booking))]
        public virtual ICollection<BookingDetail> BookingDetails { get; set; }
    }
}
