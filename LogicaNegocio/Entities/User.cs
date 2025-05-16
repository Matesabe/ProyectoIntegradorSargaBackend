using BusinessLogic.VO;
using Libreria.LogicaNegocio.IntefacesDominio;
using System.Security.Principal;

namespace BusinessLogic.Entities
{
    public abstract class User : IEntity, IEquatable<User>
    {
        public int Id { get; set; }
        public string Ci { get; set; }
        public Name Name { get; set; }
        public Password Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

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
    }
}
