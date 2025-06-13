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
    public class GetByIdRedemption : IGetById<RedemptionDto>
    {
        private IRepoRedemption _repo;
        public GetByIdRedemption(IRepoRedemption repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo), "El repositorio no puede ser nulo");
        }

        public RedemptionDto Execute(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new ArgumentException("El ID debe ser mayor que cero", nameof(id));
                }
                var redemption = _repo.GetById(id);
                if (redemption == null)
                {
                    throw new KeyNotFoundException($"No se encontró un canje con el ID {id}");
                }
                return RedemptionMapper.ToDto(redemption);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el canje por ID: " + ex.Message, ex);
            }
        }
    }
}
