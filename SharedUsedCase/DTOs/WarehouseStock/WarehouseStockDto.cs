using BusinessLogic.Entities;
using SharedUseCase.DTOs.Product;
using SharedUseCase.DTOs.Warehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedUseCase.DTOs.WarehouseStock
{
    public record WarehouseStockDto(int warehouseId, int subProductId, int quantity)
    {
    }
}
