using AdminSeguridadMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AdminSeguridadMVC.Controllers
{
    public class RolesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Listar roles
        public IActionResult Index()
        {
            var roles = _context.Roles.ToList();
            return View(roles);
        }

        // Mostrar formulario de creación/edición
        public IActionResult Edit(int? id)
        {
            if (id == null) return View(new Rol());
            var rol = _context.Roles.Find(id);
            if (rol == null) return NotFound();
            return View(rol);
        }

        // Guardar cambios
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Rol rol)
        {
            if (ModelState.IsValid)
            {
                if (rol.RolID == 0)
                {
                    _context.Roles.Add(rol);
                }
                else
                {
                    _context.Roles.Update(rol);
                }
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(rol);
        }

        // Eliminar rol
        public IActionResult Delete(int id)
        {
            var rol = _context.Roles.Find(id);
            if (rol == null) return NotFound();

            _context.Roles.Remove(rol);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
