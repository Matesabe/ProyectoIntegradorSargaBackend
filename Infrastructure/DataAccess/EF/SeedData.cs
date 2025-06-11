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
        }

        private void loadAdmin()
        {
            Administrator unA = null;
            unA = new Administrator(0, "87654321", new Name("Administrador Sarga"), new Password("SargaPass983"), new Email("adminSarga@mail.com"), "123456789", "Administrator" );
            _context.Administrators.Add(unA);
            _context.SaveChanges();
        }
     
    }
}
