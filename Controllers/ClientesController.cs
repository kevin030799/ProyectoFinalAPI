using Microsoft.AspNetCore.Mvc;

namespace ProyectoFinalAPI.Controllers
{
    public class ClientesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
