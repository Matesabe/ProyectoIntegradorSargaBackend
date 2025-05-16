using BusinessLogic.Exceptions.UserExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLogic.VO
{
    public record Password
    {
        public string value { get; private set; }
        public Password(string value)
        {
            if (string.IsNullOrWhiteSpace(value) ||
                value.Length < 8 ||
                !Regex.IsMatch(value, @"\d") ||
                !Regex.IsMatch(value, @"[A-Z]"))
            {
                throw new PassException("La contraseña debe tener al menos 8 caracteres, un número y una letra mayúscula.");
            }
            this.value = value;
        }
    }
}
