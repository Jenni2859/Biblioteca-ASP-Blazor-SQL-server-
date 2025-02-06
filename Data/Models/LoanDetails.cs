using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca.Data.Models
{
    public class LoanDetails
    {
        [Key]
        public int Id_LoanDetail { get; set; }

        [Required]
        [ForeignKey("Loan")]
        public int LoanId { get; set; }
        public Loan Loan { get; set; }

        [Required]
        [ForeignKey("Book")]
        public string BookId { get; set; }
        public Book Book { get; set; }

        // Estado del libro en el préstamo
        [Required]
        public BookLoanStatus Status { get; set; } = BookLoanStatus.Entregado; // Estado predeterminado

    }

    public enum BookLoanStatus
    {
        Devuelto,
        Entregado,
        Atrasado
    }
}
