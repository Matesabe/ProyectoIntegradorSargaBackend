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
    public class AddPromotion : IAdd<PromotionDto>
    {
        private IRepoPromotion _repo;
        public AddPromotion(IRepoPromotion repo)
        {
            _repo = repo;
        }
        public int Execute(PromotionDto obj)
        {
            try
            {
                if (obj == null)
                {
                    throw new ArgumentNullException(nameof(obj), "El objeto no puede ser nulo");
                }
                if (string.IsNullOrWhiteSpace(obj.Type))
                {
                    throw new ArgumentException("El tipo de la promoción es nulo");
                }
                return _repo.Add(PromotionMapper.FromDto(obj));
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar la promoción: " + ex.Message, ex);
            }
        }
    }
}
