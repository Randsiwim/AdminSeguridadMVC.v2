namespace AdminSeguridadMVC.Models
{
    public class Menu
    {
        public int MenuID { get; set; }
        public string NombreMenu { get; set; }
        public string URL { get; set; }

        public ICollection<PermisoMenu> PermisoMenus { get; set; }
    }

}
