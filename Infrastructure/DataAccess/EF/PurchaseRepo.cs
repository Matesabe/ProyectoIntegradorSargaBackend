using BusinessLogic.Entities;
using BusinessLogic.RepositoriesInterfaces.PurchaseInterface;
using Microsoft.EntityFrameworkCore;
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
                if (obj.PurchaseProducts == null || !obj.PurchaseProducts.Any())
                {
                    throw new ArgumentException("El campo 'PurchaseProducts' no puede estar vacío", nameof(obj.PurchaseProducts));
                }

                //setear recurrenceCount de cliente
                obj.Client = _context.Clients.FirstOrDefault(c => c.Id == obj.Client.Id) ?? throw new InvalidOperationException("Cliente no encontrado.");
                int cantidadComprasCliente = GetPurchaseCountByClientId(obj.Client.Id);
                obj.Client.RecurrenceCount = cantidadComprasCliente;

                SetPointsToPurchase(obj);
                _context.Purchases.Add(obj);
                SetPointsToUser(obj.Client.Id, obj.PointsGenerated);
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
                obj.PointsGenerated = 0;

                // Combina todas las promociones activas en una sola lista
                var activePromotions = _context.PurchasePromotionsDate.Where(p => p.IsActive).ToList()
                    .Cast<PurchasePromotion>()
                    .Concat(_context.PurchasePromotionsProducts
                        .Include(pp => pp.ProductPromotions) // Incluye la lista de productos de la promoción
                        .Where(p => p.IsActive).ToList())
                    .Concat(_context.PurchasePromotionsRecurrence.Where(p => p.IsActive).ToList())
                    .Concat(_context.PurchasePromotionsAmount.Where(p => p.IsActive).ToList())
                    .ToList();

                // Aplica cada promoción a la compra
                foreach (var promotion in activePromotions)
                {
                    pointsGenerated = promotion.generatePoints(obj);
                    obj.PointsGenerated += pointsGenerated;
                    // plantearse si pueden ser acumulables o no
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

  

        public IEnumerable<Purchase> GetAll()
        {
                try
                {
                    // Incluye los datos del cliente y los datos completos de los productos
                    return _context.Purchases
                        .Include(p => p.Client)
                        .Include(p => p.PurchaseProducts)
                        .ToList();
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
                var purchase = _context.Purchases.Include(P => P.Client).Include(p => p.PurchaseProducts).FirstOrDefault(p => p.Id==id);
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
            _context.Clients.Update(client);
                _context.SaveChanges();
            }catch(Exception ex) {
                throw new Exception(ex.Message, ex);
            }
        }

        public void SubstractPointsFromUser(int userId, int points)
        {
            try
            {
                if (points > 0)
                    throw new ArgumentException("Los puntos no pueden ser positivos", nameof(points));

                var client = _context.Clients.FirstOrDefault(c => c.Id == userId);
                if (client == null)
                    throw new InvalidOperationException($"No se encontró un cliente con el ID {userId}");

                client.Points += points;

                if (client.Points < 0)
                {
                    throw new InvalidOperationException("Los puntos del cliente no pueden ser negativos");
                }
                _context.Clients.Update(client);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
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
                if (obj.PurchaseProducts == null || !obj.PurchaseProducts.Any())
                {
                    throw new ArgumentException("El campo 'PurchaseProducts' no puede estar vacío", nameof(obj.PurchaseProducts));
                }
                var existingPurchase = GetById(id);
                if (existingPurchase == null)
                {
                    throw new KeyNotFoundException("Compra no encontrada");
                }
                // Restar los puntos generados de la compra anterior a la modificación para luego agregarlos desde la modificada
                SubstractPointsFromUser(existingPurchase.Client.Id, obj.PointsGenerated * -1);
                SetPointsToPurchase(existingPurchase);
                SetPointsToUser(existingPurchase.Client.Id, existingPurchase.PointsGenerated);

                // Actualizar los campos necesarios
                existingPurchase.Client = _context.Clients.FirstOrDefault(c => c.Id == obj.Client.Id) ?? throw new InvalidOperationException("Cliente no encontrado.");
                existingPurchase.Amount = obj.Amount;
                existingPurchase.PurchaseProducts = obj.PurchaseProducts;
                
                _context.Purchases.Update(existingPurchase);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la compra: " + ex.Message, ex);
            }
        }

        public void Clear()
        {
            try
            {
                var purchases = _context.Purchases.ToList();

                // Elimina todos las ventas
                _context.Purchases.RemoveRange(purchases);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al limpiar las ventas: " + ex.Message, ex);
            }
        }

        public int GetPurchaseCountByClientId(int clientId)
        {
            try
            {
                var sixMonthsAgo = DateTime.Now.AddMonths(-6);
                return _context.Purchases.Count(
                    p => p.Client != null
                        && p.Client.Id == clientId
                        && p.PurchaseProducts != null
                        && p.PurchaseProducts.Any()
                        && p.Date >= sixMonthsAgo
                );
            }
            catch (Exception ex)
            {
                throw new Exception("Error al contar las compras por ID de cliente: " + ex.Message, ex);
            }
        }
    }
}
