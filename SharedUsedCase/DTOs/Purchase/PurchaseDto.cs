using BusinessLogic.Entities;
using SharedUseCase.DTOs.Product;
using SharedUseCase.DTOs.Promotion;
using SharedUseCase.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedUseCase.DTOs.Purchase
{
    public record PurchaseDto(int Id,
        UserDto Client,
        double Amount,
        int PointsGenerated,
        List<ProductDto> Products)
    {

    }
}
