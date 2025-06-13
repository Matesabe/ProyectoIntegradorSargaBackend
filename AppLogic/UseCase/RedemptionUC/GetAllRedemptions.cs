using AppLogic.Mapper;
using BusinessLogic.Entities;
using BusinessLogic.RepositoriesInterfaces.RedemptionInterface;
using SharedUseCase.DTOs.Redemption;
using SharedUseCase.InterfacesUC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.UseCase.RedemptionUC
{
    public class GetAllRedemptions : IGetAll<RedemptionDto>
    {
        IRepoRedemption _repo;
        public GetAllRedemptions(IRepoRedemption repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo), "El repositorio de canjes no puede ser nulo");
        }
        public IEnumerable<RedemptionDto> Execute()
        {
            try
            {
                var redemptions = _repo.GetAll();
                if (redemptions == null || !redemptions.Any())
                {
                    throw new Exception("No se encontraron canjes");
                }
                return RedemptionMapper.ToListDto(redemptions);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todos los canjes", ex);
            }
        }
    }
}
