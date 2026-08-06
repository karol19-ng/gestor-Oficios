namespace GestorOficios.Models.DTOs.Archivos
{
    public class ArchivoDescargarDto
    {
        public string NombreArchivo { get; set; } = null!;

        public string RutaArchivo { get; set; } = null!;

        public string TipoContenido { get; set; } = null!;

        public long Tamaño { get; set; }

    }
}
