using GestorOficios.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace GestorOficios.Models.Entities
{
    public class Acciones_Sobre_Archivos
    {
        public int Id_Accion { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        //Una accion puede estar en muchas solicitudes

        public ICollection<Solicitudes>? Solicitudes { get; set; } = new List<Solicitudes>();
        public ICollection<Accesos_Temporales> AccesosTemporales { get; set; } = new List<Accesos_Temporales>();

    }
}
