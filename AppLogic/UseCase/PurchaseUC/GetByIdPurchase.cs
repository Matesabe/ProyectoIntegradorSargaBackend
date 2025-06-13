using AppLogic.Mapper;
using BusinessLogic.RepositoriesInterfaces.PurchaseInterface;
using BusinessLogic.RepositoriesInterfaces.SubProductInterface;
using SharedUseCase.DTOs.Product;
using SharedUseCase.DTOs.Purchase;
using SharedUseCase.InterfacesUC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.UseCase.PurchaseUC
{
    public class GetByIdPurchase : IGetById<PurchaseDto>
    {
        private IRepoPurchase _repo;
        public GetByIdPurchase(IRepoPurchase repo)
        {
            _repo = repo;
        }
        public PurchaseDto Execute(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new ArgumentException("El ID debe ser mayor que cero", nameof(id));
                }
                var purchase = _repo.GetById(id);
                if (purchase == null)
                {
                    throw new KeyNotFoundException("Compra no encontrada");
                }
                return PurchaseMapper.ToDto(purchase);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la compra por ID: " + ex.Message, ex);
            }
        }
    }
}
