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
        public static SubProduct FromDto(SubProductDto dto)
        {
            return new SubProduct(dto.Id,
                                 dto.ProductId, // Assuming ProductId is the same as Id in SubProductDto
                               dto.productCode,
                               dto.Name,
                               dto.Price,
                                 dto.Color,
                                    dto.Size,
                                    dto.Images,
                                    dto.Season,
                                    dto.Year,
                                    dto.genre,
                                    dto.brand,
                                    dto.type
                               );
         
        }

        public static SubProductDto ToDto(SubProduct subproduct)
        {
            return new SubProductDto(
                subproduct.Id, subproduct.ProductId, subproduct.productCode, subproduct.Name, subproduct.Price, subproduct.Color, subproduct.Size, subproduct.Season, subproduct.Year, subproduct.Images, subproduct.Genre, subproduct.Brand, subproduct.Type, null, null, null, null, null);
        }

        public static SubProductDto changeId(SubProductDto subId, SubProductDto sub)
        {
            return new SubProductDto(
                subId.Id,
                sub.ProductId,
                sub.productCode,
                sub.Name,
                sub.Price,
                sub.Color,
                sub.Size,
                sub.Season,
                sub.Year,
                sub.Images,
                sub.genre,
                sub.brand,
                sub.type,
                sub.stockPdelE ?? 0,
                sub.stockCol ?? 0,
                sub.stockPay ?? 0,
                sub.stockPeat ?? 0,
                sub.stockSal ?? 0
            );
        }

        public static IEnumerable<SubProductDto> ToListDto(IEnumerable<SubProduct> products)
        {
            List<SubProductDto> productsDto = new List<SubProductDto>();
            foreach (var item in products)
            {
                productsDto.Add(ToDto(item));
            }
            return productsDto;
        }

        public static IEnumerable<SubProduct> FromListDtoToProduct(IEnumerable<SubProductDto> productsDto)
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
