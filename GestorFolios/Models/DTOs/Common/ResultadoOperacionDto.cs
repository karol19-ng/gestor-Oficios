namespace GestorOficios.Models.DTOs.Common
{
    public class ResultadoOperacionDto
    {

        public bool Exitoso { get; set; }

        public string Mensaje { get; set; } = string.Empty;

        public List<string> Errores { get; set; } = new();


    }
}
