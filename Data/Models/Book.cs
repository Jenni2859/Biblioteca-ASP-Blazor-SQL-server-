using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public int Stock { get; set; }  

        // Relación con Autor
        [Required(ErrorMessage = "El autor es obligatorio.")]
        [ForeignKey("Author")]
        public string Id_Author { get; set; } = string.Empty;
        public Author Author { get; set; }

        // Relación con Categoría
        [Required(ErrorMessage = "La categoría es obligatoria.")]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        // Relación con detalles préstamos
        public ICollection<LoanDetails> LoanDetail { get; set; } = new List<LoanDetails>();

        public Status Status { get; set; } = Status.Activo;
    }
    // Enum para manejar el estado
    public enum Status
    {
        Activo = 1,
        Inactivo = 0
    }
}
