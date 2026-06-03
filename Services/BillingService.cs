using System.Collections.Generic;
using WindowsFormsApp1.Database;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Services
{
    /// <summary>
    /// Billing business logic - Service Layer
    /// BillingForm seedha database se baat nahi karta - Abstraction
    /// </summary>
    public class BillingService
    {
        // SalesRepository - Encapsulation
        private readonly SalesRepository _salesRepo = new SalesRepository();

        /// <summary>
        /// Bill save karta hai - Validation check karta hai phir SalesRepository se save karta hai
        /// Returns: null = success, string = error message
        /// </summary>
        public string SaveBill(int customerId, decimal total, List<SaleItem> items)
        {
            // Step 1 - Validation check
            if (items == null || items.Count == 0)
                return "Bill mein koi item nahi!";
            if (customerId <= 0)
                return "Customer select karo!";
            // Step 2 - Database mein save karo
            _salesRepo.SaveSale(customerId, total, items);
            return null;     // null = success
        }
    }
}