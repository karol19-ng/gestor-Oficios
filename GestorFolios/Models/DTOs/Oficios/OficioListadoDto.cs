namespace GestorOficios.Models.DTOs.Folios
{
    public class FolioListadoDto
    {
        public int Id { get; set; }

        public string Codigo { get; set; } = null!;

        public DateTime Fecha { get; set; }

        public string DirigidoA { get; set; } = null!;

        public string Asunto { get; set; } = null!;

        public string ElaboradoPor { get; set; } = null!;

        public bool TienePdf { get; set; }
    }
}
