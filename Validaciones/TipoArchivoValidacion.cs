using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Validaciones
{
    public class TipoArchivoValidacion: ValidationAttribute
    {
        private readonly string[] tiposValidos;

        public TipoArchivoValidacion(string[] tiposValidos)
        {
            this.tiposValidos = tiposValidos;
        }

        public TipoArchivoValidacion(GrupoTipoArchivo grupoTipoArchivo)
        {
            if (grupoTipoArchivo == GrupoTipoArchivo.Imagen)
            {
                tiposValidos = new string[] { "image/jpg", "image/png", "image/gif" };
            }
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            IFormFile formfile = value as IFormFile;

            if (formfile == null)
            {
                return ValidationResult.Success;
            }

            if (!tiposValidos.Contains(formfile.ContentType))
            {
                return new ValidationResult($"El tipo de archivo debe ser uno de los siguientes: {string.Join(", ", tiposValidos)}");
            }

            return ValidationResult.Success;
        }
    }
}
