/*
 Author: Rabea
 To search for packages (package id and package start date)
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G4TEWS4_MVC.Models
{
    public class PackageSearchModel
    {
        public int PackageId { get; set; }
        public DateTime? PkgStartDate { get; set; }
    }
}
