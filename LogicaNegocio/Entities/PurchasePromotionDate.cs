using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class PurchasePromotionDate : PurchasePromotion
    {
        public PurchasePromotionDate() { }
        public DateTime PromotionDateStart { get; set; }
        public DateTime PromotionDateEnd { get; set; }
        public int PointsPerDate { get; set; }
        public double MinimalAmount { get; set; } = 0; // Default value, can be set later if needed
        public override string Description { get; set; }
        public override string Type { get; set; }
        public override bool IsActive { get; set; }

        public PurchasePromotionDate(int id, DateTime dateStart, DateTime dateEnd, int pointsPerDate, double minimalAmount, string description)
            : base(id)
        {
            PromotionDateStart = dateStart;
            PromotionDateEnd = dateEnd;
            PointsPerDate = pointsPerDate;
            MinimalAmount = minimalAmount;
            Type = "Date";
            Description = description;
            IsActive = true; // Assuming the promotion is active by default
        }

        public override int generatePoints(Purchase purchase)
        {
            if (DateTime.Now.Date >= PromotionDateStart && DateTime.Now.Date <= PromotionDateEnd)
            {
                if (purchase.Amount < MinimalAmount)
                {
                    return 0;
                }
                return PointsPerDate;
            }
            return 0;
        }

    }
}
