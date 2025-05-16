using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class RedemptionPromotionProducts : RedemptionPromotion
    {
        public List<Product> Products { get; set; } = new();
        public int PointsPerProducts { get; set; }
    }
}
