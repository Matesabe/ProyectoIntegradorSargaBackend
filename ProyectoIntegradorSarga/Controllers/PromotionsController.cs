using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SharedUseCase.DTOs.Promotion;
using SharedUseCase.DTOs.User;
using SharedUseCase.InterfacesUC;

namespace ProyectoIntegradorSarga.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionsController : ControllerBase
    {
        IGetAll<PromotionDto> _getAll;
        IGetById<PromotionDto> _getById;
        IAdd<PromotionDto> _add;
        IUpdate<PromotionDto> _update;
        IRemove _remove;
        public PromotionsController(IGetAll<PromotionDto> getAll,
                                 IAdd<PromotionDto> add,
                                 IRemove remove,
                                 IGetById<PromotionDto> getById,
                                 IUpdate<PromotionDto> update)
        {
            _getAll = getAll;
            _add = add;
            _remove = remove;
            _getById = getById;
            _update = update;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var promotions = _getAll.Execute();
                return Ok(promotions);
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
                var promotion = _getById.Execute(id);
                if (promotion == null)
                {
                    return NotFound();
                }
                return Ok(promotion);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null
                    ? $"{ex.Message} - InnerException: {ex.InnerException.Message}"
                    : ex.Message;
                return BadRequest(errorMessage);
            }
        }

        // POST api/<ValuesController>
        [Authorize]
        [HttpPost]
        public IActionResult Create(PromotionDto promotion)
        {
            try
            {
                var rol = User.Claims.FirstOrDefault(c => c.Type == "Rol")?.Value;
                if (rol != "Administrator")
                {
                    return BadRequest("Usuario con rol inválido para crear administrador.");
                }
                if (promotion == null)
                {
                    return BadRequest("El objeto no puede ser nulo");
                }
                switch (promotion.Type)
                {
                    case "Date":
                        if (promotion.PromotionDateStart == default || promotion.PromotionDateEnd == default)
                        {
                            return BadRequest("Las fechas de inicio y fin no pueden estar vacías");
                        }
                        break;
                    case "Products":
                        if (promotion.PromotionProducts == null || !promotion.PromotionProducts.Any())
                        {
                            return BadRequest("La lista de productos no puede estar vacía al dar de alta una promoción de productos.");
                        }
                        break;
                    case "Recurrence":
                        if (promotion.RecurrenceValue <= 0 || promotion.PointsPerRecurrence <= 0)
                        {
                            return BadRequest("El campo 'Recurrence' no puede estar vacío, los puntos por recurrencia no pueden ser igual o menores a 0.");
                        }
                        break;
                    case "Amount":
                        if (promotion.AmountPerPoint <= 0)
                        {
                            return BadRequest("El campo 'Amount' debe ser mayor que 0.");
                        }
                        break;
                    default:
                        return BadRequest("Tipo de promoción desconocido");
                }
                _add.Execute(promotion);
                return Ok("Promoción creada exitosamente.");
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null
                    ? $"{ex.Message} - InnerException: {ex.InnerException.Message}"
                    : ex.Message;
                return BadRequest(errorMessage);
            }
        }

        // PUT api/<ValuesController>/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(PromotionDto promotion, int id)
        {
            try
            {
                var rol = User.Claims.FirstOrDefault(c => c.Type == "Rol")?.Value;
                if (rol != "Administrator")
                {
                    return BadRequest("Usuario con rol inválido para actualizar promociones.");
                }
                if (promotion == null)
                {
                    return BadRequest("El objeto no puede ser nulo");
                }
                if (promotion.Id != id)
                {
                    return BadRequest("El ID de la promoción no coincide con el ID proporcionado en la URL.");
                }
                switch (promotion.Type)
                {
                    case "Date":
                        if (promotion.PromotionDateStart == default || promotion.PromotionDateEnd == default)
                        {
                            return BadRequest("Las fechas de inicio y fin no pueden estar vacías");
                        }
                        break;
                    case "Products":
                        if (promotion.PromotionProducts == null || !promotion.PromotionProducts.Any())
                        {
                            return BadRequest("La lista de productos no puede estar vacía al dar de alta una promoción de productos.");
                        }
                        break;
                    case "Recurrence":
                        if (promotion.RecurrenceValue <= 0 || promotion.PointsPerRecurrence <= 0)
                        {
                            return BadRequest("El campo 'Recurrence' no puede estar vacío, los puntos por recurrencia no pueden ser igual o menores a 0.");
                        }
                        break;
                    case "Amount":
                        if (promotion.AmountPerPoint <= 0)
                        {
                            return BadRequest("El campo 'Amount' debe ser mayor que 0.");
                        }
                        break;
                    default:
                        return BadRequest("Tipo de promoción desconocido");
                }
                _update.Execute(id, promotion);
                return Ok("Promoción actualizada exitosamente.");
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null
                    ? $"{ex.Message} - InnerException: {ex.InnerException.Message}"
                    : ex.Message;
                return BadRequest(errorMessage);
            }
        }

        // DELETE api/<ValuesController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var rol = User.Claims.FirstOrDefault(c => c.Type == "Rol")?.Value;
                if (rol != "Administrator")
                {
                    return BadRequest("Usuario con rol inválido para actualizar promociones.");
                }
                _remove.Execute(id);
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
