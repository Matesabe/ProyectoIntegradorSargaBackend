using SharedUseCase.DTOs.Product;
using SharedUseCase.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedUseCase.DTOs.Redemption
{
    public record RedemptionDto (
        int Id,
        UserDto Client,
        double Amount,
        int PointsUsed,
        IEnumerable<SubProductDto> SubProducts
        )
    {
    }
}
