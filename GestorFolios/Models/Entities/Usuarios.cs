using Gestor_Oficios.Models.Entities;
using GestorOficios.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Gestor0ficios.Models.Entities
{
    public class Usuarios
    {
        //Reflejo Base de Datos 
        public int Id_Usuario { get; set; }

        [Required]
        public string Nombre_Completo { get; set; } = string.Empty;

        [Required]
        public int Codigo_Departamento { get; set; }

        [Required]
        public int Id_Cargo { get; set; }

        [Required]
        public int Id_Rol { get; set; }

        [Required]
        public string Usuario { get; set; } = string.Empty;

        [Required]
        public string Contraseña_Hash { get; set; } = string.Empty;
        public DateTime Fecha_Creacion { get; set; } = DateTime.Now;
        public bool? Estado { get; set; }

        //Un usuario puede tener muchos oficios en progreso

        public ICollection<Oficios_En_Progreso>? Oficios_En_Progreso { get; set; } = new List<Oficios_En_Progreso>(); //Relacion con Oficios_En_Progreso
        public ICollection<Archivo> Archivos { get; set; } = new List<Archivo>(); //Relacion con Archivos
        public ICollection<Oficios_En_Progreso>? OficiosEnProgreso { get; set; } = new List<Oficios_En_Progreso>(); //Relacion con Oficios_En_Progreso
        public ICollection<Solicitudes>? Solicitudes { get; set; } = new List<Solicitudes>(); //Relacion con Solicitudes
        public ICollection<Accesos_Temporales>? AccesosTemporales { get; set; } = new List<Accesos_Temporales>();
        public ICollection<Auditoria_Archivos>Auditorias { get; set; } = new List<Auditoria_Archivos>();

        //podria tener varios accesos temporales

        public ICollection<Accesos_Temporales>? Accesos_Temporales { get; set; } = new List<Accesos_Temporales>(); //Relacion con Accesos_Temporales
        //Navegacion
        public Cargos? Cargo { get; set; } //Relacion con Cargos
        public Roles? Rol { get; set; } //Relacion con Roles
        public Departamentos? Departamentos { get; set; } //Relacion con Departamentos


    }
}
