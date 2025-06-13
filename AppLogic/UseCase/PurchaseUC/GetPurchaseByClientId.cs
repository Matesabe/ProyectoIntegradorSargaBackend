using AppLogic.Mapper;
using BusinessLogic.RepositoriesInterfaces.PurchaseInterface;
using SharedUseCase.DTOs.Purchase;
using SharedUseCase.InterfacesUC.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.UseCase.PurchaseUC
{
    public class GetPurchaseByClientId : IGetPurchaseByClientId<PurchaseDto>
    {
        private IRepoPurchase _repo;
        public GetPurchaseByClientId(IRepoPurchase repo)
        {
            _repo = repo;
        }
        public IEnumerable<PurchaseDto> Execute(int clientId)
        {
            try
            {
                if (clientId <= 0)
                {
                    throw new ArgumentException("El ID del cliente debe ser mayor que cero", nameof(clientId));
                }
                var purchases = _repo.GetPurchaseByClientId(clientId);
                return purchases.Select(p => PurchaseMapper.ToDto(p));
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las compras por ID de cliente: " + ex.Message, ex);
            }
    }
}
}
