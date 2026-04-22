namespace ApiLogin.Models.General
{
    public class ParadasAutorizadas
    {
        public int id_parada_autorizada { get; set; }
        public string nombre_parada { get; set; }
        public string codigo_parada { get; set; }
        public string direccion_parada { get; set; }
        public bool activo { get; set; }
        public DateTime fecha_creacion { get; set; }
        public string usuario_creacion { get; set; }
        public DateTime? fecha_modificacion { get; set; }
        public string usuario_modificacion { get; set; }
        public string estado_parada { get; set; }
        public bool en_uso { get;set; }
    }
}