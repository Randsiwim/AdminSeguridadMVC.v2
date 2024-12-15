namespace AdminSeguridadMVC.Models
{
    public class PermisoMenu
    {
        public int MenuID { get; set; }
        public int PermisoID { get; set; }
        public bool PermisoLectura { get; set; }
        public bool PermisoEscritura { get; set; }
        public bool PermisoModificacion { get; set; }
        public bool PermisoEliminacion { get; set; }

        public Menu Menu { get; set; }
        public Permiso Permiso { get; set; }
    }

}
