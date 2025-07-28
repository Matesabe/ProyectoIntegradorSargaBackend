using BusinessLogic.Entities;
using BusinessLogic.RepositoriesInterfaces.ProductsInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess.EF
{
    public class ProductRepo : IRepoProducts
    {
        SargaContext _context;

        public ProductRepo(SargaContext context)
        {
            _context = context;
        }

        public int Add(Product obj)
        {
            try
            {
                obj.Id = 0; // Ensure Id is reset for new product
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
                _context.Products.Add(obj);
                _context.SaveChanges();
                return obj.Id; 
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el producto: " + ex.Message, ex);
            }
        }
        public void Delete(int id)
        {
            try
            {
                var product = GetById(id);
                if (product == null)
                {
                    throw new KeyNotFoundException("Producto no encontrado");
                }
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el producto: " + ex.Message, ex);
            }
        }

        public IEnumerable<Product> GetAll()
        {
            try
            {
                return _context.Products.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todos los productos: " + ex.Message, ex);
            }
        }

        public Product GetById(int id)
        {
            try
            {
                var product = _context.Products.Find(id);
                if (product == null)
                {
                    throw new KeyNotFoundException("Producto no encontrado");
                }
                return product;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el producto por ID: " + ex.Message, ex);
            }
        }

        public IEnumerable<Product> GetByProductCode(string code)
        {
            try { 
                return _context.Products.Where(p => p.productCode == code).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el producto por código: " + ex.Message, ex);
            }
        }

        public void Update(int id, Product obj)
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
                var existingProduct = GetById(id);
                if (existingProduct == null)
                {
                    throw new KeyNotFoundException("Producto no encontrado");
                }
                existingProduct.productCode = obj.productCode;
                existingProduct.Name = obj.Name;
                existingProduct.Price = obj.Price;
                existingProduct.Season = obj.Season;
                existingProduct.Year = obj.Year;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el producto: " + ex.Message, ex);
            }
        }
    }
}
