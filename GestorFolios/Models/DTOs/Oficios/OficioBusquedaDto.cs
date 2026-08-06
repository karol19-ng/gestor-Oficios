namespace GestorOficios.Models.DTOs.Folios
{
    public class FolioBusquedaDto
    {
        public int Id { get; set; }

        public string Codigo { get; set; } = null!;

        public string Asunto { get; set; } = null!;

        public string DirigidoA { get; set; } = null!;

        public DateTime Fecha { get; set; }

        public string Coincidencia { get; set; } = null!;
    }
}