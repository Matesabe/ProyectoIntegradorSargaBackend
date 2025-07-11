using BusinessLogic.RepositoriesInterfaces.PurchaseInterface;
using BusinessLogic.RepositoriesInterfaces.SubProductInterface;
using SharedUseCase.InterfacesUC.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.UseCase.PurchaseUC
{
    public class ClearPurchases : IClearPurchases
    {
        private IRepoPurchase _repo;

        public ClearPurchases(IRepoPurchase repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo), "El repositorio no puede ser nulo");
        }
        public void Execute()
        {
            try
            {
                _repo.Clear();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al limpiar los subproductos: " + ex.Message, ex);
            }
        }
    }
}
