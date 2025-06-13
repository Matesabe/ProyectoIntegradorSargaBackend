using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SharedUseCase.DTOs.Product;
using SharedUseCase.DTOs.Promotion;
using SharedUseCase.InterfacesUC;
using SharedUseCase.InterfacesUC.Product;

namespace ProyectoIntegradorSarga.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class SubProductsController : Controller
    {
        IGetAll<SubProductDto> _getAll;
        IGetById<SubProductDto> _getById;
        IGetByProductId<SubProductDto> _getByProductId;
        IGetByProductCode<SubProductDto> _getByProductCode;
        public SubProductsController(IGetAll<SubProductDto> getAll,
                                 IGetById<SubProductDto> getById,
                                 IGetByProductId<SubProductDto> getByProductId,
                                 IGetByProductCode<SubProductDto> getByProductCode)
        {
            _getAll = getAll;
            _getById = getById;
            _getByProductId = getByProductId;
            _getByProductCode = getByProductCode;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var subProducts = _getAll.Execute();
                return Ok(subProducts);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null
                    ? $"{ex.Message} - InnerException: {ex.InnerException.Message}"
                    : ex.Message;
                return BadRequest(errorMessage);
            }
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var subProduct = _getById.Execute(id);
                if (subProduct == null)
                {
                    return NotFound();
                }
                return Ok(subProduct);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null
                    ? $"{ex.Message} - InnerException: {ex.InnerException.Message}"
                    : ex.Message;
                return BadRequest(errorMessage);
            }
        }

        public IActionResult GetByProductId(int productId)
        {
            try
            {
                var subProducts = _getByProductId.Execute(productId);
                if (subProducts == null || !subProducts.Any())
                {
                    return NotFound();
                }
                return Ok(subProducts);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null
                    ? $"{ex.Message} - InnerException: {ex.InnerException.Message}"
                    : ex.Message;
                return BadRequest(errorMessage);
            }
        }

        public IActionResult GetByProductCode(string productCode)
        {
            try
            {
                var subProduct = _getByProductCode.Execute(productCode);
                if (subProduct == null)
                {
                    return NotFound();
                }
                return Ok(subProduct);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null
                    ? $"{ex.Message} - InnerException: {ex.InnerException.Message}"
                    : ex.Message;
                return BadRequest(errorMessage);
            }
        }
    }
}
