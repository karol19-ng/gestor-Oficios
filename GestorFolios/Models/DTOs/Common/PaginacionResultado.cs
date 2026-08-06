namespace GestorOficios.Models.DTOs.Common
{
    public class PaginacionResultado<T>
    {
        public IEnumerable<T> Datos { get; set; } = Enumerable.Empty<T>();

        public int PaginaActual { get; set; }

        public int TamanoPagina { get; set; }

        public int TotalRegistros { get; set; }

        public int TotalPaginas =>
            (int)Math.Ceiling((double)TotalRegistros / TamanoPagina);

        public bool TienePaginaAnterior => PaginaActual > 1;

        public bool TienePaginaSiguiente => PaginaActual < TotalPaginas;
    }
}
