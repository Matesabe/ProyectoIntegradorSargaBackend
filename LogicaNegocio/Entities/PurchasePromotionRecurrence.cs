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
    }
}
