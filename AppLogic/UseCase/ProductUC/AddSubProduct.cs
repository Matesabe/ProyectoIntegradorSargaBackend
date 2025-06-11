using AppLogic.Mapper;
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
    public class AddSubProduct : IAdd<SubProductDto>
    {
        private readonly IRepoSubProduct _repo;
        public AddSubProduct(IRepoSubProduct repo)
        {
            _repo = repo;
        }

        public int Execute(SubProductDto obj)
        {
            try
            {
                if (obj == null)
                {
                    throw new ArgumentNullException(nameof(obj), "El objeto no puede ser nulo");
                }
                if (string.IsNullOrWhiteSpace(obj.productCode))
                {
                    throw new ArgumentException("El campo 'productCode' no puede estar vacío", nameof(obj.productCode));
                }
                if (string.IsNullOrWhiteSpace(obj.Name))
                {
                    throw new ArgumentException("El campo 'Name' no puede estar vacío", nameof(obj.Name));
                }
                if (obj.Price <= 0)
                {
                    throw new ArgumentException("El campo 'Price' debe ser mayor que cero", nameof(obj.Price));
                }

                return _repo.Add(SubproductMapper.FromDto(obj));
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el subproducto: " + ex.Message, ex);
            }

        }
    }
}
