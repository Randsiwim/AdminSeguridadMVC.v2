namespace AdminSeguridadMVC.Models
{

    public class Log
    {
        public int ID { get; set; }
        public string Usuario { get; set; }
        public string Accion { get; set; }
        public DateTime? FechaHora { get; set; }
    }

    public class LogViewModel
    {
        public int ID { get; set; }
        public string Usuario { get; set; }
        public string Accion { get; set; }
        public DateTime? FechaHora { get; set; }
    }
}

