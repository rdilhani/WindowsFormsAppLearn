using System;
using System.Collections.Generic;

namespace WindowsFormsAppLearn
{
    public class Sale
    {
        public string SaleID { get; set; }
        public string CustomerID { get; set; }
        public DateTime Date { get; set; }
        public List<SaleItem> Items { get; set; }
        public double TotalAmount { get; set; }

        public Sale()
        {
            Items = new List<SaleItem>();
        }
    }

    public class SaleItem
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double SubTotal => Quantity * UnitPrice;
    }
}
