using iText.Barcodes;
using iText.IO.Font;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFStudio
{
	public class PageEventHandler : IEventHandler
	{
		public PageEventHandler()
		{

		}

		public void HandleEvent(Event @event)
		{


			PdfDocumentEvent documentEvent = (PdfDocumentEvent)@event;
			PdfDocument pdfDoc = documentEvent.GetDocument();
			PdfPage page = documentEvent.GetPage();

			int currentPage = pdfDoc.GetPageNumber(documentEvent.GetPage());
			int totalPage = pdfDoc.GetNumberOfPages();
			// 创建中文字体												  
			PdfFont font = PdfFontFactory.CreateFont("楷体_GB2312.ttf", PdfEncodings.IDENTITY_H);

			Barcode128 barcode = new Barcode128(pdfDoc);
			barcode.SetFont(font);
			barcode.SetCode("hhxxttxs");
			BarcodeQRCode qrCode = new BarcodeQRCode("hhxxttxs");

			PdfCanvas canvas = new PdfCanvas(documentEvent.GetPage());

			//页面尺寸
			float pageWidth = page.GetPageSize().GetWidth();
			float pageHeight = page.GetPageSize().GetHeight();

			//最后一页绘制文本
			if (currentPage == totalPage)
			{
				float height = 60;
				canvas.BeginText()
				.SetFontAndSize(font, 12)
				.MoveText(pageWidth - 150, height)
				.ShowText("签名：")

				.EndText();

				canvas.BeginText()
				.MoveText(36, height)
				.ShowText("档案室签名：");
			}
			/*canvas.BeginText()
				.SetFontAndSize(font, 12)
				.MoveText(500, 20)
				.ShowText("第" + currentPage + "页  共" + totalPage + "页")
				.EndText();*/


			// 绘制二维码
			Image qrCodeImage = new Image(qrCode.CreateFormXObject(pdfDoc))
				.SetWidth(UnitValue.CreatePointValue(30))
				.SetHeight(UnitValue.CreatePointValue(30));
			new Canvas(canvas, page.GetPageSize())
				.Add(qrCodeImage.SetFixedPosition(pageWidth - qrCodeImage.GetImageWidth() - 36
				, pageHeight - qrCodeImage.GetImageWidth() - 10));

			// 绘制条形码
			Image barcodeImage = new Image(barcode.CreateFormXObject(pdfDoc))
				.SetHeight(UnitValue.CreatePointValue(30))
				.SetWidth(UnitValue.CreatePointValue(90));
				//.SetBackgroundColor(ColorConstants.GRAY);
			new Canvas(canvas, page.GetPageSize())
				.Add(barcodeImage.SetFixedPosition(36, 10));
		}
	}
}