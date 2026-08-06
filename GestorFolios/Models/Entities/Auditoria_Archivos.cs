using Gestor0ficios.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace GestorOficios.Models.Entities
{
    public class Auditoria_Archivos
    {
        public int Id_Auditoria { get; set; }

        [Required]
        public int Id_Usuario { get; set; }

        [Required]
        public int Id_Oficio { get; set; }

        [Required]
        public string Accion { get; set; } = string.Empty; // Coincide con nvarchar(50) en SQL (Anulacion, Previsualizacion, Descarga, etc.)

        public string Detalle { get; set; } = string.Empty;

        public DateTime Fecha { get; set; }

        //Navegacion
        public Usuarios Usuario { get; set; } = null!;
        public Control_Oficios Oficio { get; set; } = null!;


    }
}
