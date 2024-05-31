namespace ApiLogin.Models
{
    public class EDImensaje
    {
        public string Campo { get; set; }
        public string Texto { get; set; }
        public string Tipo { get; set; }
    }

    public class EDIfunciones
    {
        public string numrefmensaje { get; set; }
        public string tipomov { get; set; }
        public int numdocviaje { get; set; }
        public string fechahoraevento { get; set; }
        public string numviajebuque { get; set; }
        public string codlinea { get; set; }
        public string imobuque { get; set; }
        public string cargadescarga { get; set; }
        public string movcgadga { get; set; }
        public string contenedor { get; set; }
        public string tamconte { get; set; }
        public string impexp { get; set; }
        public string condconte { get; set; }
        public string tipodoc { get; set; }
        public string numtdoc { get; set; }
        public string fechahoradoc { get; set; }
        public string clasemov { get; set; }
        public string tipopeso { get; set; }
        public int peso { get; set; }
        public string archivo { get; set; }
        public string fecymd { get; set; }
        public string fechm { get; set; }
        public string numdam { get; set; }
        public string comdam { get; set; }
        public int segrem { get; set; }
        public string c_llegada { get; set; }
        public string puertocarga { get; set; }
    }
}
