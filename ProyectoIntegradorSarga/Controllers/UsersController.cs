
using AppLogic.Mapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SharedUseCase.DTOs.User;
using SharedUseCase.InterfacesUC;

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
        IRemove _remove;
        IGetByName<UserDto> _getByName;
        IGetByEmail<UserDto> _getByEmail;


        public UsersController(IGetAll<UserDto> getAll,
                                 IAdd<UserDto> add,
                                 IRemove remove,
                                 IGetByName<UserDto> getByName,
                                 IGetById<UserDto> getById,
                                 IUpdate<UserDto> update,
                                 IGetByEmail<UserDto> getByEmail)
        {
            _getAll = getAll;
            _add = add;
            _remove = remove;
            _getByName = getByName;
            _getById = getById;
            _update = update;
            _getByEmail = getByEmail;
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
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var user = _getById.Execute(id);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
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
                    return BadRequest("Usuario con rol inválido para crear administrador.");
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
        [HttpPut("{id}")]
        public IActionResult Update(UserDto user, int id) {
            try {
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
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
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
