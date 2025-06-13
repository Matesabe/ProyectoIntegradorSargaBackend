using BusinessLogic.Entities;
using SharedUseCase.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Mapper
{
    public class ProductMapper
    {
        public static Product FromDto(ProductDto dto)
        {
            return new Product(
                dto.id,
                dto.productCode,
                dto.name,
                dto.price,
                dto.season,
                dto.year
            );

        }

        public static ProductDto ToDto(Product product)
        {
            return new ProductDto(product.Id,
                                  product.productCode,
                                  product.Name,
                                  product.Price,
                                  product.Season,
                                  product.Year
            );
        }


        public static IEnumerable<ProductDto> ToListDto(IEnumerable<Product> products)
        {
            List<ProductDto> productsDto = new List<ProductDto>();
            foreach (var item in products)
            {
                productsDto.Add(ToDto(item));
            }
            return productsDto;
        }

        public static IEnumerable<Product> FromListDtoToProduct(IEnumerable<ProductDto> productsDto)
        {
            List<Product> products = new List<Product>();
            foreach (var item in productsDto)
            {
                products.Add(FromDto(item));
            }
            return products;
        }
    }
}
