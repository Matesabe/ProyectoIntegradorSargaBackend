using Microsoft.AspNetCore.Mvc;
using SharedUseCase.DTOs.Product;
using SharedUseCase.InterfacesUC;

namespace IntegrationModule.Controllers
{
    public class ProductsController : Controller
    {
        IGetAll<ProductDto> _getAll;
        IGetById<ProductDto> _getById;
        IAdd<ProductDto> _add;
        IUpdate<ProductDto> _update;
        IRemove<ProductDto> _remove;
        public ProductsController(IGetAll<ProductDto> getAll,
                                 IGetById<ProductDto> getById,
                                 IAdd<ProductDto> add,
                                 IUpdate<ProductDto> update,
                                 IRemove<ProductDto> remove)
        {
            _getAll = getAll;
            _getById = getById;
            _add = add;
            _update = update;
            _remove = remove;
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

        [HttpPost]
        public IActionResult Add([FromBody] ProductDto product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest("El producto no puede ser nulo.");
                }
                int addedProductId = _add.Execute(product);
                return CreatedAtAction(nameof(GetById), new { id = addedProductId }, GetById(addedProductId));
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
        public IActionResult Update(int id, [FromBody] ProductDto product)
        {
            try
            {
                if (product == null || product.id != id)
                {
                    return BadRequest("El producto no puede ser nulo y debe coincidir con el ID.");
                }
                _update.Execute(product.id, product);
                return Ok(GetById(product.id));
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
        public IActionResult Delete(ProductDto pro) {
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
    }
}
