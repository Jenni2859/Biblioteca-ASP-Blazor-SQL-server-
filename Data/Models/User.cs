using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca.Data.Models
{
    public class User
    {
        [Key]
        [ForeignKey("Person")]
        public virtual string Id_User { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(100, ErrorMessage = "La contraseña no puede exceder los 100 caracteres.")]
        public string Password { get; set; }

        public Person Person { get; set; }

        // Propiedades opcionales para la autenticación
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastLogin { get; set; }

        public Status Status { get; set; } = Status.Activo;
    }
}

