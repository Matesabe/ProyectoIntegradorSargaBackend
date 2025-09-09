using BusinessLogic.Entities;
using BusinessLogic.RepositoriesInterfaces.PromotionInterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess.EF
{
    public class PromotionRepo : IRepoPromotion
    {
        SargaContext _context;

        public PromotionRepo(SargaContext context)
        {
            _context = context;
        }
        public int Add(PurchasePromotion obj)
        {
            try
            {
                if (obj == null)
                {
                    throw new ArgumentNullException(nameof(obj), "El objeto no puede ser nulo");
                }
                if (string.IsNullOrWhiteSpace(obj.Type))
                {
                    throw new ArgumentException("El tipo de la promoción es nulo", nameof(obj.Type));
                }
                switch (obj.Type)
                {
                    case "Date":
                        var datePromotion = obj as PurchasePromotionDate;
                        if (datePromotion == null || datePromotion.PromotionDateStart == default || datePromotion.PromotionDateEnd == default)
                        {
                            throw new ArgumentException("Las fechas de inicio y fin no pueden estar vacías", nameof(obj));
                        }
                        _context.PurchasePromotionsDate.Add(datePromotion);
                        break;
                    case "Products":
                        var productPromotion = obj as PurchasePromotionProducts;
                        if (productPromotion == null || productPromotion.ProductPromotions == null || !productPromotion.ProductPromotions.Any())
                        {
                            throw new ArgumentException("La lista de productos no puede estar vacía al dar de alta una promoción de productos.");
                        }
                        _context.PurchasePromotionsProducts.Add(productPromotion);
                        break;
                    case "Recurrence":
                        var recurrencePromotion = obj as PurchasePromotionRecurrence;
                        if (recurrencePromotion.RecurrenceValue <= 0 || recurrencePromotion.PointsPerRecurrence <= 0)
                        {
                            throw new ArgumentException("El campo 'Recurrence' no puede estar vacío, los puntos por recurrencia no pueden ser igual o menores a 0.");
                        }
                        _context.PurchasePromotionsRecurrence.Add(recurrencePromotion);
                        break;
                    case "Amount":
                        var amountPromotion = obj as PurchasePromotionAmount;
                        if (amountPromotion.AmountPerPoint <= 0)
                        {
                            throw new ArgumentException("El campo 'Amount' debe ser mayor que 0.");
                        }
                        _context.PurchasePromotionsAmount.Add(amountPromotion);
                        break;
                    default:
                        throw new ArgumentException("Tipo de promoción desconocido", nameof(obj.Type));
                }
                _context.SaveChanges();
                return obj.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar la promoción: " + ex.Message, ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var promotion = GetById(id);
                if (promotion == null)
                {
                    throw new KeyNotFoundException("Promoción no encontrada");
                }
                switch (promotion.Type)
                {
                    case "Date":
                        var datePromotion = promotion as PurchasePromotionDate;
                        _context.PurchasePromotionsDate.Remove(datePromotion);
                        break;
                    case "Products":
                        var productPromotion = promotion as PurchasePromotionProducts;
                        _context.PurchasePromotionsProducts.Remove(productPromotion);
                        break;
                    case "Recurrence":
                        var recurrencePromotion = promotion as PurchasePromotionRecurrence;
                        _context.PurchasePromotionsRecurrence.Remove(recurrencePromotion);
                        break;
                    case "Amount":
                        var amountPromotion = promotion as PurchasePromotionAmount;
                        _context.PurchasePromotionsAmount.Remove(amountPromotion);
                        break;
                    default:
                        throw new ArgumentException("Tipo de promoción desconocido", nameof(promotion.Type));
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el producto: " + ex.Message, ex);
            }
        }

        public IEnumerable<PurchasePromotion> GetAll()
        {
            try
            {
                var promotions = new List<PurchasePromotion>();
                promotions.AddRange(_context.PurchasePromotionsDate.ToList());
                promotions.AddRange(_context.PurchasePromotionsProducts.Include(p => p.ProductPromotions)
        .ThenInclude(pp => pp.Product).ToList());
                promotions.AddRange(_context.PurchasePromotionsRecurrence.ToList());
                promotions.AddRange(_context.PurchasePromotionsAmount.ToList());
                return promotions;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todas las promociones: " + ex.Message, ex);
            }
        }



        public PurchasePromotion GetById(int id)
        {
            try
            {
                // Primero intentamos con Products, que necesita Include
                var productPromotion = _context.PurchasePromotionsProducts
                    .Include(p => p.ProductPromotions)
                    .FirstOrDefault(p => p.Id == id) as PurchasePromotion;
                if (productPromotion != null) return productPromotion;

                // Los demás tipos
                var datePromotion = _context.PurchasePromotionsDate.Find(id) as PurchasePromotion;
                if (datePromotion != null) return datePromotion;

                var recurrencePromotion = _context.PurchasePromotionsRecurrence.Find(id) as PurchasePromotion;
                if (recurrencePromotion != null) return recurrencePromotion;

                var amountPromotion = _context.PurchasePromotionsAmount.Find(id) as PurchasePromotion;
                if (amountPromotion != null) return amountPromotion;

                throw new KeyNotFoundException("Promoción no encontrada");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la promoción por ID: " + ex.Message, ex);
            }
        }

        public void Update(int id, PurchasePromotion obj)
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
                var promotion = GetById(id);
                if (promotion == null)
                {
                    throw new KeyNotFoundException("Promoción no encontrada");
                }
                // Actualizar los campos necesarios
                promotion.Description = obj.Description;
                promotion.IsActive = obj.IsActive;
                switch (promotion.Type)
                {
                    case "Date":
                        var datePromotion = promotion as PurchasePromotionDate;
                        datePromotion.PromotionDateStart = (obj as PurchasePromotionDate).PromotionDateStart;
                        datePromotion.PromotionDateEnd = (obj as PurchasePromotionDate).PromotionDateEnd;
                        datePromotion.PointsPerDate = (obj as PurchasePromotionDate).PointsPerDate;
                        break;
                    case "Products":
                        var productPromotion = promotion as PurchasePromotionProducts;
                        var incoming = (obj as PurchasePromotionProducts).ProductPromotions;

                        // Limpio los productos actuales
                        productPromotion.ProductPromotions.Clear();

                        // Agregar los nuevos productos
                        foreach (var p in incoming)
                        {
                            productPromotion.ProductPromotions.Add(new ProductPromotion
                            {
                                ProductId = p.ProductId,
                                PurchasePromotionProductsId = productPromotion.Id
                            });
                        }

                        productPromotion.PointsPerProducts = (obj as PurchasePromotionProducts).PointsPerProducts;
                        break;
                    case "Recurrence":
                        var recurrencePromotion = promotion as PurchasePromotionRecurrence;
                        recurrencePromotion.RecurrenceValue = (obj as PurchasePromotionRecurrence).RecurrenceValue;
                        recurrencePromotion.PointsPerRecurrence = (obj as PurchasePromotionRecurrence).PointsPerRecurrence;
                        break;
                    case "Amount":
                        var amountPromotion = promotion as PurchasePromotionAmount;
                        amountPromotion.AmountPerPoint = (obj as PurchasePromotionAmount).AmountPerPoint;
                        break;
                    default:
                        throw new ArgumentException("Tipo de promoción desconocido", nameof(obj.Type));
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la promoción: " + ex.Message, ex.InnerException ?? ex);
            }
        }
}
}
