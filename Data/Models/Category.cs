using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Data.Models
{
    public class Category
    {
        [Key]
        public int Id_Category { get; set; }

        [Required(ErrorMessage = "El nombre de la categoría es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El nombre de la categoría no puede superar los 100 caracteres.")]
        public string Name { get; set; } = string.Empty;

        // Relacion con libros
        public ICollection<Book> Books { get; set; } = new List<Book>();

        public Status Status { get; set; } = Status.Activo;

    }
}
