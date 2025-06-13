using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedUseCase.InterfacesUC.Product
{
    public interface IGetByProductCode<T>
    {
        IEnumerable<T> Execute(string code);
    }
}
