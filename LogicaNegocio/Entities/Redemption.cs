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
        public Client Client { get; set; } 
        public int PointsUsed { get; set; }
        public Redemption() { }
        public Redemption(int id, Client client, int pointsUsed)
        {
            Id = id;
            Client = client;
            PointsUsed = pointsUsed;
        }
    }
}
