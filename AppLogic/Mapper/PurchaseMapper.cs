using BusinessLogic.Entities;
using SharedUseCase.DTOs.Product;
using SharedUseCase.DTOs.Purchase;
using SharedUseCase.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Mapper
{
    public class PurchaseMapper
    {
        public static Purchase FromDto(PurchaseDto dto)
        {
            return new Purchase(dto.Id,
                                (Client)UserMapper.FromDto(dto.Client), 
                                dto.Amount,
                                dto.PointsGenerated,
                                ProductMapper.FromListDtoToProduct(dto.Products) // SubProducts will be set later
            );  
        }

        public static PurchaseDto ToDto(Purchase purchase)
        {
            return new PurchaseDto(purchase.Id,
                                   UserMapper.ToDto(purchase.Client), // Assuming Client has an Id property
                                   purchase.Amount,
                                   purchase.PointsGenerated,
                                   ProductMapper.ToListDto(purchase.Products)
            );
        }

        public static IEnumerable<PurchaseDto> ToListDto(IEnumerable<Purchase> purchases)
        {
            List<PurchaseDto> purchasesDto = new List<PurchaseDto>(); ;
            foreach (var item in purchases)
            {
                purchasesDto.Add(ToDto(item));
            }
            return purchasesDto;
        }

        public static IEnumerable<Purchase> FromListDtoToPurchase(IEnumerable<PurchaseDto> purchasesDto)
        {
            List<Purchase> purchases = new List<Purchase>();
            foreach (var item in purchasesDto)
            {
                purchases.Add(FromDto(item));
            }
            return purchases;
        }
    }
}
