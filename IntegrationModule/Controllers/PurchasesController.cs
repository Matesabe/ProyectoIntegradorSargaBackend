using Microsoft.AspNetCore.Mvc;
using SharedUseCase.DTOs.Product;
using SharedUseCase.DTOs.Purchase;
using SharedUseCase.InterfacesUC;

namespace IntegrationModule.Controllers
{
    public class PurchasesController : Controller
    {
        IGetAll<PurchaseDto> _getAll;
        IGetById<PurchaseDto> _getById;
        IAdd<PurchaseDto> _add;
        IUpdate<PurchaseDto> _update;
        IRemove<PurchaseDto> _remove;
        public PurchasesController(IGetAll<PurchaseDto> getAll,
                                 IGetById<PurchaseDto> getById,
                                 IAdd<PurchaseDto> add,
                                 IUpdate<PurchaseDto> update,
                                 IRemove<PurchaseDto> remove)
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
                var purchases = _getAll.Execute();
                return Ok(purchases);
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
        public IActionResult GetById(int id) {
            try
            {
                var purchase = _getById.Execute(id);
                if (purchase == null)
                {
                    return NotFound();
                }
                return Ok(purchase);
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
        public IActionResult Add([FromBody] PurchaseDto purchase)
        {
            try
            {
                if (purchase == null)
                {
                    return BadRequest("Purchase data is null.");
                }
                int addedPurchase = _add.Execute(purchase);
                return CreatedAtAction(nameof(GetById), GetById(addedPurchase), addedPurchase);
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
        public IActionResult Update(int id, [FromBody] PurchaseDto purchase)
        {
            try
            {
                if (purchase == null)
                {
                    return BadRequest("Purchase data is null.");
                }
                _update.Execute(id, purchase);
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

        [HttpDelete("{id}")]
        public IActionResult Delete(PurchaseDto pur) {
            try
            {
                _remove.Execute(pur);
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