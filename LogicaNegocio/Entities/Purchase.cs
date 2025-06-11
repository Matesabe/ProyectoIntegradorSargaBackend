using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class Purchase
    {
        public Purchase() { }
        public Purchase(int id, Client client, double amount, int pointsGenerated, IEnumerable<SubProduct> subProducts)
        {
            Id = id;
            Client = client;
            Amount = amount; 
            SubProducts = subProducts;
            PointsGenerated = pointsGenerated;
        }
        public int Id { get; set; }
        public Client Client { get; set; }
        public double Amount { get; set; }
        public int PointsGenerated { get; set; }
        public Warehouse Warehouse { get; set; } // Assuming a Purchase is associated with a Warehouse
        public IEnumerable<SubProduct> SubProducts { get; set; } 
        public bool AppliesPromotions { get; set; } = false; // Indicates if promotions apply to this purchase

        public void AddSubProduct(SubProduct subProduct)
        {
            if (SubProducts == null)
            {
                SubProducts = new List<SubProduct>();
            }
            ((List<SubProduct>)SubProducts).Add(subProduct);
        }
        public void RemoveSubProduct(SubProduct subProduct)
        {
            if (SubProducts != null && SubProducts.Contains(subProduct))
            {
                ((List<SubProduct>)SubProducts).Remove(subProduct);
            }
        }
        public void ClearSubProducts()
        {
            if (SubProducts != null)
            {
                ((List<SubProduct>)SubProducts).Clear();
            }
        }
    }
}
