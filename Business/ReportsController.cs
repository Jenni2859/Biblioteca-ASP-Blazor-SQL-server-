using Microsoft.AspNetCore.Mvc;
using Biblioteca.Business.Interfases;

namespace Biblioteca.Business.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class ReportsController : Controller
    {
        private readonly IReportBook _reportBookService;
        private readonly IReportAuthor _reportAuthorService;

        public ReportsController(IReportBook reportBookService, IReportAuthor reportAuthorService)
        {
            _reportBookService = reportBookService;
            _reportAuthorService = reportAuthorService;
        }

        // Reporte de todos los libros
        [HttpGet("descargar-libros")]
        public async Task<IActionResult> DescargarReporteLibros()
        {
            var pdfBytes = await _reportBookService.GenerarReporteLibroAsync();
            return File(pdfBytes, "application/pdf", "ReporteLibros.pdf");
        }

        [HttpGet("ver-libros")]
        public async Task<IActionResult> VerReporteLibros()
        {
            var pdfBytes = await _reportBookService.GenerarReporteLibroAsync();
            return File(pdfBytes, "application/pdf");
        }

        // Reporte de libros por autor
        [HttpGet("descargar-libros-autor")]
        public async Task<IActionResult> DescargarReporteAutor([FromQuery] string? authorName, [FromQuery] string? authorId)
        {
            var pdfBytes = await _reportAuthorService.GenerarReporteAutorAsync(authorName, authorId);
            return File(pdfBytes, "application/pdf", "ReporteAutor.pdf");
        }

        [HttpGet("ver-libros-autor")]
        public async Task<IActionResult> VerReporteAutor([FromQuery] string? authorName, [FromQuery] string? authorId)
        {
            var pdfBytes = await _reportAuthorService.GenerarReporteAutorAsync(authorName, authorId);
            return File(pdfBytes, "application/pdf");
        }
    }
}
