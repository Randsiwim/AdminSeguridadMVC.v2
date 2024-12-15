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

        // Acci�n para el Dashboard
        public IActionResult Dashboard()
        {
            ViewBag.Bienvenida = "�Bienvenido al Dashboard del Sistema de Seguridad!";
            return View();
        }

        // Acci�n para la p�gina principal
        public IActionResult Index()
        {
            return View();
        }

        // Acci�n para la pol�tica de privacidad
        public IActionResult Privacy()
        {
            return View();
        }

        // Acci�n para manejar errores
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

