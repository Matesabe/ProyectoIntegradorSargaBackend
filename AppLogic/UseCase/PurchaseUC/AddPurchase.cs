using AppLogic.Mapper;
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
        private IRepoPurchase _repo;
        public AddPurchase(IRepoPurchase repo)
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
                if (obj.Client == null)
                {
                    throw new ArgumentException("El cliente no puede ser nulo", nameof(obj.Client));
                }
                if (obj.PurchaseProducts == null || !obj.PurchaseProducts.Any())
                {
                    throw new ArgumentException("La lista de PurchaseProducts no puede estar vacía", nameof(obj.PurchaseProducts));
                }
                return _repo.Add(PurchaseMapper.FromDto(obj));
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar la compra: " + ex.Message, ex);
            }
    }
}
}
