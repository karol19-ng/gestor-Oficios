using Gestor0ficios.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace GestorOficios.Models.Entities
{
    public class Departamentos
    {
        public int Codigo { get; set; }

        [Required]

        public string ID { get; set; } = string.Empty;

        [Required]
        public string Nombre { get; set; } = string.Empty;

        public byte[]? Logo { get; set; }


        //Muchos usuarios pueden pertenecer a un departamento
        public ICollection<Usuarios> Usuarios { get; set; } = new List<Usuarios>();
        public ICollection<Archivo> Archivos { get; set; } = new List<Archivo>();
        public ICollection<Plantillas_Departamento> Plantillas_Departamento { get; set; } = new List<Plantillas_Departamento>();
        public ICollection<Control_Oficios> Control_Oficios { get; set; } = new List<Control_Oficios>();
    }
}
