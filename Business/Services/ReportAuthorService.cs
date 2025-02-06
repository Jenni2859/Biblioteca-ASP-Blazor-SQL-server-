using Biblioteca.Business.Interfases;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Biblioteca.Data.Models;
using Biblioteca.Data;
using Microsoft.EntityFrameworkCore;

public class ReportAuthorService : IReportAuthor
{
    private readonly AppDbContext _context;

    public ReportAuthorService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<byte[]> GenerarReporteAutorAsync(string? authorName = null, string? authorId = null)
    {
        try
        {
            var query = _context.Authors.AsQueryable();

            // Filtros por authorId o authorName
            if (!string.IsNullOrEmpty(authorId))
            {
                query = query.Where(a => a.Id_Author == authorId); // Comparar con string
            }
            else if (!string.IsNullOrEmpty(authorName))
            {
                query = query.Where(a => a.Name.Contains(authorName));
            }
            else
            {
                throw new ArgumentException("Se debe proporcionar un authorId o un authorName.");
            }

            var authors = await query.Include(a => a.Books) // Incluye los libros relacionados con el autor
                .ToListAsync();

            if (!authors.Any())
            {
                throw new Exception("No se encontraron autores con los criterios proporcionados.");
            }

            // Seleccionamos los libros de los autores encontrados
            var libros = authors.SelectMany(a => a.Books, (a, b) => new ReportBook
            {
                Titulo = b.Title,
                Autor = a.Name,
                Stock = b.Stock,
                Precio = b.Price,
                FechaEdicion = b.Edition_Date.ToString("dd/MM/yyyy"),
                Photo = a.Photo
            }).ToList();

            if (!libros.Any())
            {
                throw new Exception("No se encontraron libros para los autores especificados.");
            }

            // Descargamos la imagen del autor
            byte[] authorImageBytes = await DescargarImagen(libros.First().Photo);

            var fechaActual = DateTime.Now.ToString("dd/MM/yyyy");

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(40);

                    // Encabezado
                    page.Header()
                        .Column(column =>
                        {
                            column.Item().Text("Biblioteca INA")
                                .FontSize(24)
                                .Bold()
                                .FontColor("#FFA500")
                                .AlignCenter();

                            column.Item().Row(row =>
                            {
                                if (authorImageBytes != null && authorImageBytes.Length > 0)
                                {
                                    row.ConstantItem(40).Height(40)
                                        .Image(authorImageBytes)
                                        .FitHeight();
                                }

                                row.RelativeItem().AlignMiddle().Text(libros.First().Autor)
                                    .FontSize(16)
                                    .Italic();
                            });


                            column.Item().Text($"{fechaActual}")
                                .FontSize(12)
                                .AlignRight();
                        });

                    page.Content().Padding(20)
                        .Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(2); // Más espacio para el título
                                columns.RelativeColumn();
                                columns.ConstantColumn(80);
                                columns.ConstantColumn(100);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("Título").Bold().FontColor("#FFA500");
                                header.Cell().Text("Fecha Edición").Bold().FontColor("#FFA500").AlignCenter();
                                header.Cell().Text("Stock").Bold().FontColor("#FFA500").AlignCenter();
                                header.Cell().Text("Precio").Bold().FontColor("#FFA500").AlignCenter();
                            });

                            foreach (var libro in libros)
                            {
                                table.Cell().Text(libro.Titulo);
                                table.Cell().Text(libro.FechaEdicion).AlignCenter();
                                table.Cell().Text(libro.Stock.ToString()).AlignCenter();
                                table.Cell().Text(libro.Precio.ToString("C")).AlignCenter();
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

    // Método para descargar imagen desde URL y convertirla a byte[]
    private async Task<byte[]> DescargarImagen(string imageUrl)
    {
        if (string.IsNullOrEmpty(imageUrl))
            return null;

        try
        {
            using (HttpClient client = new HttpClient())
            {
                return await client.GetByteArrayAsync(imageUrl);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error descargando imagen: {ex.Message}");
            return null;
        }
    }
}


