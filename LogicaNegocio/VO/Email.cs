using Libreria.LogicaNegocio.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.VO
{
    public record Email
    {
        public string Value { get; private set; }
        public Email(string value)
        {
            Value = value;
            Validate();
        }
        private void Validate()
        {
            if (string.IsNullOrEmpty(Value))
                throw new EmailException("Email inválido");
            if (!Value.Contains("@"))
                throw new EmailException("Email inválido");
            if (!Value.Contains("."))
                throw new EmailException("Email inválido");
        }
    }
}
