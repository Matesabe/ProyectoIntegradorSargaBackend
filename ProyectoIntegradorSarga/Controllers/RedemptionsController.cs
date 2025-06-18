using BusinessLogic.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SharedUseCase.DTOs.Redemption;
using SharedUseCase.InterfacesUC;
using SharedUseCase.InterfacesUC.Redemption;

namespace ProyectoIntegradorSarga.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class RedemptionsController : Controller
    {
        IGetAll<RedemptionDto> _getAll;
        IGetById<RedemptionDto> _getById;
        IAdd<RedemptionDto> _add;
        IUpdate<RedemptionDto> _update;
        IRemove<RedemptionDto> _remove;
        IGetRedemptionByUserId<RedemptionDto> _getRedemptionByUserId;

        public RedemptionsController(IGetAll<RedemptionDto> getAll, IGetById<RedemptionDto> getById, IAdd<RedemptionDto> add, IUpdate<RedemptionDto> update, IRemove<RedemptionDto> remove, IGetRedemptionByUserId<RedemptionDto> getRedemptionByUserId)
        {
            _getAll = getAll;
            _getById = getById;
            _add = add;
            _update = update;
            _remove = remove;
            _getRedemptionByUserId = getRedemptionByUserId;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAll() {
            try
            {
                // Verificar si el usuario tiene el rol de administrador
                var rol = User.Claims.FirstOrDefault(c => c.Type == "Rol")?.Value;
                if (rol != "Administrator")
                {
                    return BadRequest("Usuario con rol inválido para acceder a todos los canjes.");
                }
                var redemptions = _getAll.Execute();
                return Ok(redemptions);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null
                    ? $"{ex.Message} - InnerException: {ex.InnerException.Message}"
                    : ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("byId/{redemptionId}")]
        public IActionResult GetById(int id)
        {
            try
            {
                // Verificar si el usuario tiene el rol de administrador
                var rol = User.Claims.FirstOrDefault(c => c.Type == "Rol")?.Value;
                var idLogueado = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
                var redemption = _getById.Execute(id);
                if (idLogueado != redemption.Client.Id.ToString())
                {
                    if (rol != "Administrator")
                    {
                        return BadRequest("Usuario con rol inválido para acceder al canje.");
                    }
                }
                if (redemption == null)
                {
                    return NotFound();
                }
                return Ok(redemption);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null
                    ? $"{ex.Message} - InnerException: {ex.InnerException.Message}"
                    : ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("byUserId/{userId}")]
        public IActionResult GetByUserId(int userId)
        {
            try
            {
                var rol = User.Claims.FirstOrDefault(c => c.Type == "Rol")?.Value;
                var idLogueado = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
                if (idLogueado != userId.ToString())
                {
                    if (rol != "Administrator")
                    {
                        return BadRequest("Usuario con rol inválido para acceder a las redenciones.");
                    }   
                }
                var redemptions = _getRedemptionByUserId.Execute(userId);
                return Ok(redemptions);
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
        [Authorize]
        public IActionResult Add([FromBody] RedemptionDto redemption)
        {
            try
            {
                var rol = User.Claims.FirstOrDefault(c => c.Type == "Rol")?.Value;
                if (rol != "Administrator" || rol != "Seller")
                {
                    return BadRequest("Usuario con rol inválido para dar de alta un canje.");
                }
                _add.Execute(redemption);
                return Ok();
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null
                    ? $"{ex.Message} - InnerException: {ex.InnerException.Message}"
                    : ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [HttpPut]
        [Authorize]
        [Route("update/{id}")]
        public IActionResult Update(int id, [FromBody] RedemptionDto redemption)
        {
            try
            {
                var rol = User.Claims.FirstOrDefault(c => c.Type == "Rol")?.Value;
                if (rol != "Administrator" || rol != "Seller")
                {
                    return BadRequest("Usuario con rol inválido para actualizar un canje.");
                }
                _update.Execute(id, redemption);
                return Ok();
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
        [Authorize]
        [Route("remove/{id}")]
        public IActionResult Delete(RedemptionDto red) {
            try
            {
                var rol = User.Claims.FirstOrDefault(c => c.Type == "Rol")?.Value;
                if (rol != "Administrator")
                {
                    return BadRequest("Usuario con rol inválido para eliminar un canje.");
                }
                _remove.Execute(red);
                return Ok();
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
