/*
Purpose: Get customer profile and enable authentication
*/
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
//using TeamLID.TravelExperts.Repository.Domain;
using G4TEWS4_Data;

namespace G4TEWS4_MVC.Models.DataManager
{
    public class CustomerProfileManager
    {

        ///// <summary>
        /// Find a certain customer using id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Customers object.</returns>
        public static Customer Find(int id)
        {
            var context = new TEContext();

            // find the domain entity with this context that has the same id as 
            // the entity passed
            var customer = context.Customers.
                Include(agt => agt.Agent).
                SingleOrDefault(ast => ast.CustomerId == id);

            return customer;

        }

        ////
        /// <summary>
        /// Add a new customer to database.
        /// </summary>
        /// <param name="newCust">Customers object need to be added.</param>
        /// <returns>A bool indicate if added successfully.</returns>
        public static async Task<bool> Add(Customer newCust)
        {
            bool isSucceed = false;
            var context = new TEContext();
            context.Customers.Add(newCust);
            try
            {
                int i = await context.SaveChangesAsync();
                if (i > 0)
                    isSucceed = true;
            }
            catch (Exception e)
            {
                throw e;
            }

            return isSucceed;
        }

        /// <summary>
        /// Compares the login.
        /// </summary>
        /// <returns>The login.</returns>
        /// <param name="username">Username.</param>
        /// <param name="password">Password.</param>
        public static Customer CompareLogin(string username, string password)
        {

            var context = new TEContext();
            // use username to find a customer, if cannot find one, return null
            var cust = context.Customers.SingleOrDefault(c => c.CustUserName == username);
            if (cust == null)
                return null;
            // if find a customer, compare password
            if (cust.CustPassword == password)
                return cust;
            else
                return null;
        }

    }
}
