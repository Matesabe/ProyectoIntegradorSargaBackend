using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class Redemption
    {
        public int Id { get; set; }
        public Client Client { get; set; } 
        public double Amount { get; set; }
        public int PointsUsed { get; set; }
        public List<SubProduct> SubProducts { get; set; } = new();
        //public List<RedemptionPromotion> Promotions { get; set; } = new();
        public Redemption() { }
        public Redemption(int id, Client client, double amount, int pointsUsed, IEnumerable<SubProduct> subProducts)
        {
            Id = id;
            Client = client;
            Amount = amount;
            PointsUsed = pointsUsed;
            SubProducts = subProducts.ToList();
        }
    }
}
