using System.Data.SqlClient;
using ApiLogin.Models.General;
using ApiLogin.Interfaces;
using ApiLogin.Infraestructure.Data;
using ApiLogin.Infraestructure.Data.Repositories.Bus;

namespace ApiLogin.Services
{
    // 1. Heredar de la interfaz
    public class ParadasAutorizadasService : IParadaAutorizadaService
    {
        private readonly IParadaAutorizadaRepository _repository;

        public ParadasAutorizadasService(IParadaAutorizadaRepository repository)
        {
            _repository = repository;
        }

        // Usando repositorios evitamos la forma antigua que es colocar un data reader y estar leyendo los datos desde aqui, ya que al hacer esto se base en el modelo/entidad y se compara directamente con la base de datos
        public async Task<List<ParadasAutorizadas>> ObtenerParadasAutorizadas(int? id_parada_autorizada = null)
        {
            try
            {
                var paradas = await _repository.ObtenerParadasAsync(id_parada_autorizada);
                return paradas.ToList();
            }
            catch (SqlException ex)
            {
                // Manejo específico para errores de SQL
                throw new Exception("Error al consultar la base de datos: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                // Manejo general de excepciones
                throw new Exception("Ocurrió un error inesperado: " + ex.Message, ex);
            }
        }
    }
}