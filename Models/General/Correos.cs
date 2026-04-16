namespace ApiLogin.Models.General
{
    public class servidorCorreo
    {
        public string servidor { get; set; }
        public int puerto { get; set; }
        public string cuenta { get; set; }
        public string nombre { get; set; }
        public int pausa { get; set; }
        public string usuario { get; set; }
        public string clave { get; set; }
        public string email { get; set; }
        public string para { get; set; }
    }

    public class Destino
    {
        public string IdC { get; set; }
        public string Apara { get; set; }
        public string Email { get; set; }
        public string Tipo { get; set; }
        public string Empresa { get; set; }
    }
}
