/*
Author: Rabea
Purpose: Retrieve packages for packages page
*/
using System;
using System.Collections.Generic;
using System.Linq;
using G4TEWS4_Data;

namespace G4TEWS4_MVC.Models.DataManager
{
    public class PackagesDataManager
    {
        public static List<Package> GetAll()
        {
            var context = new TEContext();

            var packages = context.Packages.ToList();

            return packages;
        }

    }
}
