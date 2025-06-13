using BusinessLogic.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class Client : User
    {
        // Constructor requerido por EF Core
        public Client() : base() { }

        public Client(int id, string ci, Name name, Password password, Email email, string phone, string rol)
            : base(id, ci, name, password, email, phone)
        {
            Points = 0;
            Rol = rol;
            RecurrenceCount = 0;
        }

        public int Points { get; set; }
        public int RecurrenceCount { get; set; }

        protected override string RolUsuario()
        {
            return "Client";
        }

        
    }
}
