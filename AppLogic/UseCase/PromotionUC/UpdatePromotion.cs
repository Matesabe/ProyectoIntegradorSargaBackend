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
    public class UpdatePromotion:IUpdate<PromotionDto>
    {
        private IRepoPromotion _repo;
        public UpdatePromotion(IRepoPromotion repo)
        {
            _repo = repo;
        }

        public void Execute(int id, PromotionDto obj)
        {
            try
            {
                if (id <= 0)
                {
                    throw new ArgumentException("El ID debe ser mayor que cero", nameof(id));
                }
                if (obj == null)
                {
                    throw new ArgumentNullException(nameof(obj), "El objeto no puede ser nulo");
                }
                PurchasePromotion promotion = _repo.GetById(id);
                if (promotion == null)
                {
                    throw new KeyNotFoundException("Promoción no encontrada");
                }
                _repo.Update(id, PromotionMapper.FromDto(obj));
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la promoción: " + ex.Message, ex);
            }
    }
}
}
