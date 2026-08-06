using System.Data;
using Dapper;
using GestorOficios.Data.Connection;
using GestorOficios.Models.DTOs.Oficios;
using GestorOficios.Repositories.Interfaces;

namespace GestorOficios.Repositories
{
    public class FolioRepository : IFolioRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public FolioRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        // sp_Oficio_ObtenerSiguienteNumero
        public async Task<SiguienteNumeroDto> ObtenerSiguienteNumeroAsync(int codigoDepartamento, int anio)
        {
            using var connection = _connectionFactory.CreateConnection();

            var parametros = new DynamicParameters();
            parametros.Add("@Codigo_Departamento", codigoDepartamento);
            parametros.Add("@Año", anio);
            parametros.Add("@Numero_Siguiente", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@Codigo_Referencia", dbType: DbType.String, size: 50, direction: ParameterDirection.Output);

            await connection.ExecuteAsync(
                "sp_Oficio_ObtenerSiguienteNumero",
                parametros,
                commandType: CommandType.StoredProcedure);

            return new SiguienteNumeroDto
            {
                NumeroSiguiente = parametros.Get<int>("@Numero_Siguiente"),
                CodigoReferencia = parametros.Get<string>("@Codigo_Referencia")
            };
        }

        // sp_Oficio_Crear
        public async Task<int> CrearAsync(OficioCrearParamsDto p)
        {
            using var connection = _connectionFactory.CreateConnection();

            var parametros = new DynamicParameters();
            parametros.Add("@Codigo_Departamento", p.Codigo_Departamento);
            parametros.Add("@Fecha", p.Fecha);
            parametros.Add("@Dirigido_A", p.Dirigido_A);
            parametros.Add("@Copia_A", p.Copia_A);
            parametros.Add("@Referencia", p.Referencia);
            parametros.Add("@Elaborado_Por", p.Elaborado_Por);
            parametros.Add("@Firmado_Por", p.Firmado_Por);
            parametros.Add("@Id_Archivo", p.Id_Archivo);
            parametros.Add("@Id_Oficio", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await connection.ExecuteAsync(
                "sp_Oficio_Crear",
                parametros,
                commandType: CommandType.StoredProcedure);

            return parametros.Get<int>("@Id_Oficio");
        }

        // sp_Oficio_ObtenerPorId
        public async Task<OficioDetalleRawDto?> ObtenerPorIdAsync(int idOficio, int codigoDepartamentoUsuario)
        {
            using var connection = _connectionFactory.CreateConnection();

            var parametros = new { Id_Oficio = idOficio, Codigo_Departamento_Usuario = codigoDepartamentoUsuario };

            var resultado = await connection.QueryFirstOrDefaultAsync<OficioDetalleRawDto>(
                "sp_Oficio_ObtenerPorId",
                parametros,
                commandType: CommandType.StoredProcedure);

            return resultado;
        }

        // sp_Oficio_ListarPorDepartamento
        public async Task<IEnumerable<OficioRawDto>> ListarPorDepartamentoAsync(int codigoDepartamento, int pagina = 1, int registrosPorPagina = 20)
        {
            using var connection = _connectionFactory.CreateConnection();

            var parametros = new
            {
                Codigo_Departamento = codigoDepartamento,
                Pagina = pagina,
                RegistrosPorPagina = registrosPorPagina
            };

            return await connection.QueryAsync<OficioRawDto>(
                "sp_Oficio_ListarPorDepartamento",
                parametros,
                commandType: CommandType.StoredProcedure);
        }

        // sp_Oficio_Buscar
        public async Task<IEnumerable<OficioRawDto>> BuscarAsync(int codigoDepartamento, string busqueda)
        {
            using var connection = _connectionFactory.CreateConnection();

            var parametros = new { Codigo_Departamento = codigoDepartamento, Busqueda = busqueda };

            return await connection.QueryAsync<OficioRawDto>(
                "sp_Oficio_Buscar",
                parametros,
                commandType: CommandType.StoredProcedure);
        }

        // sp_Oficio_Anular
        public async Task AnularAsync(int idOficio, int anuladoPor)
        {
            using var connection = _connectionFactory.CreateConnection();

            var parametros = new { Id_Oficio = idOficio, Anulado_Por = anuladoPor };

            await connection.ExecuteAsync(
                "sp_Oficio_Anular",
                parametros,
                commandType: CommandType.StoredProcedure);
        }
    }
}
