using AppLogic.Mapper;
using BusinessLogic.RepositoriesInterfaces.PurchaseInterface;
using SharedUseCase.DTOs.Purchase;
using SharedUseCase.InterfacesUC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.UseCase.PurchaseUC
{
    public class UpdatePurchase: IUpdate<PurchaseDto>
    {
        private IRepoPurchase _repo;
        public UpdatePurchase(IRepoPurchase repo)
        {
            _repo = repo;
        }

        public void Execute(int id, PurchaseDto obj)
        {
            try
            {
                if (id <= 0)
                {
                    throw new ArgumentException("El ID debe ser mayor que cero", nameof(id));
                }
                if (obj == null)
                {
                    throw new ArgumentNullException(nameof(obj), "El objeto no puede ser nulo");
                }
                if (obj.Client == null)
                {
                    throw new ArgumentException("El cliente no puede ser nulo", nameof(obj.Client));
                }
                if (obj.SubProducts == null || !obj.SubProducts.Any())
                {
                    throw new ArgumentException("La lista de subproductos no puede estar vacía", nameof(obj.SubProducts));
                }
                _repo.Update(id, PurchaseMapper.FromDto(obj));
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la compra: " + ex.Message, ex);
            }
    }
}
}
