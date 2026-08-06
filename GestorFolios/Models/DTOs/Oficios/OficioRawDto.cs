namespace GestorOficios.Models.DTOs.Oficios
{
    /// <summary>
    /// Fila cruda tal como la devuelven los SPs de listado/búsqueda
    /// (SELECT o.*, a.Tiene_Preview). El Service la traduce a los DTOs
    /// de presentación (FolioListadoDto, etc.) que consume la UI.
    /// </summary>
    public class OficioRawDto
    {
        public int Id_Oficio { get; set; }
        public int Numero_Registro { get; set; }
        public string Codigo_De_Referencia { get; set; } = string.Empty;
        public int Codigo_Departamento { get; set; }
        public DateTime Fecha { get; set; }
        public string Dirigido_A { get; set; } = string.Empty;
        public string? Copia_A { get; set; }
        public string? Referencia { get; set; }
        public int Elaborado_Por { get; set; }
        public int? Firmado_Por { get; set; }
        public int Id_Archivo { get; set; }
        public string Estado_Oficio { get; set; } = string.Empty;
        public DateTime Fecha_Creacion { get; set; }
        public bool? Tiene_Preview { get; set; }
    }
}
