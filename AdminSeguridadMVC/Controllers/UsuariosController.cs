using AdminSeguridadMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AdminSeguridadMVC.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Listar usuarios
        public IActionResult Index()
        {
            var usuarios = _context.Usuarios.Include(u => u.Rol).ToList();
            return View(usuarios);
        }

        // Mostrar formulario de creación/edición
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return View(new Usuario());

            var usuario = _context.Usuarios.Find(id);
            if (usuario == null)
                return NotFound();

            return View(usuario);
        }

        // Guardar cambios
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                if (usuario.UsuarioID == 0)
                {
                    _context.Usuarios.Add(usuario);
                }
                else
                {
                    _context.Usuarios.Update(usuario);
                }
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // Eliminar usuario
        public IActionResult Delete(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null)
                return NotFound();

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
