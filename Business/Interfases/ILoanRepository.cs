using Biblioteca.Data.Models;

namespace Biblioteca.Business.Interfaces
{
    public interface ILoanRepository
    {
        Task<IEnumerable<Loan>> GetAllAsync();
        Task<Loan> GetByIdAsync(int id);
        Task AddLoanAsync(Loan loan);
        Task UpdateLoanAsync(Loan loan);
        Task DeleteLoanAsync(int id);
        Task<int> GetTotalBooksByLoanAsync(int loanId);
    }
}
