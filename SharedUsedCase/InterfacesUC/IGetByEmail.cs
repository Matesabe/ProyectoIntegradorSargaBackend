using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedUseCase.InterfacesUC
{
    public interface IGetByEmail<T>
    {
        T Execute(string email);
    }
}
