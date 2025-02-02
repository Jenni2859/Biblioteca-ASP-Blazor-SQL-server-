using Microsoft.AspNetCore.Mvc;
using Biblioteca.Business.Interfases;

namespace Biblioteca.Business.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class ReportsController : Controller
    {
        private readonly IReportBook _reporteBook;

        public ReportsController(IReportBook reporteBook)
        {
            _reporteBook = reporteBook;
        }

        [HttpGet("descargar")]
        public async Task<IActionResult> DescargarReporte()
        {
            var pdfBytes = await _reporteBook.GenerarReporteAsync();
            return File(pdfBytes, "application/pdf", "ReporteLibros.pdf");
        }

        [HttpGet("ver")]
        public async Task<IActionResult> VerReporte()
        {
            var pdfBytes = await _reporteBook.GenerarReporteAsync();
            return File(pdfBytes, "application/pdf");
        }
    }
}
