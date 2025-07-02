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
            if (!_context.Warehouses.Any()) loadWarehouses();
        }

        private void loadAdmin()
        {
            Administrator unA = null;
            unA = new Administrator(0, "87654321", new Name("Administrador Sarga"), new Password("SargaPass983"), new Email("adminSarga@mail.com"), "123456789", "Administrator" );
            _context.Administrators.Add(unA);
            _context.SaveChanges();
        }

        private void loadWarehouses() {
            Warehouse PdE = new Warehouse(0, "Sarga Punta del Este", null);
            Warehouse Col = new Warehouse(0, "Sarga Colonia", null);
            Warehouse Sal = new Warehouse(0, "Sarga Salto", null);
            Warehouse Pay = new Warehouse(0, "Sarga Paysandú", null);
            Warehouse Pea = new Warehouse(0, "Sarga Peatonal Maldonado", null);
            _context.Warehouses.Add(PdE);
            _context.Warehouses.Add(Col);
            _context.Warehouses.Add(Sal);
            _context.Warehouses.Add(Pay);
            _context.Warehouses.Add(Pea);
            _context.SaveChanges();
        }
    }
}
