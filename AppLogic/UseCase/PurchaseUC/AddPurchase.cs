using BusinessLogic.RepositoriesInterfaces.PurchaseInterface;
using BusinessLogic.RepositoriesInterfaces.SubProductInterface;
using SharedUseCase.DTOs.Purchase;
using SharedUseCase.InterfacesUC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.UseCase.PurchaseUC
{
    public class AddPurchase : IAdd<PurchaseDto>
    {

        private readonly IRepoPromotions _repo;
        public AddPurchase(IRepoPromotions repo)
        {
            _repo = repo;
        }
        public int Execute(PurchaseDto obj)
        {
            try
            {
                if (obj == null)
                {
                    throw new ArgumentNullException(nameof(obj), "El objeto no puede ser nulo");
                }
                if (string.IsNullOrWhiteSpace(obj.ProductCode))
                {
                    throw new ArgumentException("El campo 'ProductCode' no puede estar vacío", nameof(obj.ProductCode));
                }
                if (obj.Quantity <= 0)
                {
                    throw new ArgumentException("El campo 'Quantity' debe ser mayor que cero", nameof(obj.Quantity));
                }
                if (obj.Price <= 0)
                {
                    throw new ArgumentException("El campo 'Price' debe ser mayor que cero", nameof(obj.Price));
                }
                return _repo.Add(obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar la compra: " + ex.Message, ex);
            }
    }
}
}
