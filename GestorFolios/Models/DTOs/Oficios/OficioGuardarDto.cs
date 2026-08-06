namespace GestorOficios.Models.DTOs.Oficios
{
    /// <summary>Datos del formulario al confirmar "Guardar Oficio".</summary>
    public class OficioGuardarDto
    {
        public int IdEnProgreso { get; set; }
        public int Codigo_Departamento { get; set; }
        public DateTime Fecha { get; set; }
        public string Dirigido_A { get; set; } = string.Empty;
        public string Copia_A { get; set; } = string.Empty;
        public string? Referencia { get; set; }
        public int Elaborado_Por { get; set; }
        public int? Firmado_Por { get; set; }
        public int Id_Archivo { get; set; }
    }
}
