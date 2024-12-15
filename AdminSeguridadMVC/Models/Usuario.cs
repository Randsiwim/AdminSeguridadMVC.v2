namespace AdminSeguridadMVC.Models
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public string NombreUsuario { get; set; }
        public string Email { get; set; }
        public string NombreCompleto { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Clave { get; set; }
        public int RolID { get; set; }

        public Rol Rol { get; set; } // Relación con Rol

        // Propiedad de navegación para la relación muchos a muchos con Permisos
        public ICollection<UsuarioPermiso> UsuarioPermisos { get; set; }
    }

}

