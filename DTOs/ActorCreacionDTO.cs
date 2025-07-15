using PeliculasAPI.Validaciones;

namespace PeliculasAPI.DTOs
{
    public class ActorCreacionDTO: ActorPatchDTO
    {
        // 4 megabytes maximo para cada foto
        [PesoArchivoValidacion(4)]
        [TipoArchivoValidacion(grupoTipoArchivo: GrupoTipoArchivo.Imagen)]
        public IFormFile Foto { get; set; }
    }
}
