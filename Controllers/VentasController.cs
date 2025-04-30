using Microsoft.AspNetCore.Mvc;

namespace ProyectoFinalAPI.Controllers
{
    public class VentasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
