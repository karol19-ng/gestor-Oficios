namespace GestorOficios.Models.DTOs.Oficios
{
    /// <summary>
    /// Resultado de "abrir el formulario de Crear Oficio": el número
    /// que se le va a asignar y el Id de la reserva temporal que lo protege
    /// (para poder liberarla si el usuario cancela).
    /// </summary>
    public class IniciarOficioResultDto
    {
        public int NumeroSiguiente { get; set; }
        public string CodigoReferencia { get; set; } = string.Empty;
        public int IdEnProgreso { get; set; }
    }
}
