using Biblioteca.Business.Interfases;
using Biblioteca.Data;
using Biblioteca.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Biblioteca.Business.Repositories
{
    public class AuthorRepository: IAuthorRepository
    {
        private readonly AppDbContext _context;
        public AuthorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAuthorAdsync(Author author)
        {
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsAuthorLinkedToBooksAsync(string authorId)
        {
            // Verifica si existen libros relacionados con el autor
            return await _context.Books.AnyAsync(a => a.Id_Author == authorId);
        }

        public async Task DeleteAuthorAsync(string id)
        {
           var author = await _context.Authors.FindAsync(id);
           if (author != null)
           {
                author.Status = Status.Inactivo; // Cambiamos el estado a inactivo en lugar de eliminar
                await _context.SaveChangesAsync();

            }

        }

        public async Task<IEnumerable<Author>> GetAlLAsync()
        {
            return await _context.Authors.Include(a => a.Books).AsNoTracking().ToListAsync();
        }

        public async Task<Author> GetByIdAsyns(string id)
        {
            var author = await _context.Authors.Include(b => b.Books).AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id_Author == id);

            if (author == null)
            {
                throw new KeyNotFoundException($"No se encontro el autor deseado con el ID{id}.");
            }
            return author;
        }

        public async Task UpdateAuthorAsync(Author author)
        {
            _context.Authors.Update(author); 
            await _context.SaveChangesAsync();
        }
    }
}
