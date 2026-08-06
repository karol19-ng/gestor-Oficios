namespace GestorOficios.Models.Entities
{
    public class Correlativo_Oficio
    {
        public int Codigo_Departamento { get; set; }
        public int Año { get; set; }
        public int Ultimo_Numero { get; set; }

        // Navegación
        public Departamentos Departamento { get; set; } = null!;
    }
}
