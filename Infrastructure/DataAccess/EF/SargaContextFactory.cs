using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess.EF
{
    // Archivo: Infrastructure/DataAccess/EF/SargaContextFactory.cs
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;

    namespace Infrastructure.DataAccess.EF
    {
        public class SargaContextFactory /*: IDesignTimeDbContextFactory<SargaContext>*/
        {
            //public SargaContext CreateDbContext(string[] args)
            //{
            //    var optionsBuilder = new DbContextOptionsBuilder<SargaContext>();
            //    optionsBuilder.UseSqlServer(@"
            //    Data Source=(localdb)\MSSQLLocalDB;
            //    Initial Catalog=PruebaUsuario;
            //    Integrated Security=True;");

            //    return new SargaContext(optionsBuilder.Options);
            //}
        }
    }

}
