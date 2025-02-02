namespace Biblioteca.Business.Interfases
{
    public interface IReportBook
    {
        Task<byte[]> GenerarReporteAsync();
    }
}
