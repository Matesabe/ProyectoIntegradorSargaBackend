using BusinessLogic.Entities;
using BusinessLogic.RepositoriesInterfaces.RedemptionInterface;
using Infrastructure.DataAccess.EF.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess.EF
{
    public class RedemptionRepo : IRepoRedemption
    {
        SargaContext _context;
        public RedemptionRepo(SargaContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context), "El contexto no puede ser nulo");
        }

        public int Add(Redemption obj)
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
                if (obj.PointsUsed <= 0)
                {
                    throw new ArgumentException("El campo 'PointsUsed' debe ser mayor que cero", nameof(obj.PointsUsed));
                }
                if (obj.SubProducts == null || !obj.SubProducts.Any())
                {
                    throw new ArgumentException("El campo 'SubProducts' no puede estar vacío", nameof(obj.SubProducts));
                }
                _context.Redemptions.Add(obj);
                SetPointsToUser(obj.Client.Id, obj.PointsUsed);
                _context.SaveChanges();
                return obj.Id; // Asumiendo que el Id se genera automáticamente
            }
            catch (NotEnoughPointsException)
            {
                return -1; // Retorna -1 si no hay suficientes puntos para el canje
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el canje: " + ex.Message, ex);
            }
        }

        private void SetPointsToUser(int userId, int points)
        {
            try
            {
                if (points < 0)
                    throw new ArgumentException("Los puntos no pueden ser negativos", nameof(points));

                var client = _context.Clients.FirstOrDefault(c => c.Id == userId);
                if (client == null)
                    throw new InvalidOperationException($"No se encontró un cliente con el ID {userId}");

                // Restar los puntos al cliente
                if (client.Points < points)
                    throw new NotEnoughPointsException("El cliente no tiene suficientes puntos para el canje.");

                client.Points -= points;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var redemption = _context.Redemptions.FirstOrDefault(r => r.Id == id);
                if (redemption == null)
                {
                    throw new KeyNotFoundException($"No se encontró un canje con el ID {id}");
                }
                _context.Redemptions.Remove(redemption);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el canje: " + ex.Message, ex);
            }
        }

        public IEnumerable<Redemption> GetAll()
        {
            try
            {
                return _context.Redemptions.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todos los canjes: " + ex.Message, ex);
            }
        }

        public Redemption GetById(int id)
        {
            try
            {
                var redemption = _context.Redemptions.FirstOrDefault(r => r.Id == id);
                if (redemption == null)
                {
                    throw new KeyNotFoundException($"No se encontró un canje con el ID {id}");
                }
                return redemption;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el canje por ID: " + ex.Message, ex);
            }
        }

        public IEnumerable<Redemption> GetRedemptionByUserId(int clientId)
        {
            try
            {
                var redemptions = _context.Redemptions
                    .Where(r => r.Client.Id == clientId)
                    .ToList();
                if (!redemptions.Any())
                {
                    throw new KeyNotFoundException($"No se encontraron canjes para el cliente con ID {clientId}");
                }
                return redemptions;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los canjes por ID de cliente: " + ex.Message, ex);
            }
        }
        public void Update(int id, Redemption obj)
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
                if (obj.PointsUsed <= 0)
                {
                    throw new ArgumentException("El campo 'PointsUsed' debe ser mayor que cero", nameof(obj.PointsUsed));
                }
                if (obj.SubProducts == null || !obj.SubProducts.Any())
                {
                    throw new ArgumentException("El campo 'SubProducts' no puede estar vacío", nameof(obj.SubProducts));
                }
                var redemption = _context.Redemptions.FirstOrDefault(r => r.Id == id);
                if (redemption == null)
                {
                    throw new KeyNotFoundException($"No se encontró un canje con el ID {id}");
                }
                redemption.Client = obj.Client;
                redemption.Amount = obj.Amount;
                redemption.PointsUsed = obj.PointsUsed;
                redemption.SubProducts = obj.SubProducts;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el canje: " + ex.Message, ex);
            }
        }
    }
}
