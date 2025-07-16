using Microsoft.EntityFrameworkCore;
using PeliculasAPI.Entidades;

namespace PeliculasAPI.Datos
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PeliculasActores>()
                .HasKey(x => new { x.PeliculaId, x.ActorId });

            modelBuilder.Entity<PeliculasGeneros>()
                .HasKey(x => new { x.PeliculaId, x.GeneroId });

            modelBuilder.Entity<PeliculasSalaCine>()
                .HasKey(x => new { x.PeliculaId, x.SalaCineId });

            base.OnModelCreating(modelBuilder);
        }

        //private void SeedData(ModelBuilder modelBuilder)
        //{

        //}

        public DbSet<Genero> Generos { get; set; }
        public DbSet<Actor> Actores { get; set; }
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<PeliculasActores> PeliculasActores { get; set; }
        public DbSet<PeliculasGeneros> PeliculasGeneros { get; set; }
        public DbSet<SalaCine> SalaCines { get; set; }
        public DbSet<PeliculasSalaCine> PeliculasSalaCines { get; set; }
    }
}
