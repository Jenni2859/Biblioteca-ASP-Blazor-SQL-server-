using Biblioteca.Business.Interfases;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Biblioteca.Data.Models;
using Biblioteca.Data;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Business.Services
{
    public class ReportBookService : IReportBook
    {
        private readonly AppDbContext _context;

        public ReportBookService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<byte[]> GenerarReporteLibroAsync()
        {
            try
            {
                var libros = await _context.Books
                                           .Include(b => b.Author)
                                           .Select(b => new ReportBook
                                           {
                                               Titulo = b.Title,
                                               Autor = b.Author.Name,
                                               Stock = b.Stock,
                                               Precio = b.Price,
                                               FechaEdicion = b.Edition_Date.ToString("dd/MM/yyyy")
                                           })
                                           .ToListAsync();

                if (!libros.Any())
                {
                    throw new Exception("No se encontraron libros en la base de datos.");
                }

                var fechaActual = DateTime.Now.ToString("dd/MM/yyyy");

                var pdf = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(40);

                        // Definir encabezado con título principal y subtítulo
                        page.Header()
                            .Column(column =>
                            {
                                column.Item().Text("Biblioteca INA") // Nombre de la biblioteca
                                    .FontSize(24)
                                    .Bold()
                                    .FontColor("#FFA500")  // Color Naranja
                                    .AlignCenter();

                                column.Item().Text("Reporte de Libros")
                                    .FontSize(16)
                                    .Italic();

                                column.Item().Text($"{fechaActual}")
                                    .FontSize(12)
                                    .AlignRight();
                            });

                        page.Content().Padding(20)
                            .Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(4); // Más espacio para el título
                                    columns.RelativeColumn(4); // Más espacio para el autor
                                    columns.RelativeColumn(2); // Stock
                                    columns.RelativeColumn(2); // Precio
                                    columns.RelativeColumn(2); // Fecha de edición
                                });

                                table.Header(header =>
                                {
                                    header.Cell().Text("Título").Bold().FontColor("#FFA500");
                                    header.Cell().Text("Autor").Bold().FontColor("#FFA500");
                                    header.Cell().Text("Stock").Bold().FontColor("#FFA500").AlignCenter();
                                    header.Cell().Text("Precio").Bold().FontColor("#FFA500").AlignCenter();
                                    header.Cell().Text("Edición").Bold().FontColor("#FFA500").AlignCenter();
                                });

                                foreach (var libro in libros)
                                {
                                    table.Cell().Text(libro.Titulo);
                                    table.Cell().Text(libro.Autor);
                                    table.Cell().Text(libro.Stock.ToString()).AlignCenter();
                                    table.Cell().Text(libro.Precio.ToString("C"));
                                    table.Cell().Text(libro.FechaEdicion);
                                }
                            });
                    });
                });

                return pdf.GeneratePdf();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generando el reporte: {ex.Message}");
                throw new Exception("Error al generar el reporte PDF", ex);
            }
        }
    }
}
