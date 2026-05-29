using System.Collections.Generic;
using WindowsFormsApp1.Database;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Services
{
    public class BillingService
    {
        private readonly SalesRepository _salesRepo = new SalesRepository();

        public string SaveBill(int customerId, decimal total, List<SaleItem> items)
        {
            if (items == null || items.Count == 0)
                return "Bill mein koi item nahi!";
            if (customerId <= 0)
                return "Customer select karo!";

            _salesRepo.SaveSale(customerId, total, items);
            return null;
        }
    }
}