using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace PdfSearch.Api.Data
{
    public class PdfSearchDbContext : DbContext
    {
        public PdfSearchDbContext(DbContextOptions<PdfSearchDbContext> options) : base(options)
        {
        }

        public DbSet<Document> Documents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set up any additional configurations for the Document entity
            modelBuilder.Entity<Document>().HasKey(d => d.Id);
            modelBuilder.Entity<Document>().Property(d => d.Name).IsRequired();
            modelBuilder.Entity<Document>().Property(d => d.Content).IsRequired();
        }
    }
}
