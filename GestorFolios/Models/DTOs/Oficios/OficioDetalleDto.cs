namespace GestorOficios.Models.DTOs.Oficios
{
    public class OficioDetalleDto
    {
        public int Id { get; set; }

        public string Codigo { get; set; } = null!;

        public DateTime Fecha { get; set; }

        public string DirigidoA { get; set; } = null!;

        public string CopiaA { get; set; } = null!;

        public string Referencia { get; set; } = null!;

        public string Asunto { get; set; } = null!;

        public string ElaboradoPor { get; set; } = null!;

        public string FirmadoPor { get; set; } = null!;

        public string Ruta { get; set; } = null!;
    }
}
