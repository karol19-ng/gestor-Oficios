using System.ComponentModel.DataAnnotations;

namespace GestorOficios.Models.Entities
{
    public class Permisos_Solicitud
    {

        public int Id_Permiso { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        //un permiso puede tener muchas solicitudes
        public ICollection<Solicitudes>? Solicitudes { get; set; } = new List<Solicitudes>();

    }
}
