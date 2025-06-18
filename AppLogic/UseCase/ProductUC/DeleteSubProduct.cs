using BusinessLogic.Entities;
using BusinessLogic.RepositoriesInterfaces.SubProductInterface;
using SharedUseCase.DTOs.Product;
using SharedUseCase.InterfacesUC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.UseCase.ProductUC
{
    public class DeleteSubProduct : IRemove<SubProductDto>
    {
        private IRepoSubProduct _repo;
        public void Execute(SubProductDto sub)
        {
            try
            {
                int id = sub.Id;
                if (id <= 0)
                {
                    throw new ArgumentException("El ID debe ser mayor que cero", nameof(id));
                }
                _repo.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el subproducto: " + ex.Message, ex);
            }
        }
            
        public DeleteSubProduct(IRepoSubProduct repo)
        {
            _repo = repo;
        }
    }
}


