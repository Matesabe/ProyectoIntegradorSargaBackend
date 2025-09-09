using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class PurchasePromotionProducts : PurchasePromotion 
    {
        public List<ProductPromotion> ProductPromotions { get; set; } = new List<ProductPromotion>();
        public int PointsPerProducts { get; set; }
        public override string Description { get; set; }
        public override string Type { get; set; }
        public override bool IsActive { get; set; }
        public PurchasePromotionProducts() { }
        public PurchasePromotionProducts(int id) : base(id) { }
        public PurchasePromotionProducts(
            int id,
            string description,
            List<ProductPromotion> productPromotions,
            int pointsPerProducts,
            bool isActive
        ) : base(id)
        {
            Description = description;
            ProductPromotions = productPromotions ?? new List<ProductPromotion>(); // <- nunca queda null
            PointsPerProducts = pointsPerProducts;
            Type = "Products";
            IsActive = isActive;
        }

        public override int generatePoints(Purchase purchase)
        {
            return calculatePoints(purchase.PurchaseProducts);
        }

        public int calculatePoints(List<PurchaseProduct> Products)
        {
            if (ProductPromotions == null || ProductPromotions.Count == 0)
                return 0;
            // Verifica si todos los productos de la promoción están en subProducts
            var ProductsSet = new HashSet<int>(Products.Select(p => p.ProductId));
            bool todosPresentes = ProductPromotions.All(pp => ProductsSet.Contains(pp.ProductId));
            return todosPresentes ? PointsPerProducts : 0;
        }
    }
}
