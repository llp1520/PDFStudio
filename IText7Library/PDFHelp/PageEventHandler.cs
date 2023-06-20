using iText.IO.Font;
using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IText7Library.PDFHelp
{
	public class PageEventHandler : IEventHandler
	{
		public void HandleEvent(Event @event)
		{
			PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
			PdfDocument pdfDoc = docEvent.GetDocument();
			PdfPage currentPage = docEvent.GetPage();
			int pageNumber = pdfDoc.GetPageNumber(currentPage);

			// 创建中文字体												  
			PdfFont font = PdfFontFactory.CreateFont("楷体_GB2312.ttf", PdfEncodings.IDENTITY_H);

			/*// 在文档底部添加当前页码
			pdfDoc.ShowTextAligned(new Paragraph("页码: " + pageNumber),
				currentPage.GetPageSize().GetWidth() / 2, 20,
				pageNumber, TextAlignment.CENTER, VerticalAlignment.BOTTOM, 0);*/
		}

	}
}
