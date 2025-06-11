using SharedUseCase.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedUseCase.DTOs.Promotion
{
    public record PromotionDto(
        int Id,
        string Description,
        string Type,
        bool IsActive,
        int AmountPerPoint,
        IEnumerable<SubProductDto> PromotionProducts,
        int PointsPerProducts,
        DateTime PromotionDateStart,
        DateTime PromotionDateEnd,
        int PointsPerDate,
        double MinimalAmount,
        int RecurrenceValue,
        int PointsPerRecurrence
    )
    {
    }
}
