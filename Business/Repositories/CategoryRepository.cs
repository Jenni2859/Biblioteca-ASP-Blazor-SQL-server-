using Biblioteca.Business.Interfaces;
using Biblioteca.Data;
using Biblioteca.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Biblioteca.Business.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.AsNoTracking().ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            var category = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id_Category == id);
            if (category == null)
            {
                throw new KeyNotFoundException($"No se encontró la categoría con el ID {id}.");
            }
            return category;
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                category.Status = Status.Inactivo; // Cambiamos el estado a inactivo en lugar de eliminar
                await _context.SaveChangesAsync();
            }
        }
    }
}
