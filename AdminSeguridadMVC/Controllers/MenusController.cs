using AdminSeguridadMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AdminSeguridadMVC.Controllers
{
    public class MenusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MenusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Acción para mostrar los menús
        public IActionResult Index()
        {
            // Obtener los menús desde la base de datos
            var menus = _context.Menus.ToList();
            return View(menus);
        }
    }
}

