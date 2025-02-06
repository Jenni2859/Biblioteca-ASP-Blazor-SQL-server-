using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Data.Models
{
    public class Author
    {
        [Key]
        public string Id_Author { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "La edad es obligatoria.")]
        [Range(18, 120, ErrorMessage = "La edad debe estar entre 18 y 120 años.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "La biografía es obligatoria.")]
        [StringLength(1000, ErrorMessage = "La biografía no puede superar los 1000 caracteres.")]
        public string Biography { get; set; }

        [Url(ErrorMessage = "Debe ser una URL válida para la foto.")]
        public string? Photo { get; set; }

        // Relación con Book
        public ICollection<Book> Books { get; set; } = new List<Book>();

        // Propiedad de estado para indicar si el autor está activo o inactivo
        public Status Status { get; set; } = Status.Activo;
    }
}
