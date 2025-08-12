using BusinessLogic.Entities;
using SharedUseCase.DTOs.Purchase;
using SharedUseCase.DTOs.Redemption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Mapper
{
    public class RedemptionMapper
    {
        public static Redemption FromDto(RedemptionDto dto)
        {
            return new Redemption(dto.Id,
                                   (Client)UserMapper.FromDto(dto.Client),
                                   dto.PointsUsed
            );
        }

        public static RedemptionDto ToDto(Redemption redemption)
        {
            return new RedemptionDto(
                redemption.Id,
                UserMapper.ToDto(redemption.Client), 
                redemption.PointsUsed
            );
        }

        public static IEnumerable<RedemptionDto> ToListDto(IEnumerable<Redemption> redemptions)
        {
            List<RedemptionDto> redemptionsDto = new List<RedemptionDto>(); ;
            foreach (var item in redemptions)
            {
                redemptionsDto.Add(ToDto(item));
            }
            return redemptionsDto;
        }

        public static IEnumerable<Redemption> FromListDtoToRedemption(IEnumerable<RedemptionDto> redemptionsDto)
        {
            List<Redemption> redemptions = new List<Redemption>();
            foreach (var item in redemptionsDto)
            {
                redemptions.Add(FromDto(item));
            }
            return redemptions;
        }
    }
}
