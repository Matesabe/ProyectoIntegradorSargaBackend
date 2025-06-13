using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public abstract class PurchasePromotion
    {
        public int Id { get; set; }
        public abstract string Description { get; set; }
        //public abstract int PointsGenerated { get; set; }
        public abstract string Type { get; set; }
        public abstract bool IsActive { get; set; }
        public abstract int generatePoints(Purchase purchase);
        public PurchasePromotion() { }
        public PurchasePromotion(int id)
        {
            Id = id;
        }
    }
}
