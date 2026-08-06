using Gestor_Oficios.Models.Entities;
using Gestor0ficios.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace GestorOficios.Models.Entities
{
    public class Solicitudes
    {

        public int Id_Solicitud { get; set; }

        public int Id_Solicitante { get; set; }

        [Required]
        public int Id_Departamento { get; set; }

        [Required]
        public int Id_Oficio_Solicitado { get; set; }

        [Required]
        public int Id_Accion_Solicitada { get; set; }

        [Required]
        public int Id_Prioridad { get; set; }

        [Required]
        public int Id_Estado { get; set; }

        [Required]
        public string Justificacion { get; set; } = string.Empty;

        public DateTime Fecha_Solicitud { get; set; }

        public DateTime Fecha_Respuesta { get; set; }

        public DateTime Fecha_Expiracion { get; set; }

        public int Id_Aprobado_Por { get; set; }

        public int Id_Permiso_Otorgado { get; set; }

        public string ? Observacion_Respuesta { get; set; }=string.Empty;


        //navegacion 
        public Usuarios? Solicitante { get; set; }
        public Control_Oficios? Oficio_Solicitado { get; set; }

        public  Estados_Solicitudes? Estado { get; set; }
        public Permisos_Solicitud? Permiso { get; set; }

        public Prioridad_Solicitudes? Prioridad { get; set; }

        public Acciones_Sobre_Archivos? Accion { get; set; }

        public ICollection<Accesos_Temporales> AccesosTemporales { get; set; } = new List<Accesos_Temporales>();
    }
}
