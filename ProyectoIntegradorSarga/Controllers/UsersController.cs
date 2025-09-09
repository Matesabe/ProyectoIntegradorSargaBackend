
using AppLogic.Mapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SharedUseCase.DTOs.User;
using SharedUseCase.InterfacesUC;
using Xunit.Abstractions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyectoIntegradorSarga.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IGetAll<UserDto> _getAll;
        IGetById<UserDto> _getById;
        IAdd<UserDto> _add;
        IUpdate<UserDto> _update;
        IRemove<UserDto> _remove;
        IGetByName<UserDto> _getByName;
        IGetByEmail<UserDto> _getByEmail;
        IGetByCi<UserDto> _getByCi;


        public UsersController(IGetAll<UserDto> getAll,
                                 IAdd<UserDto> add,
                                 IRemove<UserDto> remove,
                                 IGetByName<UserDto> getByName,
                                 IGetById<UserDto> getById,
                                 IUpdate<UserDto> update,
                                 IGetByEmail<UserDto> getByEmail,
                                 IGetByCi<UserDto> getByCi)
        {
            _getAll = getAll;
            _add = add;
            _remove = remove;
            _getByName = getByName;
            _getById = getById;
            _update = update;
            _getByEmail = getByEmail;
            _getByCi = getByCi;
        }

        // GET: api/<ValuesController>
        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var users = _getAll.Execute();
                return Ok(users);
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
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var rol = User.Claims.FirstOrDefault(c => c.Type == "Rol")?.Value;
                var idToken = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value);
                if (rol == "Administrator" || idToken == id)
                {
                    var user = _getById.Execute(id);
                    if (user == null)
                    {
                        return NotFound();
                    }
                    return Ok(user);
                }
                else {
                    return BadRequest("Usuario con rol inválido para acceder a usuario.");
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null
                    ? $"{ex.Message} - InnerException: {ex.InnerException.Message}"
                    : ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetByCi/{ci}")]
        public IActionResult GetByCi(string ci)
        {
            try
            {
                var rol = User.Claims.FirstOrDefault(c => c.Type == "Rol")?.Value;
                if (rol == "Administrator" || rol == "Seller")
                {
                    var user = _getByCi.Execute(ci);
                    if (user == null)
                    {
                        return NotFound();
                    }
                    return Ok(user);
                }
                else
                {
                    return BadRequest("Usuario con rol inválido acceder a usuario.");
                }
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
        [HttpPost]
        public IActionResult Create(UserDto user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("El objeto no puede ser nulo");
                }
                if (user.Rol == "Client")
                {
                    int id = _add.Execute(user);
                    UserDto userCreated = _getByEmail.Execute(user.Email);
                    return CreatedAtAction(nameof(GetById), new { id }, userCreated);
                }
                else if (user.Rol == "Seller")
                {
                    return CreateSeller(user);
                }
                else if (user.Rol == "Administrator")
                {
                    return CreateAdmin(user);
                }
                return BadRequest("Rol no válido");
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null
                    ? $"{ex.Message} - InnerException: {ex.InnerException.Message}"
                    : ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [Authorize]
        [HttpPost("Seller")]
        public IActionResult CreateSeller(UserDto user)
        {
            try
            {
                var rol = User.Claims.FirstOrDefault(c => c.Type == "Rol")?.Value;
                if (rol != "Administrator")
                {
                    return BadRequest("Usuario con rol inválido para crear vendedor.");
                }
                if (user == null)
                {
                    return BadRequest("El objeto no puede ser nulo");
                }

                int id = _add.Execute(user);
                UserDto userCreated = _getByEmail.Execute(user.Email);
                return CreatedAtAction(nameof(GetById), new { id }, userCreated);

            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null
                    ? $"{ex.Message} - InnerException: {ex.InnerException.Message}"
                    : ex.Message;
                return BadRequest(errorMessage);
            }
        }

        [Authorize]
        [HttpPost("Administrator")]
        public IActionResult CreateAdmin(UserDto user)
        {
            try
            {
                var rol = User.Claims.FirstOrDefault(c => c.Type == "Rol")?.Value;
                if (rol != "Administrator")
                {
                    return Forbid("Usuario con rol inválido para crear administrador.");
                }

                if (user == null)
                {
                    return BadRequest("El objeto no puede ser nulo");
                }

                int id = _add.Execute(user);
                UserDto userCreated = _getByEmail.Execute(user.Email);
                return CreatedAtAction(nameof(GetById), new { id }, userCreated);

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
        public IActionResult Update(UserDto user, int id) {
            try {
                var rol = User.Claims.FirstOrDefault(c => c.Type == "Rol")?.Value;
                if (rol != "Administrator")
                {
                    return Forbid("Usuario con rol inválido para eliminar un ususario.");
                }
                if (user == null)
                {
                    return BadRequest("El objeto no puede ser nulo");
                }
                _update.Execute(id, user);
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
                    return Forbid("Usuario con rol inválido para eliminar un ususario.");
                }
                var user = _getById.Execute(id);
                _remove.Execute(user);
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
