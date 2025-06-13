using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class PurchasePromotionRecurrence : PurchasePromotion
    {
        public int RecurrenceValue { get; set; }
        public int PointsPerRecurrence { get; set; }
        public override string Description { get; set; }
        public override string Type { get; set; }
        public override bool IsActive { get; set; }

        public PurchasePromotionRecurrence()
        {
            Type = "Recurrence";
            IsActive = true; // Default value, can be set later if needed
        }
        public PurchasePromotionRecurrence(int id, string description, int recurrenceValue, int pointsPerRecurrence)
            : base(id)
        {
            Description = description;
            RecurrenceValue = recurrenceValue;
            PointsPerRecurrence = pointsPerRecurrence;
            Type = "Recurrence";
            IsActive = true; // Default value, can be set later if needed
        }

        public override int generatePoints(Purchase purchase)
        {
            return calculatePoints(purchase.Client.RecurrenceCount);
        }

        public int calculatePoints(int recurrenceCount)
        {
            if (recurrenceCount < 0)
            {
                throw new ArgumentException("Recurrence count cannot be negative");
            }
            return recurrenceCount * PointsPerRecurrence;
        }
    }
}
