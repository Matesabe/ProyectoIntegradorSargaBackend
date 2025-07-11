using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class Warehouse
    {
        protected Warehouse()
        {
        }
        public Warehouse(int id, string name, List<WarehouseStock> enumerable)
        {
            Id = id;
            Name = name;
            Stocks = enumerable ?? new List<WarehouseStock>();
        }


        public int Id { get; set; }
        public string Name { get; set; }
        public List<WarehouseStock> Stocks { get; set; } = new();
    }
}
