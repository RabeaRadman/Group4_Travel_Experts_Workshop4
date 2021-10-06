using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using G4TEWS4_Data;

namespace G4TEWS4_MVC.Models
{
    /*
     */

    /// <summary>
    /// A class represent a user. Includes password, validations for phone and postal code.
    /// </summary>
    public class UserViewModel
    {
        public int CustomerId { get; set; }
        [Required, DisplayName("First Name")]
        public string CustFirstName { get; set; }

        [Required, DisplayName("Last Name")]
        public string CustLastName { get; set; }

        [Required, DisplayName("Address")]
        public string CustAddress { get; set; }

        [Required, DisplayName("City")]
        public string CustCity { get; set; }

        [Required,DisplayName("Province")]
        [RegularExpression(@"^[A-Za-z]{2}$", ErrorMessage = "2 digits code, eg. AB, BC, ON."),]
        public string CustProv { get; set; }

        [Required,DisplayName("Postal Code")]
        [RegularExpression(@"^[A-Za-z]\d[A-Za-z][-\ ]\d[A-Za-z]\d$", ErrorMessage = "Postal code format is T2G 1X5 or T2G-1X5.")]
        public string CustPostal { get; set; }

        [DisplayName("Country")]
        public string CustCountry { get; set; }

        [DisplayName("Home Phone")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Phone format is 780.123.1234 or 403-123-1234")]
        public string CustHomePhone { get; set; }

        [Required, DisplayName("Business Phone")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Phone format is 403.123.1234 or 403-123-1234")]
        public string CustBusPhone { get; set; }

        [Required,DisplayName("Email")]
        public string CustEmail { get; set; }

        [Required, MinLength(5, ErrorMessage = "Username must be at least 5 digits."), MaxLength(50)]
        public string CustUserName { get; set; }

        [Required]
        [DataType(DataType.Password), MinLength(5, ErrorMessage = "Password must be at least 5 digits."), MaxLength(50)]
        public string CustPassword { get; set; }

        [DataType(DataType.Password),Compare(nameof(CustPassword)),DisplayName("Password Confirm")]
        public string PasswordConfirm { get; set; }

        public int? AgentId { get; set; }
        //public string CustUserName { get; set; }
    }
}
