using Gestor0ficios.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace GestorOficios.Models.Entities
{
    public class Oficios_En_Progreso
    {

        public int Id_EnProgreso { get; set; }

        [Required]
        public int Id_Usuario { get; set; }
        [Required]
        public int Codigo_Departamento { get; set; }

        [Required]
        public int Numero_Registro { get; set; }

        [Required]
        public string Codigo_Referencia { get; set; }= string.Empty;

        public DateOnly Fecha_Inicio { get; set; }

        [Required]
        public DateTime Fecha_Expiracion { get; set; }

        public string Estado { get; set; } = string.Empty;

        //Navegacion 

        public Usuarios? Usuario { get; set; } //Relacion con Usuarios
        public Departamentos? Departamento { get; set; } //Relacion con Departamentos
    }
}
