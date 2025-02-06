using Biblioteca.Business.Interfases;
using Biblioteca.Data;
using Biblioteca.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Biblioteca.Business.Repositories
{
    public class LoanDetailsRepository : ILoanDetailsRepository
    {
        private readonly AppDbContext _context;

        public LoanDetailsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddLoanDetailAsync(LoanDetails loanDetails)
        {
            await _context.LoanDetails.AddAsync(loanDetails);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLoanDetailAsync(int id)
        {
            var loanDetail = await _context.LoanDetails.FindAsync(id);
            if (loanDetail != null)
            {
                _context.LoanDetails.Remove(loanDetail);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<LoanDetails>> GetAllAsync()
        {
            return await _context.LoanDetails
                                 .Include(ld => ld.Loan)
                                 .Include(ld => ld.Book)
                                 .AsNoTracking()
                                 .ToListAsync();
        }

        public async Task<LoanDetails> GetByIdAsync(int id)
        {
            var loanDetail = await _context.LoanDetails
                                            .Include(ld => ld.Loan)
                                            .Include(ld => ld.Book)
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync(ld => ld.Id_LoanDetail == id);

            if (loanDetail == null)
            {
                throw new KeyNotFoundException($"No se encontró el detalle de préstamo con el ID {id}.");
            }

            return loanDetail;
        }

        public async Task UpdateLoanDetailAsync(LoanDetails loanDetails)
        {
            _context.LoanDetails.Update(loanDetails);
            await _context.SaveChangesAsync();
        }
    }
}
