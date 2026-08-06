namespace GestorOficios.Models.DTOs.Oficios
{
    /// <summary>Fila cruda de sp_Oficio_ObtenerPorId (incluye datos de archivo y nombres de usuarios).</summary>
    public class OficioDetalleRawDto : OficioRawDto
    {
        public string? Ruta_Relativa { get; set; }
        public string? Ruta_Preview { get; set; }
        public string? Extension { get; set; }
        public string? Nombre_Elaborado { get; set; }
        public string? Nombre_Firmado { get; set; }
    }
}
