using BusinessLogic.Entities;
using BusinessLogic.RepositoriesInterfaces.PurchaseInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess.EF
{
    public class PurchaseRepo : IRepoPurchase
    {
        SargaContext _context;
        public PurchaseRepo(SargaContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context), "El contexto no puede ser nulo");
        }
        public int Add(Purchase obj)
        {
            try
            {
                if (obj == null)
                {
                    throw new ArgumentNullException(nameof(obj), "El objeto no puede ser nulo");
                }
                if (obj.Client == null)
                {
                    throw new ArgumentException("El campo 'Client' no puede estar vacío", nameof(obj.Client));
                }
                if (obj.Amount <= 0)
                {
                    throw new ArgumentException("El campo 'Amount' debe ser mayor que cero", nameof(obj.Amount));
                }
                if (obj.SubProducts == null || !obj.SubProducts.Any())
                {
                    throw new ArgumentException("El campo 'SubProducts' no puede estar vacío", nameof(obj.SubProducts));
                }
                SetPointsToPurchase(obj);
                _context.Purchases.Add(obj);
                SetPointsToUser(obj.Client.Id, obj.PointsGenerated);
                //DeleteProductsFromWarehouse(obj); -> Omitido hasta implementar la lógica completa de los almacenes
                _context.SaveChanges();
                return obj.Id; // Asumiendo que el Id se genera automáticamente
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar la compra: " + ex.Message, ex);
            }
        }

        private void SetPointsToPurchase(Purchase obj)
        {
            try
            {
                int pointsGenerated = 0;

                // Combine all active promotions into a single list
                var activePromotions = _context.PurchasePromotionsDate
                    .Where(p => p.IsActive)
                    .Cast<PurchasePromotion>()
                    .Concat(_context.PurchasePromotionsProducts.Where(p => p.IsActive))
                    .Concat(_context.PurchasePromotionsRecurrence.Where(p => p.IsActive))
                    .Concat(_context.PurchasePromotionsAmount.Where(p => p.IsActive))
                    .ToList();

                // Apply each promotion to the purchase
                foreach (var promotion in activePromotions)
                {
                    pointsGenerated = promotion.generatePoints(obj);
                    obj.PointsGenerated += pointsGenerated;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al calcular los puntos de la compra: " + ex.Message, ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var purchase = GetById(id);
                if (purchase == null)
                {
                    throw new KeyNotFoundException("Compra no encontrada");
                }
                _context.Purchases.Remove(purchase);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la compra: " + ex.Message, ex);
            }
        }

        public void DeleteProductsFromWarehouse(Purchase obj)
        {
            try
            {
                if (obj == null)
                    throw new ArgumentNullException(nameof(obj), "El objeto compra no puede ser nulo");
                if (obj.Warehouse == null)
                    throw new ArgumentException("La compra no tiene un almacén asociado", nameof(obj.Warehouse));
                if (obj.SubProducts == null || !obj.SubProducts.Any())
                    throw new ArgumentException("La compra no tiene subproductos para eliminar", nameof(obj.SubProducts));

                // Obtener el almacén actualizado desde la base de datos
                var warehouse = _context.Warehouses
                    .Where(w => w.Id == obj.Warehouse.Id)
                    .Select(w => new
                    {
                        Warehouse = w,
                        SubProducts = w.SubProducts.ToList()
                    })
                    .FirstOrDefault();

                if (warehouse == null)
                    throw new InvalidOperationException("No se encontró el almacén asociado a la compra");

                // Eliminar los subproductos de la compra del almacén
                foreach (var subProduct in obj.SubProducts)
                {
                    var subProductInWarehouse = warehouse.SubProducts.FirstOrDefault(sp => sp.Id == subProduct.Id);
                    if (subProductInWarehouse != null)
                    {
                        warehouse.SubProducts.Remove(subProductInWarehouse);
                    }
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar los productos del almacén: " + ex.Message, ex);
            }
        }

        public IEnumerable<Purchase> GetAll()
        {
            try
            {
                return _context.Purchases.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todas las compras: " + ex.Message, ex);
            }
        }

        public Purchase GetById(int id)
        {
            try
            {
                var purchase = _context.Purchases.Find(id);
                if (purchase == null)
                {
                    throw new KeyNotFoundException("Compra no encontrada");
                }
                return purchase;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la compra por ID: " + ex.Message, ex);
            }
        }

        public IEnumerable<Purchase> GetPurchaseByClientId(int clientId)
        {
            try
            {
                if (clientId < 0)
                    throw new ArgumentException("El ID del cliente no puede ser menor a cero", nameof(clientId));
                return _context.Purchases
                    .Where(p => p.Client != null && p.Client.Id == clientId)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las compras por ID de cliente: " + ex.Message, ex);
            }
        }

        public void SetPointsToUser(int userId, int points)
        {
            try { 
            if (points < 0)
                throw new ArgumentException("Los puntos no pueden ser negativos", nameof(points));

            var client = _context.Clients.FirstOrDefault(c => c.Id == userId);
            if (client == null)
                throw new InvalidOperationException($"No se encontró un cliente con el ID {userId}");

            client.Points += points;
            _context.SaveChanges();
            }catch(Exception ex) {
                throw new Exception(ex.Message, ex);
            }
        }

        public void Update(int id, Purchase obj)
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
                if (obj.Client == null)
                {
                    throw new ArgumentException("El campo 'Client' no puede estar vacío", nameof(obj.Client));
                }
                if (obj.Amount <= 0)
                {
                    throw new ArgumentException("El campo 'Amount' debe ser mayor que cero", nameof(obj.Amount));
                }
                if (obj.SubProducts == null || !obj.SubProducts.Any())
                {
                    throw new ArgumentException("El campo 'SubProducts' no puede estar vacío", nameof(obj.SubProducts));
                }
                var existingPurchase = GetById(id);
                if (existingPurchase == null)
                {
                    throw new KeyNotFoundException("Compra no encontrada");
                }
                // Actualizar los campos necesarios
                existingPurchase.Client = obj.Client;
                existingPurchase.Amount = obj.Amount;
                existingPurchase.SubProducts = obj.SubProducts;
                SetPointsToPurchase(existingPurchase);

                _context.Purchases.Update(existingPurchase);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la compra: " + ex.Message, ex);
            }

        }
    }
}
