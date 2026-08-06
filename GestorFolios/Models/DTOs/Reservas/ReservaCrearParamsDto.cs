namespace GestorOficios.Models.DTOs.Reservas
{
    public class ReservaCrearParamsDto
    {
        public int Id_Usuario { get; set; }
        public int Codigo_Departamento { get; set; }
        public int Numero_Registro { get; set; }
        public string Codigo_Referencia { get; set; } = string.Empty;
        public int Minutos_Validez { get; set; } = 10;
    }
}
