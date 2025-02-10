using Biblioteca.Business.Interfaces;
using Biblioteca.Data;
using Biblioteca.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Biblioteca.Business.Repositories
{
    public class SanctionRepository : ISanctionRepository
    {
        private readonly AppDbContext _context;

        public SanctionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Sanction>> GetAllAsync()
        {
            return await _context.Sanctions.Include(s => s.Person).Include(s => s.Loan).AsNoTracking().ToListAsync();
        }

        public async Task<Sanction> GetByIdAsync(int id)
        {
            var sanction = await _context.Sanctions
                .Include(s => s.Person)
                .Include(s => s.Loan)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id_Sanction == id);

            if (sanction == null)
            {
                throw new KeyNotFoundException($"No se encontró la sanción con el ID {id}.");
            }
            return sanction;
        }

        public async Task AddSanctionAsync(Sanction sanction)
        {
            await _context.Sanctions.AddAsync(sanction);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSanctionAsync(Sanction sanction)
        {
            var existing = await _context.Sanctions.FindAsync(sanction.Id_Sanction);
            if (existing != null)
            {
                existing.PersonId = sanction.PersonId;
                existing.LoanId = sanction.LoanId;
                existing.Type = sanction.Type;
                existing.StartDate = sanction.StartDate;
                existing.EndDate = sanction.EndDate;
                existing.FineAmount = sanction.FineAmount;
                existing.Reason = sanction.Reason;
                existing.Status = sanction.Status;
                existing.StatusFineAmount = sanction.StatusFineAmount;

                _context.Sanctions.Update(existing);
                await _context.SaveChangesAsync();
            }
            
        }

        public async Task DeleteSanctionAsync(int id)
        {
            var sanction = await _context.Sanctions.FindAsync(id);
            if (sanction != null)
            {
                sanction.Status = Status.Inactivo; // Cambiamos el estado a inactivo en lugar de eliminar
                await _context.SaveChangesAsync();
            }
        }
    }
}
