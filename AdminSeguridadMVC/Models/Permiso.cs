namespace AdminSeguridadMVC.Models
{
    public class Permiso
    {
        public int PermisoID { get; set; }
        public string NombrePermiso { get; set; }

        public ICollection<PermisoMenu> PermisoMenus { get; set; }
        public ICollection<UsuarioPermiso> UsuarioPermisos { get; set; }
    }

}
