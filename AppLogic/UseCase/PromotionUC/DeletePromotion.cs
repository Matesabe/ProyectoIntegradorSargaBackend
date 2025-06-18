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
    public class DeletePromotion : IRemove<PromotionDto>
    {
        private IRepoPromotion _repo;
        public DeletePromotion(IRepoPromotion repo)
        {
            _repo = repo;
        }
        public void Execute(PromotionDto pro)
        {
            try
            {
                int id = pro.Id;
                if (id <= 0)
                {
                    throw new ArgumentException("El ID debe ser mayor que cero", nameof(id));
                }
                _repo.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la promoción: " + ex.Message, ex);
            }
    }
}
}