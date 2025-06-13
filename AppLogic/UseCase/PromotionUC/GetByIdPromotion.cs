using AppLogic.Mapper;
using BusinessLogic.Entities;
using BusinessLogic.RepositoriesInterfaces.PromotionInterface;
using SharedUseCase.DTOs.Promotion;
using SharedUseCase.InterfacesUC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.UseCase.PromotionUC
{
    public class GetByIdPromotion : IGetById<PromotionDto>
    {
        private IRepoPromotion _repo;
        public GetByIdPromotion(IRepoPromotion repo)
        {
            _repo = repo;
        }
        public PromotionDto Execute(int id)
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
                return PromotionMapper.ToDto(promotion);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la promoción por ID: " + ex.Message, ex);
            }
        }
    }
}
