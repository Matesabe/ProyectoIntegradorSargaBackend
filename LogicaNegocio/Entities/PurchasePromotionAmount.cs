using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class PurchasePromotionAmount : PurchasePromotion 
    {
        public PurchasePromotionAmount() { }
        public int AmountPerPoint { get; set; }
        public override string Description { get; set; }
        public override string Type { get; set; }
        public override bool IsActive { get; set; }

        public PurchasePromotionAmount(int id, string description, int amountPerPoint, string type)
            : base(id)
        {
            Description = description;
            AmountPerPoint = amountPerPoint;
            Type = "Amount";
            IsActive = true; // Assuming the promotion is active by default
        }

        public override int generatePoints(Purchase purchase)
        {
            return calculatePoints(purchase.Amount);
        }

        public int calculatePoints(double amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Amount cannot be negative");
            }
            return (int)(amount / AmountPerPoint);
        }

    }
}
