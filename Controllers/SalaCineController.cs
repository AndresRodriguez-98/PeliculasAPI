using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PeliculasAPI.Datos;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;

namespace PeliculasAPI.Controllers
{
    [ApiController]
    [Route("api/cines")]
    public class SalaCineController: CustomBaseController
    {
        public SalaCineController(ApplicationDBContext context, IMapper mapper)
            :base(context,mapper)
        {
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
