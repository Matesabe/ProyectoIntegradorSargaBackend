using BusinessLogic.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SharedUseCase.DTOs.Purchase;
using SharedUseCase.InterfacesUC;
using SharedUseCase.InterfacesUC.Purchase;

namespace ProyectoIntegradorSarga.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasesController : Controller
    {
        IGetPurchaseByClientId<PurchaseDto> _getByClientId;
        IGetById<PurchaseDto> _getById;

        public PurchasesController(IGetPurchaseByClientId<PurchaseDto> getByClientId, IGetById<PurchaseDto> getById)
        {
            _getByClientId = getByClientId;
            _getById = getById;
        }

        [Authorize]
        [HttpGet("client/{clientId}")]
        public IActionResult GetByClientId(int clientId)
        {
            try
            {
                if (clientId <= 0)
                {
                    return BadRequest("Client ID must be greater than zero.");
                }
                var purchases = _getByClientId.Execute(clientId);
                var idLogueado = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
                var rolLogueado = User.Claims.FirstOrDefault(c => c.Type == "Rol")?.Value;

                if (rolLogueado != "Administrator")
                {
                        if (string.IsNullOrEmpty(idLogueado))
                    {
                        return BadRequest("No se pudo obtener el id del usuario logueado.");
                    }
                }

                // Verificar que todas las compras tengan el id de usuario del usuario logueado
                if (purchases.Any(p => p.Client.Id.ToString() != idLogueado))
                {
                    return BadRequest("Usuario con id inválido para acceder a estas compras.");
                }
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
                var idLogueado = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
                var rolLogueado = User.Claims.FirstOrDefault(c => c.Type == "Rol")?.Value;

                if(rolLogueado != "Administrator")
                {  
                    if (string.IsNullOrEmpty(idLogueado))
                    {
                        return BadRequest("No se pudo obtener el id del usuario logueado.");
                    }
                }

                // Verificar que todas las compras tengan el id de usuario del usuario logueado
                if (purchase.Client.Id.ToString() != idLogueado)
                {
                    return BadRequest("Usuario con id inválido para acceder a esta compra.");
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
    }
}