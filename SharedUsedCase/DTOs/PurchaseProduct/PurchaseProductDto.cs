using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedUseCase.DTOs.PurchaseProduct
{
    public record PurchaseProductDto(int purchaseId, int productId, int quantity)
    {
    }
}
