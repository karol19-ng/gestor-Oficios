using System.Data;
using Microsoft.Data.SqlClient;

namespace GestorOficios.Data.Connection
{
    public class SqlConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public SqlConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("No se encontró la cadena de conexión 'DefaultConnection' en appsettings.json");
        }

        public IDbConnection CreateConnection()
        {
            // Dapper abre la conexión automáticamente si está cerrada,
            // pero la dejamos abierta explícitamente para poder reutilizarla
            // dentro de transacciones cuando se necesite (ej: Solicitud_Aprobar).
            var connection = new SqlConnection(_connectionString);
            return connection;
        }
    }
}
