namespace PeliculasAPI.Entidades
{
    public class PeliculasSalaCine
    {
        public int PeliculaId { get; set; }
        public int SalaCineId { get; set; }
        public Pelicula Pelicula { get; set; }
        public SalaCine SalaCine { get; set; }
    }
}
