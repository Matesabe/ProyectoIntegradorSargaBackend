using Microsoft.AspNetCore.Mvc;
using SharedUseCase.DTOs.Product;
using SharedUseCase.InterfacesUC;
using SharedUseCase.InterfacesUC.Product;

namespace IntegrationModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubProductsController : Controller
    {
        IGetAll<SubProductDto> _getAll;
        IGetAll<ProductDto> _getAllProducts;
        IGetById<SubProductDto> _getById;
        IAdd<SubProductDto> _add;
        IUpdate<SubProductDto> _update;
        IRemove<SubProductDto> _remove;
        IClearSubProducts _clearSubProducts;
        IGetByProductCode<ProductDto> _getByProductCode;
        IGetByProductCode<SubProductDto> _getSubsByProductCode;
        public SubProductsController(IGetAll<SubProductDto> getAll,
                                 IGetAll<ProductDto> getAllProducts,
                                 IGetById<SubProductDto> getById,
                                 IAdd<SubProductDto> add,
                                 IUpdate<SubProductDto> update,
                                 IRemove<SubProductDto> remove,
                                 IClearSubProducts clearSubProducts,
                                 IGetByProductCode<ProductDto> getProductByCode,
                                 IGetByProductCode<SubProductDto> getSubsByProductCode)
        {
            _getAll = getAll;
            _getAllProducts = getAllProducts;
            _getById = getById;
            _add = add;
            _update = update;
            _remove = remove;
            _clearSubProducts = clearSubProducts;
            _getByProductCode = getProductByCode;
            _getSubsByProductCode = getSubsByProductCode;

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

        [HttpGet("products")]
        public IActionResult GetAllProducts()
        {
            try
            {
                var products = _getAllProducts.Execute();
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

        [HttpPost]
        public IActionResult Add([FromBody] SubProductDto product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest("El producto no puede ser nulo.");
                }
                int addedProductId = _add.Execute(product);
                return Ok(addedProductId);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null
                    ? $"{ex.Message} - InnerException: {ex.InnerException.Message}"
                    : ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] SubProductDto product)
        {
            try
            {
                if (product == null || product.Id != id)
                {
                    return BadRequest("El producto no puede ser nulo y debe coincidir con el ID.");
                }
                _update.Execute(product.Id, product);
                return Ok(GetById(product.Id));
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null
                    ? $"{ex.Message} - InnerException: {ex.InnerException.Message}"
                    : ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [HttpDelete("clear")]
        public IActionResult ClearSubProducts() {
            try {
                _clearSubProducts.Execute();
                return NoContent();
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null
                    ? $"{ex.Message} - InnerException: {ex.InnerException.Message}"
                    : ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [HttpDelete]
        public IActionResult Delete(SubProductDto pro) {
            try
            {
                if (pro == null)
                {
                    return BadRequest("El producto no puede ser nulo.");
                }
                _remove.Execute(pro);
                return NoContent();
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null
                    ? $"{ex.Message} - InnerException: {ex.InnerException.Message}"
                    : ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [HttpGet("byProductCode/{code}")]
        public IActionResult GetSubProductsByProductCode(string code) {
            try
            {
                var products = _getSubsByProductCode.Execute(code);
                if (products == null || !products.Any())
                {
                    return NotFound();
                }
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

        [HttpGet("products/{code}")]
        public IActionResult GetProductByCode(string code)
        {
            try
            {
                var product = _getByProductCode.Execute(code);
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
