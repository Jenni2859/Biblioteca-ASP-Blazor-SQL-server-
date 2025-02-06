using Biblioteca.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Data
{
    public class AppDbContext: DbContext
    {
        // DbSets para las entidades
        public DbSet<User> Users { get; set; }
        public DbSet<AuthorBook> AuthorBooks { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<LoanDetails> LoanDetails { get; set; }
        public DbSet<Sanction> Sanctions { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar AuthorBook como una entidad sin clave
            modelBuilder.Entity<AuthorBook>().HasNoKey();

            modelBuilder.Entity<Book>()
                .HasOne(a => a.Author)
                .WithMany(b => b.Books)
                .HasForeignKey(a => a.Id_Author);


            // Relación: Book - Category (1:N)
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación: Book - Author (1:N)
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany()
                .HasForeignKey(b => b.Id_Author)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación: Loan - Person (1:N)
            modelBuilder.Entity<Loan>()
                .HasOne(l => l.Person)
                .WithMany(p => p.Loans)
                .HasForeignKey(l => l.PersonId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación: Loan - LoanDetails (1:N)
            modelBuilder.Entity<LoanDetails>()
                .HasOne(ld => ld.Loan)
                .WithMany(l => l.LoanDetail)
                .HasForeignKey(ld => ld.LoanId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación: LoanDetails - Book (1:N)
            modelBuilder.Entity<LoanDetails>()
                .HasOne(ld => ld.Book)
                .WithMany(b => b.LoanDetail)
                .HasForeignKey(ld => ld.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación: Sanction - Person (1:N)
            modelBuilder.Entity<Sanction>()
                .HasOne(s => s.Person)
                .WithMany()
                .HasForeignKey(s => s.PersonId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación: Sanction - Loan (1:N)
            modelBuilder.Entity<Sanction>()
                .HasOne(s => s.Loan)
                .WithMany(l => l.Sanctions)
                .HasForeignKey(s => s.LoanId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación entre User y Person (uno a uno)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Person)                // Un usuario tiene una persona asociada
                .WithOne()                             // Una persona tiene un solo usuario asociado
                .HasForeignKey<User>(u => u.Id_User)  // El Id_User en User es la clave foránea para Person
                .OnDelete(DeleteBehavior.Cascade);    // Si se elimina la persona, se elimina el usuario (si así lo deseas)



        }
    }
}
