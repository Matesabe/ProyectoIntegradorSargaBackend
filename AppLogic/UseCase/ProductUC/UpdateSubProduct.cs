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
    internal class UpdateSubProduct : IUpdate<SubProductDto>
    {
        private IRepoSubProduct _repo;
        public UpdateSubProduct(IRepoSubProduct repo)
        {
            _repo = repo;
        }
        public void Execute(int id, SubProductDto obj)
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
                SubProduct subProduct = _repo.GetById(id);
                if (subProduct == null)
                {
                    throw new KeyNotFoundException("Subproducto no encontrado");
                }
                _repo.Update(id, Mapper.SubproductMapper.FromDto(obj));
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el subproducto: " + ex.Message, ex);
            }
        }
    }
}
