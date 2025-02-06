using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca.Data.Models
{
    public enum SanctionType
    {
        Time,   // Sanción por días
        Money   // Sanción por monto monetario
    }

    public class Sanction
    {
        [Key]
        public int Id_Sanction { get; set; }

        // Relacion con Persona
        [Required]
        [ForeignKey("Person")]
        public string PersonId { get; set; }
        public Person Person { get; set; }

        // Relación con Préstamo
        [Required]
        [ForeignKey("Loan")]
        public int LoanId { get; set; }
        public Loan Loan { get; set; }

        // Tipo de sanción: por tiempo o por dinero
        [Required]
        public SanctionType Type { get; set; }

        // Campos para sanción por días
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        // Campo para sanción monetaria
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a 0.")]
        public double? FineAmount { get; set; }

        [Required(ErrorMessage = "El motivo de la sanción es obligatorio.")]
        [MaxLength(255)]
        public string Reason { get; set; } = "Entrega tardía del libro.";

        // Propiedad calculada para verificar si la sanción por tiempo sigue activa
        public bool IsActive => Type == SanctionType.Time && StartDate.HasValue && EndDate.HasValue
                                && DateTime.Now >= StartDate.Value && DateTime.Now <= EndDate.Value;

        // Validación personalizada
        public static ValidationResult ValidateSanction(Sanction sanction, ValidationContext context)
        {
            if (sanction.Type == SanctionType.Time)
            {
                if (!sanction.StartDate.HasValue || !sanction.EndDate.HasValue)
                    return new ValidationResult("Las fechas de inicio y fin son obligatorias para una sanción por tiempo.");
                if (sanction.FineAmount.HasValue)
                    return new ValidationResult("Una sanción por tiempo no debe tener un monto de multa.");
            }
            else if (sanction.Type == SanctionType.Money)
            {
                if (!sanction.FineAmount.HasValue)
                    return new ValidationResult("El monto de la multa es obligatorio para una sanción monetaria.");
                if (sanction.StartDate.HasValue || sanction.EndDate.HasValue)
                    return new ValidationResult("Una sanción monetaria no debe tener fechas de inicio o fin.");
            }

            return ValidationResult.Success;
        }
        public Status Status { get; set; } = Status.Activo;
    }
    
    
}
