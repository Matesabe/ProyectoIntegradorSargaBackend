using BusinessLogic.Entities;
using BusinessLogic.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infrastructure.DataAccess.EF
{
    public class SeedData
    {
        private SargaContext _context;

        public SeedData(SargaContext context)
        {
            _context = context;
        }

        public void Run()
        {

        }

        private void clearPurchases()
        {
            try
            {
                _context.Purchases.RemoveRange(_context.Purchases);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al limpiar compras: {ex.Message}");
            }
        }

        private void loadAdmin()
        {
            Administrator unA = null;
            unA = new Administrator(0, "87654321", new Name("Administrador Sarga"), new Password("SargaPass983"), new Email("adminSarga@mail.com"), "123456789", "Administrator" );
            _context.Administrators.Add(unA);
            _context.SaveChanges();
        }

        private void usuarioPrueba()
        {
            Client prueba = null;
            prueba = new Client(0, "40861254", new Name("Prueba de carga de ventas"), new Password("Pass1234"), new Email("pruebaVentas@mail.com"), "87665", "Client");
            _context.Clients.Add(prueba);
            _context.SaveChanges();
        }

        private void usuarioPrueba2()
        {
            Client prueba = null;
            prueba = new Client(0, "01930539", new Name("2Prueba de carga de ventas"), new Password("Pass1234"), new Email("pruebaVentas2@mail.com"), "817665", "Client");
            _context.Clients.Add(prueba);
            _context.SaveChanges();
        }

        private void CargarComprasPruebaCliente2()
        {
            Client clientePrueba = _context.Clients.FirstOrDefault(c => c.Ci == "01930539");

            if (clientePrueba == null) return;
            Purchase compra1 = new Purchase(0, clientePrueba, 2000, 0, new List<PurchaseProduct>(), DateTime.Now);
            Product product1 = _context.Products.FirstOrDefault(p => p.productCode == "1000316497");
            Product product2 = _context.Products.FirstOrDefault(p => p.productCode == "ARSAL");
            if (product1 != null && product2 != null)
            {
                compra1.PurchaseProducts.Add(new PurchaseProduct(compra1.Id, product1.Id, 2));
            }
            _context.Purchases.Add(compra1);
            //_context.Purchases.Add(compra2);
            _context.SaveChanges();
        }

        private void resetUsuarioPrueba()
        {
            _context.Clients.RemoveRange(_context.Clients.Where(c => c.Ci == "40861254"));
            _context.Purchases.RemoveRange(_context.Purchases.Where(p => p.Client.Ci == "40861254"));
            usuarioPrueba();
        }

        private void clearPromotions()
        {
            _context.PurchasePromotionsAmount.RemoveRange(_context.PurchasePromotionsAmount);
            _context.PurchasePromotionsDate.RemoveRange(_context.PurchasePromotionsDate);
            _context.PurchasePromotionsRecurrence.RemoveRange(_context.PurchasePromotionsRecurrence);
            _context.PurchasePromotionsProducts.RemoveRange(_context.PurchasePromotionsProducts);
            _context.SaveChanges();
        }

        private void PromotionPrueba()
        {
            PurchasePromotionAmount promotion = null;
            promotion = new PurchasePromotionAmount(0, "Promoción de prueba", 1, "Amount");
            _context.PurchasePromotionsAmount.Add(promotion);
            _context.SaveChanges();
        }

        private void PromotionPruebaProductos()
        {
            PurchasePromotionProducts promotion = null;
            List<ProductPromotion> promotionProducts = new List<ProductPromotion>();
            ProductPromotion productPromotion1 = new ProductPromotion(_context.Products.FirstOrDefault(p => p.productCode == "1000316497").Id, 0);
            ProductPromotion productPromotion2 = new ProductPromotion(_context.Products.FirstOrDefault(p => p.productCode == "ARSAL").Id, 0);
            promotionProducts.Add(productPromotion1);
            promotionProducts.Add(productPromotion2);

            promotion = new PurchasePromotionProducts(0, "Promoción de prueba productos", promotionProducts, 200);
            _context.PurchasePromotionsProducts.Add(promotion);
            _context.SaveChanges();
        }

        private void PromotionPruebaRecurrence()
        {
            PurchasePromotionRecurrence promotion = null;
            promotion = new PurchasePromotionRecurrence()
            {
                Id = 0,
                Description = "Promoción de prueba recurrencia",
                RecurrenceValue = 2,
                PointsPerRecurrence = 50,
                Type = "Recurrence"
            };
            _context.PurchasePromotionsRecurrence.Add(promotion);
            _context.SaveChanges();
        }

        private void PromotionPruebaFecha()
        {
            PurchasePromotionDate promotion = null;
            promotion = new PurchasePromotionDate(0, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(10), 100, 100, "DatePrueba");
            _context.PurchasePromotionsDate.Add(promotion); 
            _context.SaveChanges();
        }

        private void CargarComprasPruebaCliente()
        {
            Client clientePrueba = _context.Clients.FirstOrDefault(c => c.Ci == "40861254");

            if (clientePrueba == null) return;
            Purchase compra1 = new Purchase(0, clientePrueba, 2000, 0, new List<PurchaseProduct>(), DateTime.Now);
            Purchase compra2 = new Purchase(0, clientePrueba, 1000, 0, new List<PurchaseProduct>(), DateTime.Now);
            Product product1 = _context.Products.FirstOrDefault(p => p.productCode == "1000316497");
            Product product2 = _context.Products.FirstOrDefault(p => p.productCode == "ARSAL");
            if (product1 != null && product2 != null)
            {
                compra1.PurchaseProducts.Add(new PurchaseProduct(compra1.Id, product1.Id, 2));
                compra2.PurchaseProducts.Add(new PurchaseProduct(compra2.Id, product2.Id, 3));
            }
            _context.Purchases.Add(compra1);
            //_context.Purchases.Add(compra2);
            _context.SaveChanges();
        }

        private void articuloPrueba1()
        {
            Product product1 = new Product(0, "1000316497", "Canguro Felpa Liso Maui", 2000, "Invierno", "2025", "Hombre", "Maui", "Canguro");
            _context.Products.Add(product1);
        }

        private void articuloPrueba2()
        {
            Product product2 = new Product(0, "ARSAL", "Babucha Felpa Prueba", 1000, "Invierno", "2025", "Hombre", "Rusty", "Babucha");
            _context.Products.Add(product2);
        }
        private void articuloPrueba3()
        {
            Product product3 = new Product(0, "112344960", "Vaquero Wrangler", 1500, "Invierno", "2025", "Hombre", "Wrangler", "Jean");
            _context.Products.Add(product3);
        }

        private void clearReports()
        {
            _context.Reports.RemoveRange(_context.Reports);
            _context.SaveChanges();
        }
        private void loadWarehouses() {
            Warehouse PdE = new Warehouse(0, "Sarga Punta del Este", new List<WarehouseStock>());
            Warehouse Col = new Warehouse(0, "Sarga Colonia", new List<WarehouseStock>());
            Warehouse Sal = new Warehouse(0, "Sarga Salto", new List<WarehouseStock>());
            Warehouse Pay = new Warehouse(0, "Sarga Paysandú", new List<WarehouseStock>());
            Warehouse Pea = new Warehouse(0, "Sarga Peatonal Maldonado", new List<WarehouseStock>());
            _context.Warehouses.Add(PdE);
            _context.Warehouses.Add(Col);
            _context.Warehouses.Add(Sal);
            _context.Warehouses.Add(Pay);
            _context.Warehouses.Add(Pea);
            _context.SaveChanges();
        }
    }
}
