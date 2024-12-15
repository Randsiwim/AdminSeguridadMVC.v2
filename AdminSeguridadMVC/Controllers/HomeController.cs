using System.Diagnostics;
using AdminSeguridadMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdminSeguridadMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Acción para el Dashboard
        public IActionResult Dashboard()
        {
            ViewBag.Bienvenida = "¡Bienvenido al Dashboard del Sistema de Seguridad!";
            return View();
        }

        // Acción para la página principal
        public IActionResult Index()
        {
            return View();
        }

        // Acción para la política de privacidad
        public IActionResult Privacy()
        {
            return View();
        }

        // Acción para manejar errores
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

