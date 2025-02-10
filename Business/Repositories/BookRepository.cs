using Biblioteca.Business.Interfases;
using Biblioteca.Data;
using Biblioteca.Data.Models;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using System;

namespace Biblioteca.Business.Repositories
{
    public class BookRepository: IBookRepository
    {
        private readonly AppDbContext _context;
        public BookRepository(AppDbContext context) 
        { 
            _context = context;
        }

        public async Task AddBookAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(string id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                book.Status = Status.Inactivo; // Cambiamos el estado a inactivo en lugar de eliminar
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<Book>> GetAlLAsync()
        {
            return await _context.Books.Include(a => a.Author).AsNoTracking().Include(c => c.Category).ToListAsync();
             
        }

        public async Task<Book> GetByIdAsyns(string id)
        {
            var book = await _context.Books.Include(a => a.Author).AsNoTracking().Include(c => c.Category)
                .FirstOrDefaultAsync(b => b.Id_Book == id);

            if(book == null)
            {
                throw new KeyNotFoundException($"No se encontro el libro deseado con el ID{id}.");
            }
            return book;
        }

        public async Task UpdateBookAsync(Book book)
        {
            var existing = await _context.Books.FindAsync(book.Id_Book);
            if (existing != null)
            {
                _context.Books.Update(book);
                await _context.SaveChangesAsync();
            }
            
        }
    }
}
