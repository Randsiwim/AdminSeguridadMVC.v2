using AdminSeguridadMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AdminSeguridadMVC.Controllers
{
    public class PermisosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PermisosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Acción para listar permisos
        public IActionResult Index()
        {
            var permisos = _context.Permisos.ToList();
            return View(permisos);
        }

        // Acción para mostrar el formulario de crear o editar
        public IActionResult Edit(int? id)
        {
            if (id == null) return View(new Permiso());
            var permiso = _context.Permisos.Find(id);
            if (permiso == null) return NotFound();
            return View(permiso);
        }

        // Acción para guardar los cambios
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Permiso permiso)
        {
            if (ModelState.IsValid)
            {
                if (permiso.PermisoID == 0)
                {
                    _context.Permisos.Add(permiso);
                }
                else
                {
                    _context.Permisos.Update(permiso);
                }

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(permiso);
        }

        // Acción para eliminar permisos
        public IActionResult Delete(int id)
        {
            var permiso = _context.Permisos.Find(id);
            if (permiso == null) return NotFound();

            _context.Permisos.Remove(permiso);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}

