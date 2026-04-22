using ApiLogin.Models.General;

namespace ApiLogin.Infraestructure.Data.Repositories.Bus
{
    public interface IParadaAutorizadaRepository
    {
        Task<IEnumerable<ParadasAutorizadas>> ObtenerParadasAsync(int? idParada);
    }
}
