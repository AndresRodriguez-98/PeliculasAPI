﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;

namespace PeliculasAPI.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Genero, GeneroDTO>().ReverseMap();
            CreateMap<GeneroCreacionDTO, Genero>();

            CreateMap<IdentityUser, UsuarioDTO>();

            CreateMap<Review, ReviewDTO>()
                .ForMember(x => x.NombreUsuario, x => x.MapFrom(y => y.Usuario.UserName));

            CreateMap<ReviewDTO, Review>();
            CreateMap<ReviewCreacionDTO, Review>();

            // un punto hacia latitud/longitud:
            CreateMap<SalaCine, SalaCineDTO>()
                .ForMember(x => x.Latitud, x => x.MapFrom(y => y.Ubicacion.Y))
                .ForMember(x => x.Longitud, x => x.MapFrom(y => y.Ubicacion.X));

            // long/lat hacia un punto:
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            CreateMap<SalaCineDTO, SalaCine>()
                .ForMember(x => x.Ubicacion, x => x.MapFrom(y => geometryFactory.CreatePoint(new Coordinate(y.Longitud, y.Latitud))));

            CreateMap<SalaCineCreacionDTO, SalaCine>()
                .ForMember(x => x.Ubicacion, x => x.MapFrom(y => geometryFactory.CreatePoint(new Coordinate(y.Longitud, y.Latitud))));


            CreateMap<Actor, ActorDTO>().ReverseMap();
            CreateMap<ActorCreacionDTO, Actor>()
                .ForMember(x => x.Foto, options => options.Ignore());
            CreateMap<ActorPatchDTO, Actor>().ReverseMap();

            CreateMap<Pelicula, PeliculaDTO>().ReverseMap();
            CreateMap<PeliculaCreacionDTO, Pelicula>()
                .ForMember(x => x.Poster, options => options.Ignore())
                .ForMember(x => x.PeliculasGeneros, options => options.MapFrom(MapPeliculasGeneros))
                .ForMember(x => x.PeliculasActores, options => options.MapFrom(MapPeliculasActores));

            CreateMap<Pelicula, PeliculaDetallesDTO>()
                .ForMember(x => x.Generos, options => options.MapFrom(MapPeliculasGeneros))
                .ForMember(x => x.Actores, options => options.MapFrom(MapPeliculasActores));

            CreateMap<PeliculaPatchDTO, Pelicula>().ReverseMap();

        }

        private List<ActorPeliculaDetalleDTO> MapPeliculasActores(Pelicula pelicula,
            PeliculaDetallesDTO peliculaDetallesDTO)
        {
            var resultado = new List<ActorPeliculaDetalleDTO>();
            if (pelicula.PeliculasActores == null) { return resultado; };
            foreach(var actorPelicula in pelicula.PeliculasActores)
            {
                resultado.Add(new ActorPeliculaDetalleDTO
                {
                    ActorId = actorPelicula.ActorId,
                    Personaje = actorPelicula.Personaje,
                    Nombre = actorPelicula.Actor.Nombre
                });
            }
            return resultado;
        }

        private List<GeneroDTO> MapPeliculasGeneros(Pelicula pelicula, PeliculaDetallesDTO peliculaDetallesDTO)
        {
            var resultado = new List<GeneroDTO>();
            if (pelicula.PeliculasGeneros == null) { return resultado; };
            foreach(var generoPelicula in pelicula.PeliculasGeneros)
            {
                resultado.Add(new GeneroDTO() { Id = generoPelicula.GeneroId, 
                    Nombre = generoPelicula.Genero.Nombre });
            }

            return resultado;
        }

        private List<PeliculasGeneros> MapPeliculasGeneros(PeliculaCreacionDTO peliculaCreacionDTO,
            Pelicula pelicula)
        {
            var resultado = new List<PeliculasGeneros>();
            if (peliculaCreacionDTO.GenerosIDs == null) { return resultado; }
            foreach (var id in peliculaCreacionDTO.GenerosIDs)
            {
                resultado.Add(new PeliculasGeneros() { GeneroId = id });
            }

            return resultado;
        }

        private List<PeliculasActores> MapPeliculasActores(PeliculaCreacionDTO peliculaCreacionDTO,
            Pelicula pelicula)
        {
            var resultado = new List<PeliculasActores>();
            if (peliculaCreacionDTO.Actores == null) { return resultado; }
            foreach (var actor in peliculaCreacionDTO.Actores)
            {
                resultado.Add(new PeliculasActores() { ActorId = actor.ActorId, Personaje = actor.Personaje });
            }

            return resultado;
        }
    }
}
