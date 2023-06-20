using DevExpress.CodeParser;
using iText.IO.Font;
using iText.IO.Font.Constants;
using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDFStudio
{
	public class PageXofY : IEventHandler
	{
		protected int total;
		protected PdfFont helv;
		public void SetTotal(int total)
		{
			this.total =total;
		}

		public void HandleEvent(iText.Kernel.Events.Event @event)
		{
			PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
			PdfDocument pdf = docEvent.GetDocument();
			PdfPage page = docEvent.GetPage();
			int pageNumber = pdf.GetPageNumber(page);
			Rectangle pageSize = page.GetPageSize();
			PdfCanvas pdfCanvas = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), pdf);
			//Add Current Page Number
			pdfCanvas.BeginText();
			helv = PdfFontFactory.CreateFont("楷体_GB2312.ttf", PdfEncodings.IDENTITY_H);
			pdfCanvas.SetFontAndSize(helv, 12);
			pdfCanvas.MoveText(pageSize.GetWidth() - 150, pageSize.GetBottom() + 20);
			pdfCanvas.ShowText("第 " + pageNumber+" 页 ");
			pdfCanvas.EndText();
			//Add Total Page Number
			pdfCanvas.BeginText();
			pdfCanvas.SetFontAndSize(helv, 12);
			pdfCanvas.MoveText(pageSize.GetWidth() - 100, pageSize.GetBottom() + 20);
			pdfCanvas.ShowText(" 共 "+total+" 页");
			pdfCanvas.EndText();
			pdfCanvas.Release();
		}
	}

}



