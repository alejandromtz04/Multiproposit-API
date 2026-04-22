using ApiLogin.Models.General;

namespace ApiLogin.Interfaces
{
    public interface IParadaAutorizadaService
    {
        Task<List<ParadasAutorizadas>> ObtenerParadasAutorizadas(int? id_parada_autorizada = null);
    }
}