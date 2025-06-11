using BusinessLogic.RepositoriesInterfaces.ProductsInterface;
using SharedUseCase.DTOs.Product;
using SharedUseCase.InterfacesUC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.UseCase.ProdUC
{
    public class UpdateProduct : IUpdate<ProductDto>
    {
        IRepoProducts _repo;
        public UpdateProduct(IRepoProducts repo)
        {
            _repo = repo;
        }

        public void Execute(int id, ProductDto obj)
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
                var product = _repo.GetById(id);
                if (product == null)
                {
                    throw new KeyNotFoundException("Producto no encontrado");
                }
                _repo.Update(id, Mapper.ProductMapper.FromDto(obj));
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el producto: " + ex.Message, ex);
            }
        }
    }
}
