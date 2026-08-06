namespace GestorOficios.Models.DTOs.Oficios
{
    /// <summary>Resultado de sp_Oficio_ObtenerSiguienteNumero.</summary>
    public class SiguienteNumeroDto
    {
        public int NumeroSiguiente { get; set; }
        public string CodigoReferencia { get; set; } = string.Empty;
    }
}
