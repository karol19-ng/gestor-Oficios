namespace GestorOficios.Models.DTOs.Accesos
{
    public class AccesoVigenteDto
    {
        //Varias tablas unificadas para dar vision  a los usuarios
        public string CodigoOficio { get; set; } = string.Empty;

        public string Asunto { get; set; } = string.Empty;

        public string DepartamentoOriginal { get; set; } = string.Empty;

        public string AccionPermitida { get; set; } = string.Empty;

        public DateTime FechaExpiracion { get; set; }

        public int HorasRestantes { get; set; }//Codigio

    }
}
