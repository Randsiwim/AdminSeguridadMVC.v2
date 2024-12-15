using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using AdminSeguridadMVC.Models;

namespace AdminSeguridadMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Acción para mostrar la vista de login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Acción para procesar el inicio de sesión
        [HttpPost]
        public async Task<IActionResult> Login(string usuario, string contrasena)
        {
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contrasena))
            {
                ViewBag.Error = "Por favor ingrese su usuario y contraseña.";
                return View();
            }

            // Validar las credenciales
            var user = _context.Usuarios.FirstOrDefault(u => u.NombreUsuario == usuario && u.Clave == contrasena);
            if (user == null)
            {
                ViewBag.Error = "Usuario o contraseña incorrectos.";
                return View();
            }

            // Crear los claims (datos de usuario)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.NombreUsuario),
                new Claim("RolID", user.RolID.ToString()) // Guardar el RolID
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            // Iniciar la autenticación
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            // Redirigir según el rol
            switch (user.RolID)
            {
                case 1: // Administrador
                    return RedirectToAction("Index", "Menus");
                case 2: // Usuario
                    return RedirectToAction("Index", "Usuarios");
                case 3: // Supervisor
                    return RedirectToAction("Index", "Menus");
                default:
                    ViewBag.Error = "Rol no reconocido.";
                    return View();
            }
        }

        // Acción para cerrar sesión
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
