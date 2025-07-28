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
    public class AddReport : IAdd<ReportDto>
    {
        private IRepoReport _repo;
        public AddReport(IRepoReport repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo), "El repositorio no puede ser nulo");
        }
        public int Execute(ReportDto obj)
        {
            try
            {
                if (obj == null)
                {
                    throw new ArgumentNullException(nameof(obj), "El objeto no puede ser nulo");
                }
                
                return _repo.Add(ReportMapper.FromDto(obj));
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el reporte: " + ex.Message, ex);
            }
        }
    }
}
