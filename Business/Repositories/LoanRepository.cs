using Biblioteca.Business.Interfaces;
using Biblioteca.Data;
using Biblioteca.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Biblioteca.Business.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly AppDbContext _context;

        public LoanRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Loan>> GetAllAsync()
        {
            return await _context.Loans
                .Include(l => l.Person)
                .Include(l => l.LoanDetail)
                .ThenInclude(d => d.Book)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Loan> GetByIdAsync(int id)
        {
            var loan = await _context.Loans
                .Include(l => l.Person)
                .Include(l => l.LoanDetail)
                .ThenInclude(d => d.Book)
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.Id_Loan == id);

            if (loan == null)
            {
                throw new KeyNotFoundException($"No se encontró el préstamo con el ID {id}.");
            }
            return loan;
        }

        public async Task AddLoanAsync(Loan loan)
        {
            await _context.Loans.AddAsync(loan);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLoanAsync(Loan loan)
        {
            _context.Loans.Update(loan);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLoanAsync(int id)
        {
            var loan = await _context.Loans.FindAsync(id);
            if (loan != null)
            {
                loan.Status = Status.Inactivo; // Cambiamos el estado a inactivo en lugar de eliminar
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetTotalBooksByLoanAsync(int loanId)
        {
            return await _context.LoanDetails
                .Where(d => d.LoanId == loanId)
                .CountAsync();
        }
    }
}
