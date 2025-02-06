using Biblioteca.Business.Interfaces;
using Biblioteca.Data;
using Biblioteca.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.Business.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly AppDbContext _context;

        public PersonRepository(AppDbContext context)
        {
            _context = context;
        }

        // Método para agregar una nueva persona
        public async Task AddPersonAsync(Person person)
        {
            await _context.Persons.AddAsync(person);
            await _context.SaveChangesAsync();
        }

        // Método para actualizar una persona existente
        public async Task UpdatePersonAsync(Person person)
        {
            _context.Persons.Update(person);
            await _context.SaveChangesAsync();
        }

        // Método para desactivar una persona (cambiar el estado a Inactivo)
        public async Task DeletePersonAsync(string personId)
        {
            var person = await _context.Persons.FirstOrDefaultAsync(p => p.Id_Person == personId);
            if (person != null)
            {
                person.Status = Status.Inactivo; // Cambiamos el estado a inactivo en lugar de eliminar
                await _context.SaveChangesAsync();
            }
        }

        // Método para obtener una persona por su ID
        public async Task<Person> GetByIdAsync(string personId)
        {
            var person = await _context.Persons
                .Include(p => p.Loans) // Puedes incluir otras relaciones si es necesario
                .FirstOrDefaultAsync(p => p.Id_Person == personId);

            if (person == null)
            {
                throw new KeyNotFoundException($"No se encontró la persona con el ID {personId}.");
            }

            return person;
        }

        // Método para obtener todas las personas
        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _context.Persons  
                .Include(p => p.Loans) // Incluir las relaciones si es necesario
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
