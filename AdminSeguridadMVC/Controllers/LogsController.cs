
using AdminSeguridadMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AdminSeguridadMVC.Controllers
{
    public class LogsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LogsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Acción para mostrar la vista de Logs
        public IActionResult Index()
        {
            // Obtener los registros de la base de datos
            var logs = _context.Logs.Select(log => new LogViewModel
            {
                ID = log.ID,
                Usuario = log.Usuario,
                Accion = log.Accion,
                FechaHora = log.FechaHora
            }).ToList();

            return View(logs);
        }
    }
}
