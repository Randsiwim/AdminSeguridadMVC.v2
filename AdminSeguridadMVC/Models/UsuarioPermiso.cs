namespace AdminSeguridadMVC.Models
{
    public class UsuarioPermiso
    {
        public int UsuarioID { get; set; }
        public int PermisoID { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime FechaActualizacion { get; set; } = DateTime.Now;

        public Usuario Usuario { get; set; }
        public Permiso Permiso { get; set; }
    }

}
