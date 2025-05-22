using BusinessLogic.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class Seller : User
    {
        // Constructor requerido por EF Core
        public Seller() : base() { }

        public Seller(int id, string ci, Name name, Password password, Email email, string phone, string rol)
            : base(id, ci, name, password, email, phone)
        {
            Rol = rol;
        }

        protected override string RolUsuario()
        {
            return "Seller";
        }
    }
}
