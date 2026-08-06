namespace GestorOficios.Models.DTOs.Plantillas
{
    public class PlantillaSubirDto
    {
        public string Nombre { get; set; } = null!;
        public string Departamento { get; set; } = null!;
        public string CodigoDepartamento { get; set; } = null!;
        public IFormFile Archivo { get; set; } = null!;
    }
}
