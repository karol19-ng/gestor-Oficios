using GestorOficios.Models.DTOs.Oficios;

namespace GestorOficios.Services.Interfaces
{
    public interface IFolioService
    {
        /// <summary>
        /// Paso 1 del flujo: calcula el siguiente número correlativo y lo
        /// reserva por unos minutos para que nadie más lo use mientras
        /// el usuario llena el formulario. (sp_Oficio_ObtenerSiguienteNumero + sp_Reserva_Crear)
        /// </summary>
        Task<IniciarOficioResultDto> IniciarCreacionAsync(int codigoDepartamento, int idUsuario);

        /// <summary>
        /// Paso 2: el usuario confirma y guarda. Crea el oficio definitivo
        /// y libera la reserva temporal. (sp_Oficio_Crear + sp_Reserva_Liberar)
        /// </summary>
        Task<int> GuardarOficioAsync(OficioGuardarDto datos);

        /// <summary>El usuario cierra el formulario sin guardar: libera el número reservado.</summary>
        Task CancelarCreacionAsync(int idEnProgreso);

        Task<OficioDetalleRawDto?> ObtenerDetalleAsync(int idOficio, int codigoDepartamentoUsuario);

        Task<IEnumerable<OficioRawDto>> ListarPorDepartamentoAsync(int codigoDepartamento, int pagina = 1, int registrosPorPagina = 20);

        Task<IEnumerable<OficioRawDto>> BuscarAsync(int codigoDepartamento, string busqueda);

        Task AnularAsync(int idOficio, int anuladoPor);
    }
}
