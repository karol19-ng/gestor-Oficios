using Gestor0ficios.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Gestor_Oficios.Models.Entities
{
    public class Cargos
    {
        public int Id_Cargo { get; set; }

        [Required] //Campos obligatorios
        public string Nombre { get; set; } = string.Empty;

        //Inicializado
        public ICollection<Usuarios> Usuarios { get; set; } = new List<Usuarios>();
    }
}
