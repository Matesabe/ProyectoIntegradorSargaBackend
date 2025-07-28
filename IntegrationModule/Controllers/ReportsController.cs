using Microsoft.AspNetCore.Mvc;
using SharedUseCase.DTOs.Purchase;
using SharedUseCase.InterfacesUC.Purchase;
using SharedUseCase.InterfacesUC;
using SharedUseCase.DTOs.Reports;

namespace IntegrationModule.Controllers
{
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
        public IActionResult GetAll()
        {
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

        [HttpPost]
        public IActionResult Add([FromBody] ReportDto report)
        {
            try
            {
                if (report == null)
                {
                    return BadRequest("Purchase data is null.");
                }
                int addedReportId = _add.Execute(report);
                return Ok(addedReportId);
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
