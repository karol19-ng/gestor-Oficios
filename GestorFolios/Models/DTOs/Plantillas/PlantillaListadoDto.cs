namespace GestorOficios.Models.DTOs.Plantillas
{
    public class PlantillaListadoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

        public string Departamento { get; set; } = null!;

        public string CodigoDepartamento { get; set; } = null!;

        public DateOnly FechaCreacion { get; set; }



    }
}
