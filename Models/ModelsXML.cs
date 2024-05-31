namespace ApiLogin.Models
{
    public class ServicioXml
    {
        public string registro { get; set; }
        public string codigo { get; set; }
        public List<iddetas> lstddeta { get; set; }
    }

    public class iddetas
    {
        public string numero { get; set; }
        public string tipo { get; set; }
    }
}
