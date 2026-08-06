namespace GestorOficios.Models.DTOs.Archivos
{
    public class ArchivoSubirDto
    {
        public IFormFile Archivo { get; set; } = null!;

        public string TipoContenido { get; set; } = null!;

        public int IdOficio { get; set; }

        public string  NombreArchivo { get; set; } = null!;

    }
}
