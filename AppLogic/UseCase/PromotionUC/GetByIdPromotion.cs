using BusinessLogic.Entities;
using BusinessLogic.RepositoriesInterfaces.PromotionInterface;
using SharedUseCase.InterfacesUC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.UseCase.PromotionUC
{
    public class GetByIdPromotion : IGetById<PurchasePromotion>
    {
        public IRepoPromotion _repo;
        public GetByIdPromotion(IRepoPromotion repo)
        {
            _repo = repo;
        }
        public PurchasePromotion Execute(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new ArgumentException("El ID debe ser mayor que cero", nameof(id));
                }
                PurchasePromotion promotion = _repo.GetById(id);
                if (promotion == null)
                {
                    throw new KeyNotFoundException("Promoción no encontrada");
                }
                return promotion;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la promoción por ID: " + ex.Message, ex);
            }
        }
    }
}
