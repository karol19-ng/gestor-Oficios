using System.ComponentModel.DataAnnotations;

namespace GestorOficios.Models.Entities
{
    public class Prioridad_Solicitudes
    {
        public int Id_Prioridad { get; set;}

        [Required]
        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }


        // Una prioridad puede estar en muchas solicitudes
        public ICollection<Solicitudes>? Solicitudes { get; set; } = new List<Solicitudes>();
    }
}
