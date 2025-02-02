using System;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Data.Models
{
    public class Book
    {
        [Key]
        public string Id_Book { get; set; }

        [Required(ErrorMessage = "El título es obligatorio.")]
        public string Title { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El número de páginas debe ser al menos 1.")]
        public int Pages { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0.")]
        public double Price { get; set; }

        [Required(ErrorMessage = "La fecha de edición es obligatoria.")]
        public DateTime Edition_Date { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "El stock es obligatorio.")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo.")]
        public int Stock { get; set; }  // Se cambió a mayúscula para seguir la convención de C#

        // Relación con el autor
        [Required(ErrorMessage = "El autor es obligatorio.")]
        public string Id_Author { get; set; }
        public Author Author { get; set; }
    }
}
