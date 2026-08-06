using GestorOficios.Models.DTOs.Oficios;
using GestorOficios.Models.DTOs.Reservas;
using GestorOficios.Repositories.Interfaces;
using GestorOficios.Services.Interfaces;

namespace GestorOficios.Services.Implementations
{
    /// <summary>
    /// Orquesta Folios + Reservas siguiendo el flujo documentado en la
    /// </summary>
    public class FolioService : IFolioService
    {
        private readonly IFolioRepository _folioRepository;
        private readonly IReservaRepository _reservaRepository;

        public FolioService(IFolioRepository folioRepository, IReservaRepository reservaRepository)
        {
            _folioRepository = folioRepository;
            _reservaRepository = reservaRepository;
        }

        public async Task<IniciarOficioResultDto> IniciarCreacionAsync(int codigoDepartamento, int idUsuario)
        {
            var anio = DateTime.Now.Year;

            // 1. sp_Oficio_ObtenerSiguienteNumero: calcula el número pero NO lo guarda todavía.
            var siguiente = await _folioRepository.ObtenerSiguienteNumeroAsync(codigoDepartamento, anio);

            // 2. sp_Reserva_Crear: lo reserva por 10 minutos para que nadie más lo tome
            //    mientras el usuario llena el formulario.
            var idEnProgreso = await _reservaRepository.CrearAsync(new ReservaCrearParamsDto
            {
                Id_Usuario = idUsuario,
                Codigo_Departamento = codigoDepartamento,
                Numero_Registro = siguiente.NumeroSiguiente,
                Codigo_Referencia = siguiente.CodigoReferencia,
                Minutos_Validez = 10
            });

            return new IniciarOficioResultDto
            {
                NumeroSiguiente = siguiente.NumeroSiguiente,
                CodigoReferencia = siguiente.CodigoReferencia,
                IdEnProgreso = idEnProgreso
            };
        }

        public async Task<int> GuardarOficioAsync(OficioGuardarDto datos)
        {
            // 3. sp_Oficio_Crear: inserta el oficio real. El número correlativo se
            //    vuelve a calcular de forma atómica dentro del propio SP (ROWLOCK/HOLDLOCK),
            //    así que aunque la reserva haya expirado, nunca se generan duplicados.
            var idOficio = await _folioRepository.CrearAsync(new OficioCrearParamsDto
            {
                Codigo_Departamento = datos.Codigo_Departamento,
                Fecha = datos.Fecha,
                Dirigido_A = datos.Dirigido_A,
                Copia_A = datos.Copia_A,
                Referencia = datos.Referencia,
                Elaborado_Por = datos.Elaborado_Por,
                Firmado_Por = datos.Firmado_Por,
                Id_Archivo = datos.Id_Archivo
            });

            // 4. La reserva ya cumplió su propósito (protegió el número mientras se
            //    llenaba el formulario) — se libera para no dejar basura en Oficios_En_Progreso.
            await _reservaRepository.LiberarAsync(datos.IdEnProgreso);

            return idOficio;
        }

        public Task CancelarCreacionAsync(int idEnProgreso)
            => _reservaRepository.LiberarAsync(idEnProgreso);

        public Task<OficioDetalleRawDto?> ObtenerDetalleAsync(int idOficio, int codigoDepartamentoUsuario)
            => _folioRepository.ObtenerPorIdAsync(idOficio, codigoDepartamentoUsuario);

        public Task<IEnumerable<OficioRawDto>> ListarPorDepartamentoAsync(int codigoDepartamento, int pagina = 1, int registrosPorPagina = 20)
            => _folioRepository.ListarPorDepartamentoAsync(codigoDepartamento, pagina, registrosPorPagina);

        public Task<IEnumerable<OficioRawDto>> BuscarAsync(int codigoDepartamento, string busqueda)
            => _folioRepository.BuscarAsync(codigoDepartamento, busqueda);

        public Task AnularAsync(int idOficio, int anuladoPor)
            => _folioRepository.AnularAsync(idOficio, anuladoPor);
    }
}
