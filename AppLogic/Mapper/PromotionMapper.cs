using BusinessLogic.Entities;
using SharedUseCase.DTOs.Product;
using SharedUseCase.DTOs.Promotion;
using SharedUseCase.DTOs.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Mapper
{
    public static class PromotionMapper
    {
        public static PurchasePromotion FromDto(PromotionDto dto)
        {
            if (dto.Type=="Amount")
            {
                return new PurchasePromotionAmount(
                    dto.Id,
                    dto.Description,
                    dto.AmountPerPoint,
                    dto.Type,
                    dto.IsActive 
                );
            }
            if (dto.Type=="Date")
            {
                return new PurchasePromotionDate(
                    dto.Id,
                    dto.PromotionDateStart,
                    dto.PromotionDateEnd,
                    dto.PointsPerDate,
                    dto.MinimalAmount,
                    dto.Description,
                    dto.IsActive
                );
            }
            if (dto.Type=="Products")
            {
                Console.WriteLine($"DTO.Products: {dto.PromotionProducts?.Count ?? 0}");
                return new PurchasePromotionProducts(
                    dto.Id,
                    dto.Description,
                    ProductPromotionMapper.FromDtoList(dto.PromotionProducts) ?? new List<ProductPromotion>(),
                    dto.PointsPerProducts,
                    dto.IsActive
                );
            }
            if (dto.Type == "Recurrence")
            {
                return new PurchasePromotionRecurrence(
                    dto.Id,
                    dto.Description,
                    dto.RecurrenceValue,
                    dto.PointsPerRecurrence,
                    dto.IsActive 
                );
            }
            throw new Exception("Tipo de promoción no reconocido: " + dto.Type);
        }

        public static PromotionDto ToDto(PurchasePromotion promotion)
        {
            if (promotion.Type == "Amount")
            {
                return new PromotionDto(
                    promotion.Id,
                    promotion.Description,
                    promotion.Type,
                    promotion.IsActive, // Use IsActive instead of PointsGenerated
                    (promotion as PurchasePromotionAmount).AmountPerPoint,
                    null, // PromotionProducts is not applicable for Amount promotions
                    0, // PointsPerProducts is not applicable for Amount promotions
                    default(DateTime), // Use default(DateTime) instead of null
                    default(DateTime),
                    0, // PointsPerDate is not applicable for Amount promotions
                    0, // MinimalAmount is not applicable for Amount promotions
                    0, // RecurrenceValue is not applicable for Amount promotions
                    0 // PointsPerRecurrence is not applicable for Amount promotions
                );
            }
            if (promotion.Type == "Date")
            {
                return new PromotionDto(
                    promotion.Id,
                    promotion.Description,
                    promotion.Type,
                    promotion.IsActive, 
                    0,
                    null, 
                    0, 
                    (promotion as PurchasePromotionDate).PromotionDateStart,
                    (promotion as PurchasePromotionDate).PromotionDateEnd,
                    (promotion as PurchasePromotionDate).PointsPerDate, 
                    (promotion as PurchasePromotionDate).MinimalAmount,
                    0, // RecurrenceValue is not applicable for Date promotions
                    0 // PointsPerRecurrence is not applicable for Date promotions
                );
            }
            
            if (promotion.Type == "Products")
            {
                return new PromotionDto(
                    promotion.Id,
                    promotion.Description,
                    promotion.Type,
                    promotion.IsActive, // Use IsActive instead of PointsGenerated
                    0,
                    ProductPromotionMapper.ToDtoList((promotion as PurchasePromotionProducts).ProductPromotions), 
                    (promotion as PurchasePromotionProducts).PointsPerProducts, 
                    default(DateTime), // Use default(DateTime) instead of null
                    default(DateTime),
                    0, // PointsPerDate is not applicable for Amount promotions
                    0, // MinimalAmount is not applicable for Amount promotions
                    0, // RecurrenceValue is not applicable for Products promotions
                    0 // PointsPerRecurrence is not applicable for Products promotions
                );
            }
            if (promotion.Type == "Recurrence")
            {
                return new PromotionDto(
                    promotion.Id,
                    promotion.Description,
                    promotion.Type,
                    promotion.IsActive, // Use IsActive instead of PointsGenerated
                    0,
                    null, 
                    0, 
                    default(DateTime), // Use default(DateTime) instead of null
                    default(DateTime),
                    0, // PointsPerDate is not applicable for Amount promotions
                    0, // MinimalAmount is not applicable for Amount promotions
                    (promotion as PurchasePromotionRecurrence).RecurrenceValue,
                    (promotion as PurchasePromotionRecurrence).PointsPerRecurrence
                    );
            }
            throw new Exception("Tipo de promoción no reconocido: " + promotion.Type);
        }


        public static IEnumerable<PromotionDto> ToListDto(IEnumerable<PurchasePromotion> promotions)
        {
            List<PromotionDto> promotionsDto = new List<PromotionDto>();
            foreach (var item in promotions)
            {
                promotionsDto.Add(ToDto(item));
            }
            return promotionsDto;
        }

        public static IEnumerable<PurchasePromotion> FromListDtoToProduct(IEnumerable<PromotionDto> purchasesDto)
        {
            List<PurchasePromotion> purchases = new List<PurchasePromotion>();
            foreach (var item in purchasesDto)
            {
                purchases.Add(FromDto(item));
            }
            return purchases;
        }
    }
}
