using Gestor0ficios.Models.Entities;
using Gestor_Oficios.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Gestor_Oficios.Models.Entities
{
    public class Roles
    {
        //Reflejo BD

        public int Id_Rol { get; set; }
        
        [Required] //Campos obligatorios
        public string Nombre { get; set; } = string.Empty;  //Inicializado 

        //Rol puede pertenecer a  muchos usuarios 
        //coleccion 

        public ICollection<Usuarios> Usuarios { get; set; } = new List<Usuarios>();

    }
}
