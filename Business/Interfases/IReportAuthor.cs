namespace Biblioteca.Business.Interfases
{
    public interface IReportAuthor
    {
        Task<byte[]> GenerarReporteAutorAsync(string? authorName = null, string? authorId = null);
    }
}
