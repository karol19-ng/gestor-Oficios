using System.ComponentModel.DataAnnotations;

namespace GestorOficios.Models.Entities
{
    public class Plantillas_Departamento
    {
        public int Id_Plantilla { get; set; }

        [Required]
        public int Codigo_Departamento { get; set; }

        [Required]
        public int Id_Archivo { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public bool? Activa { get; set; }

        public DateTime Fecha_Actualizacion { get; set; }

        // Navigation properties
        public Departamentos Departamentos { get; set; } = null!;
            public Archivo Archivo { get; set; } = null!;

    }
}
