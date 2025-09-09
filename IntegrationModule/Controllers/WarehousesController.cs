using Microsoft.AspNetCore.Mvc;
using SharedUseCase.DTOs.Product;
using SharedUseCase.InterfacesUC.Product;
using SharedUseCase.InterfacesUC;
using SharedUseCase.InterfacesUC.Warehouse;
using SharedUseCase.DTOs.Warehouse;

namespace IntegrationModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehousesController : ControllerBase
    {
        private readonly IClearStocks _clearStocks;
        private readonly IUpdateStocks _updateStocks;
        private readonly IGetAll<WarehouseDto> _getAll;

        public WarehousesController(IClearStocks clearStocks,
                                    IUpdateStocks updateStocks,
                                    IGetAll<WarehouseDto> getAll)
        {
            _clearStocks = clearStocks;
            _updateStocks = updateStocks;
            _getAll = getAll;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var warehouses = _getAll.Execute();
                return Ok(warehouses);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null
                    ? $"{ex.Message} - InnerException: {ex.InnerException.Message}"
                    : ex.Message;
                return BadRequest(new { error = errorMessage });
            }
        }

        [HttpPut("clear-stocks")]
        public IActionResult ClearStocks()
        {
            try
            {
                _clearStocks.Execute();
                return Ok(new { message = "Stocks cleared successfully." });
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null
                    ? $"{ex.Message} - InnerException: {ex.InnerException.Message}"
                    : ex.Message;
                return BadRequest(new { error = errorMessage });
            }
        }

        [HttpPut("update-stocks")]
        public IActionResult UpdateStocks([FromBody] SubProductDto sub)
        {
            try
            {
                if (sub == null)
                {
                    return BadRequest(new { error = "El subproducto no puede ser nulo." });
                }

                if (sub.Id <= 0)
                {
                    return BadRequest(new { error = "El ID de subproducto es inválido." });
                }

                if (new[] { sub.stockPdelE, sub.stockCol, sub.stockPay, sub.stockPeat, sub.stockSal }
                    .Any(stock => stock < 0))
                {
                    return BadRequest(new { error = "Las cantidades de stock no pueden ser negativas." });
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
