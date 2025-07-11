using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SharedUseCase.DTOs.User;
using SharedUseCase.InterfacesUC;

namespace IntegrationModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IGetAll<UserDto> _getAll;
        IGetById<UserDto> _getById;
        IGetByCi<UserDto> _getByCi;



        public UsersController(IGetAll<UserDto> getAll,
                                 IGetByCi<UserDto> getByCi,
                                 IGetById<UserDto> getById)
        {
            _getAll = getAll;
            _getById = getById;
            _getByCi = getByCi;
        }

        // GET: api/<ValuesController>
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

        [HttpGet("ci/{ci}")]
        public IActionResult GetByCi(string ci)
        {
            try
            {
                var user = _getByCi.Execute(ci);
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

    }
}
