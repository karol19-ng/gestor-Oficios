using Gestor0ficios.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace GestorOficios.Models.Entities
{
    public class Control_Oficios
    {
        public int Id_Oficio { get; set; }

        [Required]
        public int Numero_Registro { get; set; }

        [Required]
        public string Codigo_De_Referencia { get; set; } = string.Empty;

        [Required]
        public int Codigo_Departamento { get; set; }

        [Required]
        public DateOnly Fecha { get; set; }

        [Required]
        public string Dirigido_A { get; set; } = string.Empty;

        [Required]
        public string Copia_A { get; set; } = string.Empty;

        public string?Referencia { get; set; }

        [Required]
        public int Elaborado_Por { get; set; }

        public int? Firmado_Por { get; set; }

        [Required]
        public int Id_Archivo { get; set; }

        public string? Estado_Ofico { get; set; }

        public DateTime Fecha_Creacion { get; set; }

        // Navegación
        public Departamentos Departamento { get; set; } = null!;
        public Usuarios UsuarioElaborador { get; set; } = null!;
        public Usuarios? UsuarioFirmante { get; set; }
        public Archivo Archivo { get; set; } = null!;

        public ICollection<Solicitudes> Solicitudes { get; set; } = new List<Solicitudes>();
        public ICollection<Accesos_Temporales> AccesosTemporales { get; set; } = new List<Accesos_Temporales>();
        public ICollection<Auditoria_Archivos> Auditorias { get; set; } = new List<Auditoria_Archivos>();
    }


}

