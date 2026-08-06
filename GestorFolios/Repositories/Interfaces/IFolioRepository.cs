using GestorOficios.Models.DTOs.Oficios;

namespace GestorOficios.Repositories.Interfaces
{
    /// <summary>
    /// Acceso a datos de Control_Oficios. Toda la lógica de negocio
    /// (correlativo atómico, filtrado por permisos, etc.) vive en los
    /// Stored Procedures — este repositorio solo los invoca vía Dapper.
    /// </summary>
    public interface IFolioRepository
    {
        Task<SiguienteNumeroDto> ObtenerSiguienteNumeroAsync(int codigoDepartamento, int anio);

        Task<int> CrearAsync(OficioCrearParamsDto parametros);

        Task<OficioDetalleRawDto?> ObtenerPorIdAsync(int idOficio, int codigoDepartamentoUsuario);

        Task<IEnumerable<OficioRawDto>> ListarPorDepartamentoAsync(int codigoDepartamento, int pagina = 1, int registrosPorPagina = 20);

        Task<IEnumerable<OficioRawDto>> BuscarAsync(int codigoDepartamento, string busqueda);

        Task AnularAsync(int idOficio, int anuladoPor);
    }
}
