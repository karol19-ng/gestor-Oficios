using GestorOficios.Models.DTOs.Reservas;

namespace GestorOficios.Repositories.Interfaces
{
    /// <summary>Acceso a Oficios_En_Progreso (anti-duplicados) vía los 4 SPs de reserva.</summary>
    public interface IReservaRepository
    {
        Task<int> CrearAsync(ReservaCrearParamsDto parametros);
        Task LiberarAsync(int idEnProgreso);
        Task<int> LimpiarVencidasAsync();
        Task<int> CambiarNumeroAsync(int idEnProgresoActual, int nuevoNumero, string nuevoCodigoReferencia);
    }
}
