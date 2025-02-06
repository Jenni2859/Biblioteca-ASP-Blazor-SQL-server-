using Biblioteca.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Biblioteca.Business.Interfases
{
    public interface ILoanDetailsRepository
    {
        Task AddLoanDetailAsync(LoanDetails loanDetails);
        Task DeleteLoanDetailAsync(int id);
        Task<IEnumerable<LoanDetails>> GetAllAsync();
        Task<LoanDetails> GetByIdAsync(int id);
        Task UpdateLoanDetailAsync(LoanDetails loanDetails);
    }
}
