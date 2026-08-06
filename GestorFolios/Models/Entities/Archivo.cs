using Gestor0ficios.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace GestorOficios.Models.Entities
{
    public class Archivo
    {
        public int Id_Archivo { get; set; }

        [Required]
        public string Nombre_Original { get; set; } = string.Empty;

        [Required]
        public string Nombre_Almacenado { get; set; } = string.Empty;

        [Required]
        public string Ruta_Relativa { get; set; } = string.Empty;

        [Required]
        public string Extension { get; set; } = string.Empty;

        [Required]
        public string Tipo_Contenido { get; set; } = string.Empty;

        [Required]
        public int Codigo_Departamento { get; set; } 

        [Required]
        public long Tamaño_Bytes { get; set; }

        [Required]
        public string Hash_SHA256 { get; set; } = string.Empty;

       
        public bool Tiene_Preview { get; set; }


        public string? Ruta_Preview { get; set; }

        [Required]
        public int Subido_Por { get; set; }

        public DateTime Fecha_Subida { get; set; }

        public bool? Estado { get; set; }

        //Navegacion
        public Departamentos? Departamentos { get; set; }
        public Usuarios Usuarios { get; set; } = null!;
        public Control_Oficios? Control_Oficios { get; set; }
        public Plantillas_Departamento? Plantillas_Departamento { get; set; }


    }
}
