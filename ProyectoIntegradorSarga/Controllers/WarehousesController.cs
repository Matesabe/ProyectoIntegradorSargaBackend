using Microsoft.AspNetCore.Mvc;
using SharedUseCase.DTOs.Product;
using SharedUseCase.InterfacesUC.Product;
using SharedUseCase.InterfacesUC;
using SharedUseCase.InterfacesUC.Warehouse;
using SharedUseCase.DTOs.Warehouse;
using Microsoft.AspNetCore.Cors;

namespace IntegrationModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class WarehousesController : Controller
    {
        IClearStocks _clearStocks;
        IUpdateStocks _updateStocks;
        IGetAll<WarehouseDto> _getAll;

        public WarehousesController(IGetAll<WarehouseDto> getAll)
        {
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
            return BadRequest(new { error = errorMessage});
            }
        }
    }
}
