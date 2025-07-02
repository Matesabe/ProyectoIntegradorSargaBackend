using BusinessLogic.RepositoriesInterfaces.SubProductInterface;
using SharedUseCase.InterfacesUC.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.UseCase.ProductUC
{
    public class ClearSubProducts : IClearSubProducts
    {
        private IRepoSubProduct _repo;
        
        public ClearSubProducts(IRepoSubProduct repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo), "El repositorio no puede ser nulo");
        }
        public void Execute()
        {
            try
            {
                _repo.Clear();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al limpiar los subproductos: " + ex.Message, ex);
            }
        }
    }
}
