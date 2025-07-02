using BusinessLogic.Entities;
using BusinessLogic.RepositoriesInterfaces.SubProductInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess.EF
{
    public class SubProductRepo : IRepoSubProduct
    {
        private SargaContext _context;

        public SubProductRepo(SargaContext context)
        {
            _context = context;
        }
        public int Add(SubProduct obj)
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
                try { 
                Product product = _context.Products.FirstOrDefault(p => p.productCode == obj.productCode);
                    if (product == null)
                    {
                        throw new KeyNotFoundException("Producto no encontrado con el código proporcionado");
                    }
                }
                catch(KeyNotFoundException e)
                {
                    _context.Products.Add(new Product(0, obj.productCode, obj.Name, obj.Price, obj.Season, obj.Year, obj.Genre, obj.Brand, obj.Type));
                }
                _context.SubProducts.Add(obj);
                _context.SaveChanges();
                return obj.Id; // Asumiendo que el Id se genera automáticamente
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el subproducto: " + ex.Message, ex);
            }
        }

        public void Clear()
        {
            try
            {
                var subProducts = _context.SubProducts.ToList();
                
                // Elimina todos los subproductos
                _context.SubProducts.RemoveRange(subProducts);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al limpiar los subproductos: " + ex.Message, ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var subProduct = GetById(id);
                if (subProduct == null)
                {
                    throw new KeyNotFoundException("Subproducto no encontrado");
                }
                _context.SubProducts.Remove(subProduct);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el subproducto: " + ex.Message, ex);
            }
        }

        public IEnumerable<SubProduct> GetAll()
        {
            try
            {
                return _context.SubProducts.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todos los subproductos: " + ex.Message, ex);
            }
        }

        public SubProduct GetById(int id)
        {
            try
            {
                var subProduct = _context.SubProducts.FirstOrDefault(sp => sp.Id == id);
                if (subProduct == null)
                {
                    throw new KeyNotFoundException("Subproducto no encontrado");
                }
                return subProduct;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el subproducto por ID: " + ex.Message, ex);
            }
        }

        public IEnumerable<SubProduct> GetByProductCode(string value)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("El código del producto no puede estar vacío", nameof(value));
                }
                return _context.SubProducts.Where(sp => sp.productCode == value).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los subproductos por código de producto: " + ex.Message, ex);
            }
        }

        public IEnumerable<SubProduct> GetByProductId(int id)
        {
            try
            {
                return _context.SubProducts.Where(sp => sp.Id == id).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los subproductos por ID de producto: " + ex.Message, ex);
            }
        }

        public void Update(int id, SubProduct obj)
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
                var existingSubProduct = GetById(id);
                if (existingSubProduct == null)
                {
                    throw new KeyNotFoundException("Subproducto no encontrado");
                }
                existingSubProduct.productCode = obj.productCode;
                existingSubProduct.Name = obj.Name;
                existingSubProduct.Price = obj.Price;
                existingSubProduct.Color = obj.Color;
                existingSubProduct.Size = obj.Size;
                existingSubProduct.Images = obj.Images;
                existingSubProduct.Season = obj.Season;
                existingSubProduct.Year = obj.Year;
                _context.SubProducts.Update(existingSubProduct);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el subproducto: " + ex.Message, ex);
            }
    }
    }
}
