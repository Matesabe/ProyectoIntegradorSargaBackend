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
        public ProductsController(IGetAll<ProductDto> getAll,
                                 IGetById<ProductDto> getById)
        {
            _getAll = getAll;
            _getById = getById;
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
    }
}
