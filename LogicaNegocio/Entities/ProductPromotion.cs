using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class ProductPromotion
    {
        public ProductPromotion(int productId, int purchasePromotionProductsId)
        {
            ProductId = productId;
            PurchasePromotionProductsId = purchasePromotionProductsId;
        }
        public ProductPromotion() { }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int PurchasePromotionProductsId { get; set; }
        public PurchasePromotionProducts Promotion { get; set; }
    }
}
