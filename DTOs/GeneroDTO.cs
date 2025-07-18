﻿using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.DTOs
{
    public class GeneroDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(40)]
        public required string Nombre { get; set; }
    }
}
