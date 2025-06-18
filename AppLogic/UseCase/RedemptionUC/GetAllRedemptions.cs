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
        private IRepoRedemption _repo;
        public GetAllRedemptions(IRepoRedemption repo)
        {
            _repo = repo;
        }
        public IEnumerable<RedemptionDto> Execute()
        {
            try
            {
                return RedemptionMapper.ToListDto(_repo.GetAll());
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todos los canjes", ex);
            }
        }
    }
}
