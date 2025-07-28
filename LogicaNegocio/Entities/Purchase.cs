using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class Purchase
    {
        public Purchase() { }
        public Purchase(int id, Client client, double amount, int pointsGenerated, List<PurchaseProduct> products, DateTime date)
        {
            Id = id;
            Client = client;
            Amount = amount; 
            PurchaseProducts = products;
            PointsGenerated = pointsGenerated;
            Date = date;
        }
        public int Id { get; set; }
        public Client Client { get; set; }
        public double Amount { get; set; }
        public int PointsGenerated { get; set; }
        public List<PurchaseProduct> PurchaseProducts { get; set; } 
        public bool AppliesPromotions { get; set; } = true; // Indicates if promotions apply to this purchase
        public DateTime Date { get; set; } 

    }
}
