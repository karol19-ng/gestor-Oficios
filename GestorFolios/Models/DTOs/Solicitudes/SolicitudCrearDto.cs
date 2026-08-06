namespace GestorOficios.Models.DTOs.Solicitudes
{
    public class SolicitudCrearDto
    {
        public int IdSolicitud { get; set; }
        public string NombreSolicitante { get; set; } = null!;
        public string DepartamentoProcedencia { get;set; } = null!;
        public string CodigoDepa {  get; set; } = null!;// codigo  del departameto a solicitar 
        public string CodigoOficio { get; set; } = null!;
        public string DueñoFolio { get; set; } = null!;
        public string Justificacion { get; set; } = null!;
        public string EstadoSolicitud { get; set; } = null!;


    }
}
