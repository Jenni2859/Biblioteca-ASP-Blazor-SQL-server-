using Biblioteca.Business.Interfaces;
using Biblioteca.Data;
using Biblioteca.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Biblioteca.Business.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(string userId)
        {
            var user = await _context.Users.Include(u => u.Person).FirstOrDefaultAsync(u => u.Id_User == userId);
            if (user != null)
            {
                user.Status = Status.Inactivo; // Cambiamos el estado a inactivo en lugar de eliminar
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.Include(u => u.Person).AsNoTracking().ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            var user = await _context.Users.Include(u => u.Person).AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id_User == userId);

            if (user == null)
            {
                throw new KeyNotFoundException($"No se encontró el usuario con el ID {userId}.");
            }

            return user;
        }

        public async Task UpdateUserAsync(User user)
        {
            var existing = await _context.Users.FindAsync(user.Id_User);
            if (existing != null)
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
           
        }
    }
}
