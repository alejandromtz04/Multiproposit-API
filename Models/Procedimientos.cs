namespace ApiLogin.Models
{
    public class Procedure
    {
        public string NBase { get; set; }
        public string Servidor { get; set; }
        public string Procedimiento { get; set; }
        public List<Parametros> Parametros { get; set; }
        public Procedure() { }
        public Procedure(string nbase)
        {
            NBase = nbase;
        }
    }

    public class Parametros
    {
        public string nombre { get; set; }
        public string valor { get; set; }
        //public string tipo { get; set; }
    }
}
