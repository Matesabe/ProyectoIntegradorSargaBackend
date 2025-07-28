using BusinessLogic.Entities;
using SharedUseCase.DTOs.PurchaseProduct;
using SharedUseCase.DTOs.Warehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Mapper
{
    public class PurchaseProductMapper
    {
        public static PurchaseProduct FromDto(PurchaseProductDto dto)
        {
            return new PurchaseProduct(dto.purchaseId, dto.productId, dto.quantity);
        }

        public static PurchaseProductDto ToDto(PurchaseProduct purchaseProduct)
        {
            return new PurchaseProductDto(purchaseProduct.PurchaseId, purchaseProduct.ProductId, purchaseProduct.Quantity);
        }


        public static List<PurchaseProductDto> ToListDto(List<PurchaseProduct> purchaseProducts)
        {
            List<PurchaseProductDto> purchaseProductDtos = new List<PurchaseProductDto>();
            foreach (var item in purchaseProducts)
            {
                purchaseProductDtos.Add(ToDto(item));
            }
            return purchaseProductDtos;
        }

        public static List<PurchaseProduct> FromListDto(List<PurchaseProductDto> purchaseProductsDto)
        {
            List<PurchaseProduct> purchaseProducts = new List<PurchaseProduct>();
            foreach (var item in purchaseProductsDto)
            {
                purchaseProducts.Add(FromDto(item));
            }
            return purchaseProducts;
        }
    }
}
