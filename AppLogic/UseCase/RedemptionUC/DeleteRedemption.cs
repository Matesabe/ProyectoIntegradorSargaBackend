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
    public class DeleteRedemption : IRemove<RedemptionDto>
    {
        private IRepoRedemption _repo;
        public DeleteRedemption(IRepoRedemption repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo), "El repositorio no puede ser nulo");
        }
        public void Execute(RedemptionDto red)
        {
            try
            {
                int id = red.Id;
                if (id <= 0)
                {
                    throw new ArgumentException("El ID debe ser mayor que cero", nameof(id));
                }
                _repo.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el canje: " + ex.Message, ex);
            }
        }
    }
}
