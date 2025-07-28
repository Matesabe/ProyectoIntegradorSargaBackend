using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class PurchaseProduct
    {
        PurchaseProduct() { }
        public PurchaseProduct(int purchaseId, int productId, int quantity)
        {
            PurchaseId = purchaseId;
            ProductId = productId;
            Quantity = quantity;
        }
        public int PurchaseId { get; set; }
        public Purchase Purchase { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; } // Opcional
    }

}
