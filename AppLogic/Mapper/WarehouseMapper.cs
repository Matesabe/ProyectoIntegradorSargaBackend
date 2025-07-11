using BusinessLogic.Entities;
using BusinessLogic.VO;
using SharedUseCase.DTOs.User;
using SharedUseCase.DTOs.Warehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Mapper
{
    public static class WarehouseMapper
    {
        public static Warehouse FromDto(WarehouseDto dto)
        {
            return new Warehouse(dto.Id,
                                    dto.Name,
                                    WarehouseStockMapper.FromListDto(dto.Stocks));
        }

        public static WarehouseDto ToDto(Warehouse warehouse)
        {
            return new WarehouseDto(warehouse.Id,
                                    warehouse.Name,
                                    WarehouseStockMapper.ToListDto(warehouse.Stocks));
        }


        public static IEnumerable<WarehouseDto> ToListDto(IEnumerable<Warehouse> warehouses)
        {
            List<WarehouseDto> warehousesDto = new List<WarehouseDto>();
            foreach (var item in warehouses)
            {
                warehousesDto.Add(ToDto(item));
            }
            return warehousesDto;
        }

    }
}
