using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class Purchase
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public int PointsGenerated { get; set; }
        public List<Product> Products { get; set; } = new();
        public List<PurchasePromotion> Promotions { get; set; } = new();
    }
}
