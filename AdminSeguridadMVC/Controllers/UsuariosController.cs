using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdminSeguridadMVC.Models;
using System.IO;
using System.Linq;

public class UsuariosController : Controller
{
    private readonly ApplicationDbContext _context;

    public UsuariosController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Mostrar formulario de edición o creación
    public IActionResult Edit(int? id)
    {
        if (id == null)
            return View(new Usuario()); // Nuevo usuario

        var usuario = _context.Usuarios.Find(id);
        if (usuario == null)
            return NotFound();

        return View(usuario);
    }

    // POST: Guardar cambios del usuario
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Usuario usuario, IFormFile fotoFile)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Los datos ingresados no son válidos.");
            return View(usuario);
        }

        try
        {
            // Manejo de la foto si se sube una nueva
            if (fotoFile != null && fotoFile.Length > 0)
            {
                var extension = Path.GetExtension(fotoFile.FileName).ToLower();
                if (extension != ".png" && extension != ".jpg" && extension != ".jpeg")
                {
                    ModelState.AddModelError("", "Solo se permiten archivos en formato PNG o JPG.");
                    return View(usuario);
                }

                using (var ms = new MemoryStream())
                {
                    fotoFile.CopyTo(ms);
                    usuario.Foto = ms.ToArray();
                }
            }
            else if (usuario.UsuarioID != 0)
            {
                // Mantener la foto existente si no se sube una nueva
                var usuarioExistente = _context.Usuarios
                    .AsNoTracking()
                    .FirstOrDefault(u => u.UsuarioID == usuario.UsuarioID);

                if (usuarioExistente != null)
                    usuario.Foto = usuarioExistente.Foto;
            }

            // Diferenciar entre Crear y Editar
            if (usuario.UsuarioID == 0) // Nuevo usuario
            {
                _context.Usuarios.Add(usuario);
                TempData["Success"] = "Usuario agregado correctamente.";
            }
            else // Editar usuario existente
            {
                _context.Entry(usuario).State = EntityState.Modified;
                TempData["Success"] = "Usuario editado correctamente.";
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException ex)
        {
            ModelState.AddModelError("", $"Error al guardar los datos: {ex.Message}");
            return View(usuario);
        }
    }


    // GET: Mostrar la foto del usuario
    public IActionResult ObtenerFoto(int id)
    {
        var usuario = _context.Usuarios.Find(id);
        if (usuario?.Foto != null)
            return File(usuario.Foto, "image/png");

        return File("~/images/default.png", "image/png"); // Imagen por defecto
    }

    // POST: Eliminar un usuario
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        try
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null)
                return NotFound();

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
            TempData["Success"] = "Usuario eliminado correctamente.";
        }
        catch
        {
            TempData["Error"] = "Error al eliminar el usuario.";
        }

        return RedirectToAction(nameof(Index));
    }

    // GET: Listar los usuarios
    public IActionResult Index()
    {
        var usuarios = _context.Usuarios.ToList();
        return View(usuarios);
    }
}
