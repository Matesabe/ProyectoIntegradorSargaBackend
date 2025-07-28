using AppLogic.Mapper;
using BusinessLogic.RepositoriesInterfaces.RedemptionInterface;
using BusinessLogic.RepositoriesInterfaces.ReportsInterface;
using SharedUseCase.DTOs.Redemption;
using SharedUseCase.DTOs.Reports;
using SharedUseCase.InterfacesUC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.UseCase.ReportUC
{
    public class GetAllReports : IGetAll<ReportDto>
    {
        private IRepoReport _repo;
        public GetAllReports(IRepoReport repo)
        {
            _repo = repo;
        }
        public IEnumerable<ReportDto> Execute()
        {
            try
            {
                return ReportMapper.ToListDto(_repo.GetAll());
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todos los canjes", ex);
            }
        }
    }
}
