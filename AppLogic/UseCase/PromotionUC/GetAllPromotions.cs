using AppLogic.Mapper;
using BusinessLogic.Entities;
using BusinessLogic.RepositoriesInterfaces.PromotionInterface;
using Libreria.LogicaNegocio.InterfacesRepositorios;
using SharedUseCase.DTOs.Promotion;
using SharedUseCase.InterfacesUC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.UseCase.PromotionUC
{
    public class GetAllPromotions:IGetAll<PromotionDto>
    {
        private IRepoPromotion _repo;
        public GetAllPromotions(IRepoPromotion repo)
        {
            _repo = repo;
        }
        public IEnumerable<PromotionDto> Execute()
        {
            try
            {
                return PromotionMapper.ToListDto(_repo.GetAll());
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todas las promociones", ex);
            }
        }
    }
}