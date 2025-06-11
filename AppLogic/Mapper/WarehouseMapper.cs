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
                                    SubproductMapper.FromListDtoToProduct(dto.SubProducts));
        }

        public static WarehouseDto ToDto(Warehouse usuario)
        {
            return new WarehouseDto(usuario.Id,
                                    usuario.Name,
                                    SubproductMapper.ToListDto(usuario.SubProducts).ToList());
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
