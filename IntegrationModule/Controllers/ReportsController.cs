using Microsoft.AspNetCore.Mvc;
using SharedUseCase.DTOs.Purchase;
using SharedUseCase.InterfacesUC.Purchase;
using SharedUseCase.InterfacesUC;
using SharedUseCase.DTOs.Reports;

namespace IntegrationModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IGetAll<ReportDto> _getAll;
        private readonly IAdd<ReportDto> _add;

        public ReportsController(IGetAll<ReportDto> getAll, IAdd<ReportDto> add)
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
                    return BadRequest("Report data is null.");
                }

                // Validaciones de negocio mínimas
                if (string.IsNullOrWhiteSpace(report.type))
                    return BadRequest("El tipo de reporte es obligatorio.");
                if (report.TotalLines < 0 || report.ProcessedLines < 0)
                    return BadRequest("Los valores de líneas deben ser mayores o iguales a 0.");

                int addedReportId = _add.Execute(report);

                return CreatedAtAction(nameof(GetAll), new { id = addedReportId }, report);
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
