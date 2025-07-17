using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using PeliculasAPI.Datos;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;

namespace PeliculasAPI.Controllers
{
    [ApiController]
    [Route("api/cines")]
    public class SalaCineController: CustomBaseController
    {
        private readonly ApplicationDBContext context;
        private readonly IMapper mapper;
        private readonly GeometryFactory geometryFactory;

        public SalaCineController(ApplicationDBContext context, IMapper mapper, GeometryFactory geometryFactory)
            :base(context,mapper)
        {
            this.context = context;
            this.mapper = mapper;
            this.geometryFactory = geometryFactory;
        }

        [HttpGet(Name = "ObtenerSalasDeCine")]
        public async Task<ActionResult<List<SalaCineDTO>>> Get()
        {
            return await Get<SalaCine, SalaCineDTO>();
        }

        [HttpGet("{id}", Name = "ObtenerSaladeCine")]
        public async Task<ActionResult<SalaCineDTO>> Get(int id)
        {
            return await Get<SalaCine, SalaCineDTO>(id);
        }

        [HttpGet("Cercanos")]
        public async Task<ActionResult<List<SalaDeCineCercanoDTO>>> Cercanos(
            [FromQuery] SalaDeCineCercanoFiltroDTO filtro)
        {
            var ubicacionUsuario = geometryFactory.CreatePoint(new Coordinate(filtro.Longitud, filtro.Latitud));

            var salasDeCine = await context.SalaCines
                .OrderBy(x => x.Ubicacion.Distance(ubicacionUsuario))
                .Where(x => x.Ubicacion.IsWithinDistance(ubicacionUsuario, filtro.DistanciaEnKms * 1000))
                .Select(x => new SalaDeCineCercanoDTO
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Latitud = x.Ubicacion.Y,
                    Longitud = x.Ubicacion.X,
                    DistanciaEnMetros = Math.Round(x.Ubicacion.Distance(ubicacionUsuario))
                })
                .ToListAsync();

            return salasDeCine;
        }

        [HttpPost]
        public async Task<ActionResult> Post(SalaCineCreacionDTO salaCineCreacionDTO)
        {
            return await Post<SalaCine, SalaCineCreacionDTO, SalaCineDTO>(salaCineCreacionDTO, "ObtenerSaladeCine");
        }

        [HttpPut]
        public async Task<ActionResult> Put(SalaCineCreacionDTO salaCineCreacionDTO, int id)
        {
            return await Put<SalaCine,SalaCineCreacionDTO>(salaCineCreacionDTO, id);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            return await Delete<SalaCine>(id);
        }
    }
}
