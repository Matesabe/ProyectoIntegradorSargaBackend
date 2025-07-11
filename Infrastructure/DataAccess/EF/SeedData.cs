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
            if (!_context.Administrators.Any()) loadAdmin();
            if (!_context.Clients.Any(c => c.Ci == "40861254")){
                usuarioPrueba();
            }
            else
            {
                resetUsuarioPrueba();
            }
            if (!_context.Warehouses.Any()) loadWarehouses();
            if (!_context.Products.Any(p => p.productCode == "1000316497")) articuloPrueba1();
            if (!_context.Products.Any(p => p.productCode == "ARSAL")) articuloPrueba2();
            if (!_context.Products.Any(p => p.productCode == "112344960")) articuloPrueba3();
            if (!_context.PurchasePromotionsAmount.Any(p => p.Description == "Promoción de prueba")) PromotionPrueba();
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

        private void resetUsuarioPrueba()
        {
            _context.Clients.RemoveRange(_context.Clients.Where(c => c.Ci == "40861254"));
            _context.SaveChanges();
            usuarioPrueba();
        }

        private void PromotionPrueba()
        {
            PurchasePromotionAmount promotion = null;
            promotion = new PurchasePromotionAmount(0, "Promoción de prueba", 1, "Amount");
            _context.PurchasePromotionsAmount.Add(promotion);
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
