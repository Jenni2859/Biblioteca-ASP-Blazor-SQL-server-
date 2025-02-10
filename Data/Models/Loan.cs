using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Data.Models
{
    public class Loan
    {
        [Key]
        public int Id_Loan { get; set; }

        [Required]
        [ForeignKey("Person")]
        public string PersonId { get; set; }
        public Person Person { get; set; }


        [Required(ErrorMessage = "La fecha de préstamo es obligatoria.")]
        public DateTime LoanDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "La fecha de devolución es obligatoria.")]
        public DateTime ReturnDate { get; set; } = DateTime.Now;

        public DateTime? ActualReturnDate { get; set; }

        public bool IsReturned => ActualReturnDate.HasValue;

        public ICollection<Sanction> Sanctions { get; set; } = new List<Sanction>();
        public ICollection<LoanDetails> LoanDetail { get; set;  } = new List<LoanDetails>();

        // Total de libros prestados en este préstamo
        //[NotMapped] // Se calcula automáticamente
        //public int TotalBooks => LoanDetails;
        public int TotalBooks { get; set; }

        public Status Status { get; set; } = Status.Activo;
    }
}
 
