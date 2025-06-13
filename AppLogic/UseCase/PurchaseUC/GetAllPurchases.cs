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
    public class GetAllPurchases : IGetAll<PurchaseDto>
    {
        private IRepoPurchase _repo;
        public GetAllPurchases(IRepoPurchase repo)
        {
            _repo = repo;
        }

        public IEnumerable<PurchaseDto> Execute()
        {
            try
            {
                var purchases = _repo.GetAll();
                return purchases.Select(p => PurchaseMapper.ToDto(p));
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todas las compras: " + ex.Message, ex);
            }
        }
    }
}
