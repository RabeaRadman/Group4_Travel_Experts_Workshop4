using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G4TEWS4_Data
{
    public static class CustomerPackageMgr
    {
        private static TEContext _context = new();
        public static List<Package> GetAllPackages()
        {
            List<Package> allPackages = _context.Packages.ToList();
            return allPackages;
        }
        /// <summary>
        /// Author: Gilmar Castillo
        /// 
        /// Method to return the full name of a customer
        /// 
        /// </summary>
        /// <param name="custId">Customer Id</param>
        /// <returns>Customer full name</returns>
        public static string CustFullName(int custId)
        {
            string custFullName;
            Customer cust = _context.Customers.Find(custId);
            custFullName = cust.CustFirstName + ' ' + cust.CustLastName;
            return custFullName;
        }
    }
}
