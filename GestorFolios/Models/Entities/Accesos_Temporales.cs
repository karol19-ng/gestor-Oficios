using Gestor0ficios.Models.Entities;
using GestorOficios.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace GestorOficios.Models.Entities
{
    public class Accesos_Temporales
    {
        public int Id_Acceso { get; set; }

        [Required]
        public int Id_Usuario { get; set; }

        [Required]
        public int Id_Oficio { get; set; }

        [Required]
        public int Id_Accion_Permitida { get; set; }

        [Required]
        public DateTime Fecha_Inicio { get; set; }

        [Required]

        public DateTime Fecha_Expiracion { get; set; }

        public bool Activo { get; set; } // Coincide con SQL: Activo bit
        //Navegacion
        public Usuarios? Usuarios { get; set; }
        public Solicitudes? Solicitudes { get; set; }
        public Control_Oficios Control_Oficios { get; set; } = null!;
        public Acciones_Sobre_Archivos Acciones_Sobre_Archivos { get; set; } = null!;

    }
}
