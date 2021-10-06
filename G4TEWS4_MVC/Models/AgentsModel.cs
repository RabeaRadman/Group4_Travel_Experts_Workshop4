using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace G4TEWS4_MVC.Models
{
    public class AgentsModel
    {
        [DisplayName("First Name")]
        public string AgtFirstName { get; set; }
        [DisplayName("Initirla")]
        public string AgtMiddleInitial { get; set; }
        [DisplayName("Last Name")]
        public string AgtLastName { get; set; }
        [DisplayName("Business Phone")]
        public string AgtBusPhone { get; set; }
        [DisplayName("Email")]
        public string AgtEmail { get; set; }



    }
}
