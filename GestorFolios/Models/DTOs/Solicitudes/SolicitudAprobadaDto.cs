namespace GestorOficios.Models.DTOs.Solicitudes
{
    public class SolicitudAprobadaDto
    {
        public int IdSolicitud { get; set; } //Solicitud que se aprobó propia del usuario no todas
        public int IdFolio { get; set; } //El folio que se generó al aprobar la solicitud
        public string EstadoSolicitud { get; set; } = null!;
        public string Aprobador { get; set; } = null!;
        public string? Observaciones { get; set; } = null!;
        public DateOnly FechaAprobacion { get; set; }
        public DateTime? FechaExpiracion { get; set; }


    }
}
