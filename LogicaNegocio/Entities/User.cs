using BusinessLogic.VO;
using Libreria.LogicaNegocio.IntefacesDominio;
using System.Security.Principal;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BusinessLogic.Entities
{
    public abstract class User : IEntity, IEquatable<User>
    {
        public int Id { get; set; }
        public string Ci { get; set; }
        public Name Name { get; set; }
        public Password Password { get; set; }
        public Email Email { get; set; }
        public string Phone { get; set; }
        public string Rol { get; set; } = string.Empty;

        protected User() { }

        public User(int id, string ci, Name name, Password password, Email email, string phone)
        {
            Id = id;
            Ci = ci;
            Name = name;
            Password = password;
            Email = email;
            Phone = phone;
            Rol = RolUsuario();
            Validar();
        }

        protected abstract string RolUsuario();

        public void validarCi()
        {
            if (string.IsNullOrWhiteSpace(Ci))
                throw new ArgumentException("CI no puede ser vacía.");
            if (Ci.Length != 8)
                throw new ArgumentException("CI debe de tener 8 caracteres.");
            if (!int.TryParse(Ci, out _))
                throw new ArgumentException("CI debe de escribirse solo con números, incluyendo dígito verificador, sin guión ni puntos.");
        }

        public void validarPhone()
        {
            if (string.IsNullOrWhiteSpace(Phone)) throw new ArgumentException("Teléfono vacío.");
            if (!int.TryParse(Phone, out _))
                throw new ArgumentException("El número de teléfono deben ser solo números.");
        }


        public void Validar() {
            validarCi();
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as User);
        }

        public bool Equals(User? other)
        {
            if (other is null)
                return false;

            return Id == other.Id ||
                   Ci == other.Ci ||
                   Email == other.Email ||
                   Phone == other.Phone;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Ci, Email, Phone);
        }

        public void Update(User obj)
        {
            Name = obj.Name;
            Email = obj.Email;
            Phone = obj.Phone;
            Validar();
        }
    }
}
