using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using AdminSeguridadMVC.Models;
using Microsoft.EntityFrameworkCore;

public class UsuariosController : Controller
{
    private readonly ApplicationDbContext _context;

    public UsuariosController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Acción para listar usuarios en la vista Index
    public IActionResult Index()
    {
        var usuarios = _context.Usuarios.ToList(); // Cargar todos los usuarios
        return View(usuarios);
    }

    // Acción para mostrar el formulario de usuario (Nuevo o Editar)
    public IActionResult Edit(int? id)
    {
        if (id == null)
            return View(new Usuario()); // Nuevo usuario

        var usuario = _context.Usuarios.Find(id);
        if (usuario == null)
            return NotFound();

        return View(usuario); // Editar usuario existente
    }

    // Acción para guardar usuario con foto
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Usuario usuario, IFormFile fotoFile)
    {
        if (ModelState.IsValid)
        {
            // Validar tamaño del archivo
            if (fotoFile != null && fotoFile.Length > 5 * 1024 * 1024) // 5 MB límite
            {
                ModelState.AddModelError("", "El archivo es demasiado grande. Máximo permitido: 5 MB.");
                return View(usuario);
            }

            // Procesar la foto y convertirla en byte[]
            if (fotoFile != null && fotoFile.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    fotoFile.CopyTo(ms);
                    usuario.Foto = ms.ToArray();
                }
            }
            else
            {
                // Mantener la foto anterior si no se sube una nueva
                var usuarioExistente = _context.Usuarios
                    .AsNoTracking()
                    .FirstOrDefault(u => u.UsuarioID == usuario.UsuarioID);

                if (usuarioExistente != null)
                    usuario.Foto = usuarioExistente.Foto;
            }

            // Guardar en la base de datos
            if (usuario.UsuarioID == 0)
                _context.Usuarios.Add(usuario);
            else
                _context.Usuarios.Update(usuario);

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        return View(usuario);
    }


    // Acción para eliminar un usuario
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        var usuario = _context.Usuarios.Find(id);
        if (usuario != null)
        {
            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
        }

        return RedirectToAction(nameof(Index));
    }

    // Acción para obtener la foto de un usuario
    public IActionResult ObtenerFoto(int id)
    {
        var usuario = _context.Usuarios.Find(id);
        if (usuario?.Foto != null)
        {
            return File(usuario.Foto, "image/png"); // Devuelve la imagen en formato PNG
        }

        // Imagen por defecto si el usuario no tiene foto
        return File("~/images/default.png", "image/png");
    }

}
