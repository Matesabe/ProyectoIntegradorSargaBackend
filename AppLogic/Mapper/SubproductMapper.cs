using BusinessLogic.Entities;
using BusinessLogic.VO;
using SharedUseCase.DTOs.Product;
using SharedUseCase.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Mapper
{
    public static class SubproductMapper
    {
        public static SubProduct FromDto(SubproductDto dto)
        {
            return new SubProduct(dto.Id,
                               dto.productCode,
                               dto.Name,
                               dto.Price,
                                 dto.Color,
                                    dto.Size,
                                    dto.Images,
                                    dto.Season,
                                    dto.Year
                               );
         
        }

        public static SubproductDto ToDto(SubProduct subproduct)
        {
            return new SubproductDto(
                subproduct.Id, subproduct.productCode, subproduct.Name, subproduct.Price, subproduct.Color, subproduct.Size, subproduct.Season, subproduct.Year, subproduct.Images);
        }


        public static IEnumerable<SubproductDto> ToListDto(IEnumerable<SubProduct> products)
        {
            List<SubproductDto> productsDto = new List<SubproductDto>();
            foreach (var item in products)
            {
                productsDto.Add(ToDto(item));
            }
            return productsDto;
        }

        public static IEnumerable<SubProduct> FromListDtoToProduct(IEnumerable<SubproductDto> productsDto)
        {
            List<SubProduct> products = new List<SubProduct>();
            foreach (var item in productsDto)
            {
                products.Add(FromDto(item));
            }
            return products;
        }
    }
}
