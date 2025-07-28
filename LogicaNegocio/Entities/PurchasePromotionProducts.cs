using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class PurchasePromotionProducts : PurchasePromotion 
    {
        public List<ProductPromotion> ProductPromotions { get; set; }
        public int PointsPerProducts { get; set; }
        public override string Description { get; set; }
        public override string Type { get; set; }
        public override bool IsActive { get; set; }
        public PurchasePromotionProducts() { }
        public PurchasePromotionProducts(int id) : base(id) { }
        public PurchasePromotionProducts(int id, string description, List<ProductPromotion> productPromotions, int pointsPerProducts)
            : base(id)
        {
            Description = description;
            ProductPromotions = productPromotions;
            PointsPerProducts = pointsPerProducts;
            Type = "Products";
            IsActive = true;
        }

        public override int generatePoints(Purchase purchase)
        {
            return calculatePoints(purchase.PurchaseProducts);
        }

        public int calculatePoints(List<PurchaseProduct> Products)
        {
            // Verifica si todos los productos de la promoción están en subProducts
            var ProductsSet = new HashSet<int>(Products.Select(p => p.ProductId));
            bool todosPresentes = ProductPromotions.All(pp => ProductsSet.Contains(pp.ProductId));
            return todosPresentes ? PointsPerProducts : 0;
        }
    }
}
