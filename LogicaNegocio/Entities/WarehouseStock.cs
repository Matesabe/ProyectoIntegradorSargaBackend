using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class WarehouseStock
    {
        public WarehouseStock() { }
        public WarehouseStock(int warehouseId, int subProductId, int quantity)
        {
            WarehouseId = warehouseId;
            SubProductId = subProductId;
            Quantity = quantity;
        }
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
        public int SubProductId { get; set; }
        public SubProduct SubProduct { get; set; }
        public int Quantity { get; set; }
    }
}
