using AppLogic.Mapper;
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
    public class AddRedemption : IAdd<RedemptionDto>
    {
        private IRepoRedemption _repo;
        public AddRedemption(IRepoRedemption repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo), "El repositorio no puede ser nulo");
        }
        public int Execute(RedemptionDto obj)
        {
            try
            {
                if (obj == null)
                {
                    throw new ArgumentNullException(nameof(obj), "El objeto no puede ser nulo");
                }
                if (obj.Client == null)
                {
                    throw new ArgumentException("El cliente no puede ser nulo", nameof(obj.Client));
                }
                return _repo.Add(RedemptionMapper.FromDto(obj));
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el canje: " + ex.Message, ex);
            }
        }
    }
}
