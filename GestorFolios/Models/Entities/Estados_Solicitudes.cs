using GestorOficios.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace GestorOficios.Models.Entities
{
    public class Estados_Solicitudes
    {
        public EstadoSolicitudEnum Id_Estado { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion {get;set;}

        //Un estado puede estar en muchas solicitudes

        public ICollection<Solicitudes>? Solicitudes { get; set; }= new List<Solicitudes>();
    }
}
