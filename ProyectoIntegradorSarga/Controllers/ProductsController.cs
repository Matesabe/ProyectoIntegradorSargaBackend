using Microsoft.AspNetCore.Mvc;
using SharedUseCase.DTOs.Product;
using SharedUseCase.InterfacesUC.Product;
using SharedUseCase.InterfacesUC;
using Microsoft.AspNetCore.Cors;

namespace ProyectoIntegradorSarga.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        IGetAll<ProductDto> _getAll;
        IGetById<ProductDto> _getById;
        IGetByBrand<ProductDto> _getByBrand;
        IGetByProductCode<ProductDto> _getByProductCode;
        public ProductsController(IGetAll<ProductDto> getAll,
                                 IGetById<ProductDto> getById,
                                 IGetByProductCode<ProductDto> getByProductCode,
                                 IGetByBrand<ProductDto> getByBrand)
        {
            _getAll = getAll;
            _getById = getById;
            _getByProductCode = getByProductCode;
            _getByBrand = getByBrand;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var products = _getAll.Execute();
                return Ok(products);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null
                    ? $"{ex.Message} - InnerException: {ex.InnerException.Message}"
                    : ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var product = _getById.Execute(id);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null
                    ? $"{ex.Message} - InnerException: {ex.InnerException.Message}"
                    : ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [HttpGet("by-code/{code}")]
        public IActionResult GetByProductCode(string code)
        {
            try
            {
                if (string.IsNullOrEmpty(code))
                {
                    return BadRequest("Product code cannot be null or empty.");
                }
                var products = _getByProductCode.Execute(code);
                var product = products.FirstOrDefault();
                return Ok(product);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null
                    ? $"{ex.Message} - InnerException: {ex.InnerException.Message}"
                    : ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [HttpGet("brand/{brand}")]
        public IActionResult GetByBrand(string brand)
        {
            try
            {
                if (string.IsNullOrEmpty(brand))
                {
                    return BadRequest("Brand cannot be null or empty.");
                }
                var products = _getByBrand.Execute(brand);
                return Ok(products);
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
