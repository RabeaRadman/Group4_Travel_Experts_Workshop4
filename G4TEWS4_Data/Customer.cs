using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace G4TEWS4_Data
{
    [Index(nameof(AgentId), Name = "EmployeesCustomers")]
    public partial class Customer
    {
        public Customer()
        {
            Bookings = new HashSet<Booking>();
            CreditCards = new HashSet<CreditCard>();
            CustomersRewards = new HashSet<CustomersReward>();
        }

        [Key]
        [DisplayName("Customer ID")]
        public int CustomerId { get; set; }
        [Required]
        [StringLength(25)]
        [DisplayName("First Name")]
        public string CustFirstName { get; set; }
        [Required]
        [StringLength(25)]
        [DisplayName("Last Name")]
        public string CustLastName { get; set; }
        [Required]
        [StringLength(75)]
        [DisplayName("Address")]
        public string CustAddress { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("City")]
        public string CustCity { get; set; }
        [Required]
        [StringLength(2)]
        [DisplayName("Province")]
        public string CustProv { get; set; }
        [Required]
        [StringLength(7)]
        [DisplayName("Postal Code")]
        public string CustPostal { get; set; }
        [StringLength(25)]
        [DisplayName("Country")]
        public string CustCountry { get; set; }
        [StringLength(20)]
        [DisplayName("Home Phone")]
        public string CustHomePhone { get; set; }
        [Required]
        [StringLength(20)]
        [DisplayName("Business Phone")]
        public string CustBusPhone { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("Email")]
        public string CustEmail { get; set; }
        [DisplayName("Agent Id")]
        public int? AgentId { get; set; }
       
        [Required]
        [StringLength(15)]
        [DisplayName("UserName")]
        public string CustUserName { get; set; }
        [Required]
        [DisplayName("Password")]
        public string CustPassword { get; set; }

        [ForeignKey(nameof(AgentId))]
        [InverseProperty("Customers")]
        public virtual Agent Agent { get; set; }
        [InverseProperty(nameof(Booking.Customer))]
        public virtual ICollection<Booking> Bookings { get; set; }
        [InverseProperty(nameof(CreditCard.Customer))]
        public virtual ICollection<CreditCard> CreditCards { get; set; }
        [InverseProperty(nameof(CustomersReward.Customer))]
        public virtual ICollection<CustomersReward> CustomersRewards { get; set; }
    }
}
