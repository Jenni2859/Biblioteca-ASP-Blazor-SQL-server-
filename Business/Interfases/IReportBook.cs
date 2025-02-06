namespace Biblioteca.Business.Interfases
{
    public interface IReportBook
    {
        Task<byte[]> GenerarReporteLibroAsync();
        
    }
}
