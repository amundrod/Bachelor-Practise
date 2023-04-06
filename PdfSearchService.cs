namespace Bachelor
{
    public class PdfSearchService
    {
        private readonly PdfDbContext _dbContext;

        public PdfSearchService(PdfDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Pdf> Search(string searchText)
        {
            var pdfs = _dbContext.Pdfs.Where(p => p.Text.Contains(searchText)).ToList();

            foreach (var pdf in pdfs)
            {
                var highlightedText = pdf.Text.Replace(searchText, $"<mark>{searchText}</mark>");
                var html = $"<html><body>{highlightedText}</body></html>";
                var renderer = new IronPdf.HtmlToPdf();
                var highlightedPdf = renderer.RenderHtmlAsPdf(html);
                var newFilePath = pdf.FilePath.Replace(".pdf", $"_highlighted_{searchText}.pdf");
                highlightedPdf.SaveAs(newFilePath);

                pdf.FilePath = newFilePath;
            }

            _dbContext.SaveChanges();

            return pdfs;
        }
    }


}
