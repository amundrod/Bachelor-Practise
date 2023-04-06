using Bachelor;
using IronPdf;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PdfSearch.Api.Data;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;



namespace PdfSearch.Api.Services
{
    public class PdfIndexingService
    {
        private readonly IConfiguration _configuration;

        public PdfIndexingService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void IndexPdf(string filePath, string fileName)
        {
            // Load the PDF file using IronPDF
            var pdf = PdfDocument.FromFile(filePath);

            // Extract the text from each page of the PDF
            var textList = new List<string>();
            for (int i = 0; i < pdf.PageCount; i++)
            {
                var pageText = pdf.ExtractTextFromPage(i);
                textList.Add(pageText);
            }

            // Save the extracted text to a database
            var optionsBuilder = new DbContextOptionsBuilder<PdfSearchDbContext>();
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("PdfSearchDb"));
            using (var context = new PdfSearchDbContext(optionsBuilder.Options))
            {
                var document = new Bachelor.Document(fileName, new Content(string.Join(" ", textList)));

                context.Documents.Add(document);
                context.SaveChanges();
            }

        }

        public List<string> SearchPdf(string searchTerm)
        {
            // Search the database for documents that contain the search term
            var optionsBuilder = new DbContextOptionsBuilder<PdfSearchDbContext>();
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("PdfSearchDb"));
            using (var context = new PdfSearchDbContext(optionsBuilder.Options))
            {
                var matchingDocuments = context.Documents
                    .Where(d => d.Content.Contains(searchTerm))
                    .ToList();

                return matchingDocuments.Select(d => d.Name).ToList();
            }
        }

    }
}
