using iTextSharp.text;
using iTextSharp.text.pdf;

namespace FurnitureShop
{
    public class HeaderFooter : PdfPageEventHelper
    {
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            PdfPTable tbHeader = new PdfPTable(3);
            tbHeader.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
            tbHeader.DefaultCell.Border = 0;

            tbHeader.AddCell(new Paragraph());


            PdfPCell _cell = new PdfPCell(new Paragraph("Invoice"));
            _cell.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell.Border = 0;
            tbHeader.AddCell(_cell);
            tbHeader.AddCell(new Paragraph());

            tbHeader.WriteSelectedRows(0, -1, document.LeftMargin, writer.PageSize.GetTop(document.TopMargin) + 40, writer.DirectContent);

            PdfPTable tbFooter = new PdfPTable(3);
            tbFooter.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
            tbFooter.DefaultCell.Border = 0;
            _cell = new PdfPCell(new Paragraph("Jordan/Irbid"));
            _cell.HorizontalAlignment = Element.ALIGN_CENTER;

            _cell.Border = 0;
            tbFooter.AddCell(_cell);
            tbFooter.AddCell(new Paragraph());

            tbFooter.WriteSelectedRows(0, -1, document.LeftMargin, writer.PageSize.GetBottom(document.BottomMargin) - 5, writer.DirectContent);




        }
    }
}
