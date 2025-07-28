using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Mapper
{
    public class ProductPromotionMapper
    {
        public static SharedUseCase.DTOs.Purchase.ProductPromotionDto ToDto(BusinessLogic.Entities.ProductPromotion productPromotion)
        {
            return new SharedUseCase.DTOs.Purchase.ProductPromotionDto(
                productPromotion.ProductId,
                productPromotion.PurchasePromotionProductsId
            );
        }
        public static BusinessLogic.Entities.ProductPromotion FromDto(SharedUseCase.DTOs.Purchase.ProductPromotionDto productPromotionDto)
        {
            return new BusinessLogic.Entities.ProductPromotion
            {
                ProductId = productPromotionDto.ProductId,
                PurchasePromotionProductsId = productPromotionDto.PurchasePromotionProductsId
            };
        }

        public static List<SharedUseCase.DTOs.Purchase.ProductPromotionDto> ToDtoList(List<BusinessLogic.Entities.ProductPromotion> productPromotions)
        {
            return productPromotions.Select(ToDto).ToList();
        }

        public static List<BusinessLogic.Entities.ProductPromotion> FromDtoList(List<SharedUseCase.DTOs.Purchase.ProductPromotionDto> productPromotionDtos)
        {
            return productPromotionDtos.Select(FromDto).ToList();
        }
    }
}
