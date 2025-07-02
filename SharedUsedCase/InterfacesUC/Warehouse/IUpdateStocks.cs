using BusinessLogic.Entities;
using SharedUseCase.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedUseCase.InterfacesUC.Warehouse
{
    public interface IUpdateStocks
    {
        void Execute(SubProductDto sub, int stockPdelE, int stockCol, int stockPay, int stockPeat, int stockSal);
    }
}
