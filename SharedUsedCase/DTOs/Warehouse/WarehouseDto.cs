using SharedUseCase.DTOs.Product;
using SharedUseCase.DTOs.WarehouseStock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedUseCase.DTOs.Warehouse
{
    public record WarehouseDto(int Id,
                              string Name,
                              List<WarehouseStockDto> Stocks)
    {
    }
}
