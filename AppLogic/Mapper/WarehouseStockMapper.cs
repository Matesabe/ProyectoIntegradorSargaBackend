using BusinessLogic.Entities;
using SharedUseCase.DTOs.Warehouse;
using SharedUseCase.DTOs.WarehouseStock;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Mapper
{
    public class WarehouseStockMapper
    {
        public static WarehouseStock FromDto(WarehouseStockDto dto)
        {
            return new WarehouseStock(dto.warehouseId, dto.subProductId, dto.quantity); 
        }

        public static WarehouseStockDto ToDto(WarehouseStock stock)
        {
            return new WarehouseStockDto(stock.WarehouseId,
                                    stock.SubProductId,
                                    stock.Quantity);
            
        }

        public static List <WarehouseStock> FromListDto(List<WarehouseStockDto> dtos)
        {
            List<WarehouseStock> stocks = new List<WarehouseStock>();
            foreach (var item in dtos)
            {
                stocks.Add(FromDto(item));
            }
            return stocks;
        }
        public static List<WarehouseStockDto> ToListDto(IEnumerable<WarehouseStock> stocks)
        {
            List<WarehouseStockDto> stocksDto = new List<WarehouseStockDto>();
            foreach (var item in stocks)
            {
                stocksDto.Add(ToDto(item));
            }
            return stocksDto;
        }
    }
}
