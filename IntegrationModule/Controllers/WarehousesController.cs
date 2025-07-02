using Microsoft.AspNetCore.Mvc;
using SharedUseCase.DTOs.Product;
using SharedUseCase.InterfacesUC.Product;
using SharedUseCase.InterfacesUC;
using SharedUseCase.InterfacesUC.Warehouse;

namespace IntegrationModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehousesController : Controller
    {
        IClearStocks _clearStocks;
        IUpdateStocks _updateStocks;

        public WarehousesController(IClearStocks clearStocks,
                                    IUpdateStocks updateStocks)
        {
         _clearStocks = clearStocks;
         _updateStocks = updateStocks;
        }

        [HttpPut("clear-stocks")]
        public IActionResult ClearStocks()
        {
            try
            {
                _clearStocks.Execute();
                return Ok("Stocks cleared successfully.");
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null
                    ? $"{ex.Message} - InnerException: {ex.InnerException.Message}"
                    : ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [HttpPut("update-stocks")]
        public IActionResult UpdateStocks(SubProductDto sub)
        {
            try
            {
                if (sub == null)
                {
                    return BadRequest(new { error = "El subproducto no puede ser nulo." });
                }
                _updateStocks.Execute(sub, (int)sub.stockPdelE, (int)sub.stockCol, (int)sub.stockPay, (int)sub.stockPeat, (int)sub.stockSal);
                return Ok(new { message = "Stocks updated successfully." });
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null
                    ? $"{ex.Message} - InnerException: {ex.InnerException.Message}"
                    : ex.Message;
                return BadRequest(new { error = errorMessage });
            }
        }
    }
}
