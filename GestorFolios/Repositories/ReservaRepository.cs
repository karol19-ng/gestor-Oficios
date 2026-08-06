using System.Data;
using Dapper;
using GestorOficios.Data.Connection;
using GestorOficios.Models.DTOs.Reservas;
using GestorOficios.Repositories.Interfaces;

namespace GestorOficios.Repositories
{
    public class ReservaRepository : IReservaRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public ReservaRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        // sp_Reserva_Crear
        public async Task<int> CrearAsync(ReservaCrearParamsDto p)
        {
            using var connection = _connectionFactory.CreateConnection();

            var parametros = new DynamicParameters();
            parametros.Add("@Id_Usuario", p.Id_Usuario);
            parametros.Add("@Codigo_Departamento", p.Codigo_Departamento);
            parametros.Add("@Numero_Registro", p.Numero_Registro);
            parametros.Add("@Codigo_Referencia", p.Codigo_Referencia);
            parametros.Add("@Minutos_Validez", p.Minutos_Validez);
            parametros.Add("@Id_EnProgreso", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await connection.ExecuteAsync(
                "sp_Reserva_Crear",
                parametros,
                commandType: CommandType.StoredProcedure);

            return parametros.Get<int>("@Id_EnProgreso");
        }

        // sp_Reserva_Liberar
        public async Task LiberarAsync(int idEnProgreso)
        {
            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(
                "sp_Reserva_Liberar",
                new { Id_EnProgreso = idEnProgreso },
                commandType: CommandType.StoredProcedure);
        }

        // sp_Reserva_LimpiarVencidas (usada por el job de Hangfire)
        public async Task<int> LimpiarVencidasAsync()
        {
            using var connection = _connectionFactory.CreateConnection();

            var resultado = await connection.QueryFirstOrDefaultAsync<int>(
                "sp_Reserva_LimpiarVencidas",
                commandType: CommandType.StoredProcedure);

            return resultado;
        }

        // sp_Reserva_CambiarNumero
        public async Task<int> CambiarNumeroAsync(int idEnProgresoActual, int nuevoNumero, string nuevoCodigoReferencia)
        {
            using var connection = _connectionFactory.CreateConnection();

            var parametros = new
            {
                Id_EnProgreso_Actual = idEnProgresoActual,
                Nuevo_Numero = nuevoNumero,
                Nuevo_Codigo_Referencia = nuevoCodigoReferencia
            };

            var resultado = await connection.QueryFirstOrDefaultAsync<int>(
                "sp_Reserva_CambiarNumero",
                parametros,
                commandType: CommandType.StoredProcedure);

            return resultado;
        }
    }
}
