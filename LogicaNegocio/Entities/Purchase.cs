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
        public Purchase(int id, Client client, double amount, int pointsGenerated, IEnumerable<Product> products)
        {
            Id = id;
            Client = client;
            Amount = amount; 
            Products = products;
            PointsGenerated = pointsGenerated;
        }
        public int Id { get; set; }
        public Client Client { get; set; }
        public double Amount { get; set; }
        public int PointsGenerated { get; set; }
        public IEnumerable<Product> Products { get; set; } 
        public bool AppliesPromotions { get; set; } = false; // Indicates if promotions apply to this purchase

        public void AddProduct(Product product)
        {
            if (Products == null)
            {
                Products = new List<Product>();
            }
            ((List<Product>)Products).Add(product);
        }
        public void RemoveSubProduct(Product Product)
        {
            if (Products != null && Products.Contains(Product))
            {
                ((List<Product>)Products).Remove(Product);
            }
        }
        public void ClearSubProducts()
        {
            if (Products != null)
            {
                ((List<SubProduct>)Products).Clear();
            }
        }
    }
}
