﻿using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.DTOs
{
    public class SalaCineCreacionDTO
    {
        [Required]
        [StringLength(200)]
        public string Nombre { get; set; }
        [Range(-90, 90)]
        public double Latitud { get; set; }
        [Range(-180, 180)]
        public double Longitud { get; set; }
    }
}
