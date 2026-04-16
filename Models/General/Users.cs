namespace ApiLogin.Models.General
{
    public class Users
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }

    public class Activo
    {
        public bool Estado { get; set; }
    }

    public class Usuario
    {
        public string Expediente { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Alfa { get; set; }
        public string Ccostos { get; set; }
        public string Nombrecc { get; set; }

        public string Ccargo { get; set; }
        public string Dcargo { get; set; }
        //Comentado originalmente para evitar sobrecarga de datos
        //public byte[] Foto { get; set; } 
        public string Sexo { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }

    }
}
