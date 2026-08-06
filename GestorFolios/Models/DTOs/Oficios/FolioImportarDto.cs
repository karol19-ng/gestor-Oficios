public class FolioImportarDto
{
    public string Codigo { get; set; } = null!;

    public DateTime Fecha { get; set; }

    public string DirigidoA { get; set; } = null!;

    public string Asunto { get; set; } = null!;

    public IFormFile Archivo { get; set; } = null!;
}
