using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class Redemption
    {
        public int Id { get; set; }
        public Client? Client { get; set; }
        public int ClientId { get; set; } // Foreign key for Client
        public int PointsUsed { get; set; }
        public DateTime RedemptionDate { get; set; } = DateTime.Now;
        public Redemption() { }
        public Redemption(int id, int clientId, int pointsUsed)
        {
            Id = id;
            ClientId = clientId;
            PointsUsed = pointsUsed;
        }
    }
}
