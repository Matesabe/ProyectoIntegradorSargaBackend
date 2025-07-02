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

        public void UpdateStocks(SubProduct sub, int stockPdelE, int stockCol, int stockPay, int stockPeat, int stockSal)
        {
            try
            {
                if (sub == null)
                {
                    throw new ArgumentNullException(nameof(sub), "El subproducto no puede ser nulo");
                }
                var existingSubProduct = _context.SubProducts.FirstOrDefault(s => s.Id == sub.Id);
                if (existingSubProduct == null)
                {
                    throw new KeyNotFoundException("Subproducto no encontrado");
                }
                UpdateStockPdE(existingSubProduct, stockPdelE);
                UpdateStockCol(existingSubProduct, stockCol);
                UpdateStockPay(existingSubProduct, stockPay);
                UpdateStockPeat(existingSubProduct, stockPeat);
                UpdateStockSal(existingSubProduct, stockSal);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar los stocks del subproducto: " + ex.Message, ex);
            }
        }

        private void UpdateStockSal(SubProduct SubProduct, int stockSal)
        {
            try
            {
                if (stockSal < 0)
                {
                    throw new ArgumentException("El stock de sal no puede ser negativo", nameof(stockSal));
                }
                var salto = _context.Warehouses.FirstOrDefault(w => w.Name == "Sarga Salto");
                if (salto == null)
                {
                    throw new KeyNotFoundException("Depósito Sarga Salto no encontrado");
                }
                for (global::System.Int32 i = 0; i < stockSal; i++)
                {
                    salto.SubProducts.Append(SubProduct);
                }    
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el stock de sal: " + ex.Message, ex);
            }
        }

        private void UpdateStockPeat(SubProduct SubProduct, int stockPeat)
        {
            try
            {
                if (stockPeat < 0)
                {
                    throw new ArgumentException("El stock de sal no puede ser negativo", nameof(stockPeat));
                }
                var salto = _context.Warehouses.FirstOrDefault(w => w.Name == "Sarga Peatonal Maldonado");
                if (salto == null)
                {
                    throw new KeyNotFoundException("Depósito Sarga Salto no encontrado");
                }
                for (global::System.Int32 i = 0; i < stockPeat; i++)
                {
                    salto.SubProducts.Append(SubProduct);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el stock de sal: " + ex.Message, ex);
            }
        }

        private void UpdateStockPay(SubProduct SubProduct, int stockPay)
        {
            try
            {
                if (stockPay < 0)
                {
                    throw new ArgumentException("El stock de sal no puede ser negativo", nameof(stockPay));
                }
                var salto = _context.Warehouses.FirstOrDefault(w => w.Name == "Sarga Paysandú");
                if (salto == null)
                {
                    throw new KeyNotFoundException("Depósito Sarga Salto no encontrado");
                }
                for (global::System.Int32 i = 0; i < stockPay; i++)
                {
                    salto.SubProducts.Append(SubProduct);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el stock de sal: " + ex.Message, ex);
            }
        }

        private void UpdateStockCol(SubProduct SubProduct, int stockCol)
        {
            try
            {
                if (stockCol < 0)
                {
                    throw new ArgumentException("El stock de sal no puede ser negativo", nameof(stockCol));
                }
                var salto = _context.Warehouses.FirstOrDefault(w => w.Name == "Sarga Colonia");
                if (salto == null)
                {
                    throw new KeyNotFoundException("Depósito Sarga Salto no encontrado");
                }
                for (global::System.Int32 i = 0; i < stockCol; i++)
                {
                    salto.SubProducts.Append(SubProduct);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el stock de sal: " + ex.Message, ex);
            }
        }

        private void UpdateStockPdE(SubProduct SubProduct, int stockPdelE)
        {
            try
            {
                if (stockPdelE < 0)
                {
                    throw new ArgumentException("El stock de sal no puede ser negativo", nameof(stockPdelE));
                }
                var salto = _context.Warehouses.FirstOrDefault(w => w.Name == "Sarga Punta del Este");
                if (salto == null)
                {
                    throw new KeyNotFoundException("Depósito Sarga Salto no encontrado");
                }
                for (global::System.Int32 i = 0; i < stockPdelE; i++)
                {
                    salto.SubProducts.Append(SubProduct);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el stock de sal: " + ex.Message, ex);
            }
        }

        public void ClearStocks()
        {
            IEnumerable<Warehouse> warehouses = GetAll();
            try
            {
                foreach (var warehouse in warehouses)
                {
                    if (warehouse.SubProducts != null && warehouse.SubProducts.Any())
                    {
                        // Elimina todos los subproductos del depósito
                        warehouse.SubProducts = new List<SubProduct>();
                    }
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al limpiar los stocks de los depósitos: " + ex.Message, ex);
            }
        }
    }
}
