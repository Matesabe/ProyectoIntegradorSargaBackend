using BusinessLogic.RepositoriesInterfaces.ProductsInterface;
using Libreria.LogicaNegocio.InterfacesRepositorios;
using SharedUseCase.DTOs.Product;
using SharedUseCase.InterfacesUC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.UseCase.ProdUC
{
    public class DeleteProduct : IRemove
    {
        IRepoProducts _repo;
        public DeleteProduct(IRepoProducts repo)
        {
            _repo = repo;
        }
        public void Execute(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new ArgumentException("El ID debe ser mayor que cero", nameof(id));
                }
                _repo.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el producto: " + ex.Message, ex);
            }
        }
    }
}
