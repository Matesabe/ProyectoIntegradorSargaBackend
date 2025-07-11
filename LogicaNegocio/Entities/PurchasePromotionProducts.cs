using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class PurchasePromotionProducts : PurchasePromotion 
    {
        public IEnumerable<SubProduct> PromotionProducts { get; set; }
        public int PointsPerProducts { get; set; }
        public override string Description { get; set; }
        public override string Type { get; set; }
        public override bool IsActive { get; set; }
        public PurchasePromotionProducts() { }
        public PurchasePromotionProducts(int id) : base(id) { }
        public PurchasePromotionProducts(int id, string description, IEnumerable<SubProduct> promotionProducts, int pointsPerProducts)
            : base(id)
        {
            Description = description;
            PromotionProducts = promotionProducts;
            PointsPerProducts = pointsPerProducts;
            Type = "Products";
            IsActive = true;
        }

        public override int generatePoints(Purchase purchase)
        {
            return calculatePoints(purchase.Products);
        }

        public int calculatePoints(IEnumerable<Product> Products)
        {
            // Verifica si todos los productos de la promoción están en subProducts
            var ProductsSet = new HashSet<int>(Products.Select(p => p.Id));
            bool todosPresentes = PromotionProducts.All(pp => ProductsSet.Contains(pp.Id));
            return todosPresentes ? PointsPerProducts : 0;
        }
    }
}
