namespace PeliculasAPI.DTOs
{
    public class PaginacionDTO
    {
        public int Pagina { get; set; } = 1;

        private int cantidadRegistros = 10;
        private readonly int cantidadMaximaRegistrosPorPagina = 50;

        public int CantidadRegistrosPorPagina {
            get => cantidadRegistros;
            set
            {
                cantidadRegistros = (value > cantidadMaximaRegistrosPorPagina ? cantidadMaximaRegistrosPorPagina : value);
            } 
        }
    }
}
