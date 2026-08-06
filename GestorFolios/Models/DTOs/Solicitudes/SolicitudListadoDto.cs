namespace GestorOficios.Models.DTOs.Solicitudes
{
    public class SolicitudListadoDto
    {
        //Solicitudes tanto propias (solicitudes realizadas) como  las que han llegado al propietario (solicitudes rechazadas aprobadas o pendientes )

        public int IdSolicitud { get; set; }
        public string CodigoDepartamento { get; set; } = null!;//departamento solicitante 
        public string Solicitante { get; set; } = null!;
        public string NombreDepa { get; set; } = null!;
        public DateOnly FechaDeSolicitud { get; set; }
        public string PermisoSolicitado { get; set; } = null!;
        public string EstadoSolicitud { get; set; } = null!;

        


    }
}
