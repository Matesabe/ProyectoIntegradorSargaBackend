using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class RedemptionPromotionDate : RedemptionPromotion
    {
        public DateTime Date { get; set; }
        public int PointsPerDate { get; set; }
    }
}
