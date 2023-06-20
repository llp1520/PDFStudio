using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Events;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDFStudio.EventHandler
{

	public class AddWaterMarkEventHandler : IEventHandler
	{

		private readonly Form _enclosing;

		public AddWaterMarkEventHandler(Form enclosing)
		{
			_enclosing = enclosing;
		}
		public virtual void HandleEvent(Event @event)
		{
			PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
			PdfDocument pdfDoc = docEvent.GetDocument();
			PdfPage page = docEvent.GetPage();
			int pageNumber = pdfDoc.GetPageNumber(page);
			Rectangle pageSize = page.GetPageSize();
			PdfCanvas pdfCanvas = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), pdfDoc);//此方式，水印在内容下层，会被文本内容遮挡
																											//Set background
			Color limeColor = new DeviceCmyk(0.208f, 0, 0.584f, 0);
			Color blueColor = new DeviceCmyk(0.445f, 0.0546f, 0, 0.0667f);
			var helveticaFont = iText.Kernel.Font.PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
			var helveticaBoldFont = iText.Kernel.Font.PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

			pdfCanvas.SaveState()
						.SetFillColor(pageNumber % 2 == 1 ? limeColor : blueColor)
						.Rectangle(pageSize.GetLeft(), pageSize.GetBottom(), pageSize.GetWidth(), pageSize.GetHeight())
						.Fill()
						.RestoreState();

			//Add header and footer
			pdfCanvas.BeginText()
						.SetFontAndSize(helveticaFont, 9)
						.MoveText(pageSize.GetWidth() / 2 - 60, pageSize.GetTop() - 20)
						.ShowText("THE TRUTH IS OUT THERE")
						.MoveText(60, -pageSize.GetTop() + 30)
						.ShowText(pageNumber.ToString())
						.EndText();

			PdfCanvas overCanvas = new PdfCanvas(page);//水印在内容上面，不会被遮挡
													   //Add watermark
			Color whiteColor = new DeviceRgb(System.Drawing.Color.White.R, System.Drawing.Color.White.G, System.Drawing.Color.White.B);
			Color blackColor = new DeviceRgb(System.Drawing.Color.Black.R, System.Drawing.Color.Black.G, System.Drawing.Color.Black.B);

			//方式1
			//iText.Layout.Canvas canvas = new iText.Layout.Canvas(overCanvas, page.GetPageSize());
			//UnitValue fontSize = new UnitValue(UnitValue.POINT, 60);
			//TransparentColor fontColor = new TransparentColor(whiteColor, 0.8f);
			////不能使用注释掉的方式(注释内容是入门教程中的写法)去设置文本的样式，不然在ShowTextAligned中会报转换异常
			//canvas.SetProperty(Property.FONT_COLOR, fontColor);//canvas.SetProperty(Property.FONT_COLOR, whiteColor);
			//canvas.SetProperty(Property.FONT_SIZE, fontSize);//canvas.SetProperty(Property.FONT_SIZE, 60);
			//canvas.SetProperty(Property.FONT, helveticaBoldFont);
			//canvas.ShowTextAligned(new Paragraph("CONFIDENTIAL"), 298, 421, pdfDoc.GetPageNumber(page), TextAlignment.CENTER, VerticalAlignment.MIDDLE, 45);
			//canvas.Close();

			//方式2 推荐方式2，书写更简单
			//overCanvas.SaveState();
			//Paragraph paragraph = new Paragraph("CONFIDENTIAL")
			//        .SetFont(helveticaBoldFont)
			//        .SetFontSize(60)
			//        .SetFontColor(whiteColor)
			//        .SetOpacity(0.8f);
			////直接给Paragraph设置透明度比下面这种方式更简单

			////PdfExtGState gs1 = new PdfExtGState();
			////gs1.SetFillOpacity(0.5f);//设置透明度
			////overCanvas.SetExtGState(gs1);
			//Canvas canvasWatermark1 = new Canvas(overCanvas, pdfDoc.GetDefaultPageSize())
			//        .ShowTextAligned(paragraph, 298, 421, pdfDoc.GetPageNumber(page), TextAlignment.CENTER, VerticalAlignment.MIDDLE, 45);
			//canvasWatermark1.Close();
			//overCanvas.RestoreState();

			pdfCanvas.Release();
		}

	}
}
