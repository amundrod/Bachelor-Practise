using Microsoft.EntityFrameworkCore;

namespace Bachelor
{
    public class PdfDbContext : DbContext
    {
        public PdfDbContext(DbContextOptions<PdfDbContext> options) : base(options)
        {
        }

        public DbSet<Pdf> Pdfs { get; set; }
    }

    public class Pdf
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public string Text { get; set; }
    }

}
