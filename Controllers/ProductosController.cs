using Microsoft.AspNetCore.Mvc;

namespace ProyectoFinalAPI.Controllers
{
    public class ProductosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
