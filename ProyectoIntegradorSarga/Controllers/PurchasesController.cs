using BusinessLogic.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SharedUseCase.DTOs.Purchase;
using SharedUseCase.InterfacesUC;
using SharedUseCase.InterfacesUC.Purchase;

namespace ProyectoIntegradorSarga.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PurchasesController : Controller
    {
        private readonly IGetAll<PurchaseDto> _getAllPurchases;
        private readonly IGetPurchaseByClientId<PurchaseDto> _getByClientId;
        private readonly IGetById<PurchaseDto> _getById;

        public PurchasesController(
            IGetAll<PurchaseDto> getAll,
            IGetPurchaseByClientId<PurchaseDto> getByClientId,
            IGetById<PurchaseDto> getById)
        {
            _getAllPurchases = getAll;
            _getByClientId = getByClientId;
            _getById = getById;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var rolLogueado = User.Claims.FirstOrDefault(c => c.Type == "Rol")?.Value;
                var idLogueado = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;

                if (rolLogueado == "Administrator")
                {
                    return Ok(_getAllPurchases.Execute());
                }

                if (string.IsNullOrEmpty(idLogueado))
                    return BadRequest("No se pudo obtener el id del usuario logueado.");

                var purchases = _getByClientId.Execute(int.Parse(idLogueado));
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

        [HttpGet("client/{clientId}")]
        public IActionResult GetByClientId(int clientId)
        {
            try
            {
                if (clientId <= 0)
                    return BadRequest("Client ID must be greater than zero.");

                var rolLogueado = User.Claims.FirstOrDefault(c => c.Type == "Rol")?.Value;
                var idLogueado = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;

                if (rolLogueado != "Administrator" && idLogueado != clientId.ToString())
                    return StatusCode(403, "Usuario con id inválido para acceder a estas compras.");

                var purchases = _getByClientId.Execute(clientId);
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
        public IActionResult GetById(int id)
        {
            try
            {
                var purchase = _getById.Execute(id);
                if (purchase == null)
                    return NotFound();

                var rolLogueado = User.Claims.FirstOrDefault(c => c.Type == "Rol")?.Value;
                var idLogueado = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;

                if (rolLogueado != "Administrator" && purchase.Client.Id.ToString() != idLogueado)
                    return Forbid("Usuario con id inválido para acceder a esta compra.");

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
    }

}