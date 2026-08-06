namespace GestorOficios.Models.DTOs.Common
{
    public class PaginacionParametros
    {
        public int Pagina { get; set; } = 1;

        private int _tamanoPagina = 10;

        public int TamanoPagina
        {
            get => _tamanoPagina;
            set => _tamanoPagina = value > 50 ? 50 : value;
        }
    }
}
