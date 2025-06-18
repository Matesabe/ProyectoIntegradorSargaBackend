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
    internal class DeletePurchase : IRemove<PurchaseDto>
    {
        private IRepoPurchase _repo;
        public DeletePurchase(IRepoPurchase repo)
        {
            _repo = repo;
        }
        public void Execute(PurchaseDto pur)
        {
            try
            {
                int id = pur.Id;
                if (id <= 0)
                {
                    throw new ArgumentException("El ID debe ser mayor que cero", nameof(id));
                }
                 _repo.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la compra: " + ex.Message, ex);
            }
        }
    }
}
