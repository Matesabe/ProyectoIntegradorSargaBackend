using BusinessLogic.Entities;
using BusinessLogic.RepositoriesInterfaces.WarehouseInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess.EF
{
    public class WarehouseRepo : IRepoWarehouse
    {
        private SargaContext _context;
        public WarehouseRepo(SargaContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context), "El contexto no puede ser nulo");
        }

        public int Add(Warehouse obj)
        {
            try { 
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj), "El objeto no puede ser nulo");
            }
            if (string.IsNullOrWhiteSpace(obj.Name))
            {
                throw new ArgumentException("El campo 'Name' no puede estar vacío", nameof(obj.Name));
            }
            if (obj.SubProducts == null || !obj.SubProducts.Any())
            {
                throw new ArgumentException("El campo 'SubProducts' no puede estar vacío", nameof(obj.SubProducts));
            }
            _context.Warehouses.Add(obj);
            _context.SaveChanges();
            return obj.Id; // Asumiendo que el Id se genera automáticamente
            }catch(Exception ex)
            {
                throw new Exception("Error al agregar el depósito: " + ex.Message, ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var warehouse = GetById(id);
                if (warehouse == null)
                {
                    throw new KeyNotFoundException("Depósito no encontrado");
                }
                _context.Warehouses.Remove(warehouse);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el depósito: " + ex.Message, ex);
            }
        }

        public IEnumerable<Warehouse> GetAll()
        {
            try
            {
                return _context.Warehouses.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todos los depósitos: " + ex.Message, ex);
            }
        }

        public Warehouse GetById(int id)
        {
            try
            {
                var warehouse = _context.Warehouses.FirstOrDefault(w => w.Id == id);
                if (warehouse == null)
                {
                    throw new KeyNotFoundException("Depósito no encontrado");
                }
                return warehouse;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el depósito por ID: " + ex.Message, ex);
            }
        }

        public IEnumerable<SubProduct> GetProductsByWarehouseId(int id)
        {
            try
            {
                var warehouse = GetById(id);
                if (warehouse == null)
                {
                    throw new KeyNotFoundException("Depósito no encontrado");
                }
                return warehouse.SubProducts;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los productos del depósito: " + ex.Message, ex);
            }
        }

        public void Update(int id, Warehouse obj)
        {
            try
            {
                if (obj == null)
                {
                    throw new ArgumentNullException(nameof(obj), "El objeto no puede ser nulo");
                }
                if (string.IsNullOrWhiteSpace(obj.Name))
                {
                    throw new ArgumentException("El campo 'Name' no puede estar vacío", nameof(obj.Name));
                }
                if (obj.SubProducts == null || !obj.SubProducts.Any())
                {
                    throw new ArgumentException("El campo 'SubProducts' no puede estar vacío", nameof(obj.SubProducts));
                }
                var existingWarehouse = GetById(id);
                if (existingWarehouse == null)
                {
                    throw new KeyNotFoundException("Depósito no encontrado");
                }
                existingWarehouse.Name = obj.Name;
                existingWarehouse.SubProducts = obj.SubProducts;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el depósito: " + ex.Message, ex);
            }
        }

    }
}
