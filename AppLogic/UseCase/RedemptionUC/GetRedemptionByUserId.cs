using AppLogic.Mapper;
using BusinessLogic.Entities;
using BusinessLogic.RepositoriesInterfaces.RedemptionInterface;
using SharedUseCase.DTOs.Redemption;
using SharedUseCase.InterfacesUC.Redemption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.UseCase.RedemptionUC
{
    public class GetRedemptionByUserId : IGetRedemptionByUserId<RedemptionDto>
    {
        IRepoRedemption _repo;
        public GetRedemptionByUserId(IRepoRedemption repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo), "El repositorio no puede ser nulo");
        }
        public IEnumerable<RedemptionDto> Execute(int userId)
        {
            try
            {
                if (userId <= 0)
                {
                    throw new ArgumentException("El ID de usuario debe ser mayor que cero", nameof(userId));
                }
                var redemptions = _repo.GetRedemptionByUserId(userId);
                if (redemptions.Any())
                {
                    return redemptions.Select(r => RedemptionMapper.ToDto(r)).ToList();
                }
                return Enumerable.Empty<RedemptionDto>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los canjes por ID de usuario: " + ex.Message, ex);
            }
        }
    }
}
