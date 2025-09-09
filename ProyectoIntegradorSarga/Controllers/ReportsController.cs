using Microsoft.AspNetCore.Mvc;
using SharedUseCase.DTOs.Purchase;
using SharedUseCase.InterfacesUC.Purchase;
using SharedUseCase.InterfacesUC;
using SharedUseCase.DTOs.Reports;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace IntegrationModule.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : Controller
    {
        IGetAll<ReportDto> _getAll;
        IAdd<ReportDto> _add;
        
        public ReportsController(IGetAll<ReportDto> getAll,
                                 IAdd<ReportDto> add)
        {
            _getAll = getAll;
            _add = add;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            var userRole = User.Claims
                .FirstOrDefault(c => c.Type == "Rol")?.Value;
            if (userRole != "Administrator")
            {
                return BadRequest("No tienes el rol necesario para acceder.");
            }
                try
            {
                var reports = _getAll.Execute();
                return Ok(reports);
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
