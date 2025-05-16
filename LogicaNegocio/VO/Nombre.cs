using BusinessLogic.Exceptions.UserExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.VO
{
    public record Name
    {
        public string Value { get; private set; }

        public Name(string value)
        {
            Value = value;
            Validar();
        }

        private void Validar()
        {
            if (string.IsNullOrEmpty(Value))
                throw new NameException("Nombre inválido");
        }
    }
}
