using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca.Data.Models
{
    public class Person
    {
        [Key]
        public string Id_Person { get; set; }

        [Required(ErrorMessage = "El nombre completo es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre completo no puede exceder los 100 caracteres.")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        [StringLength(100, ErrorMessage = "El correo electrónico no puede exceder los 100 caracteres.")]
        public string Email { get; set; } = string.Empty;

        [Phone(ErrorMessage = "El número de teléfono no tiene un formato válido.")]
        [StringLength(15, ErrorMessage = "El número de teléfono no puede exceder los 15 caracteres.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [StringLength(200, ErrorMessage = "La dirección no puede exceder los 200 caracteres.")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "El tipo de persona es obligatorio.")]
        public int PersonTypeId { get; set; }

        [ForeignKey("PersonTypeId")]
        public PersonType PersonType { get; set; }

        // Campos opcionales si es estudiante
        [StringLength(10, ErrorMessage = "El grado no puede exceder los 10 caracteres.")]
        public string? Grade { get; set; }

        [StringLength(10, ErrorMessage = "La sección no puede exceder los 10 caracteres.")]
        public string? Section { get; set; }

        [StringLength(100, ErrorMessage = "El nombre del tutor no puede exceder los 100 caracteres.")]
        public string? GuardianName { get; set; }

        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
        public Status Status { get; set; } = Status.Activo;
    }
}

    public enum PersonType
    {
        Estudiante,
        Profesor,
        Otro
    }
