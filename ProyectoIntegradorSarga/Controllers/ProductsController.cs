using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoIntegradorSarga.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        public IActionResult Get()
        {
            return View();
        }
    }
}
