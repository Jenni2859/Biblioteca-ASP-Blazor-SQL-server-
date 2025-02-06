namespace Biblioteca.Data.Models
{
    public class ReportBook
    {
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int Stock { get; set; }
        public double Precio { get; set; }
        public string FechaEdicion { get; set; }

        public string Photo { get; set; }
    }
}
