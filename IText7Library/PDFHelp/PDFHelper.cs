using iText.Forms;
using iText.Forms.Fields;
using iText.Forms.Form.Element;
using iText.IO.Font;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Utils;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IText7Library.PDFHelp
{
	public class PDFHelper
	{

		/// <summary>
		/// 解析并填充数据表，输出PDF 一条数据填充一个模板
		/// </summary>
		/// <param name="templatePath">具有表单的PDF路径</param>
		/// <param name="outputPath">输出填充内容后的PDF</param>
		/// <param name="dataTable">要填充的数据表表</param>
		public  void CreatePdfFromTemplate(string templatePath, string outputPath, DataTable dataTable)
		{
			string tempPath = "temp.pdf";

			// 以写入方式创建输出文档
			PdfDocument outputDoc = new PdfDocument(new PdfWriter(outputPath));

			// 读取模板文档写入临时文档
			PdfDocument templateDoc = new PdfDocument(new PdfReader(templatePath), new PdfWriter(tempPath));

			// 获取模板文档的表单
			PdfAcroForm form = PdfAcroForm.GetAcroForm(templateDoc, true);

			// 创建一个 PdfMerger 对象，用于合并页到输出文档
			PdfMerger merger = new PdfMerger(outputDoc);

			// 遍历数据表
			for (int rowIndex = 0; rowIndex < dataTable.Rows.Count; rowIndex++)
			{
				DataRow dr = dataTable.Rows[rowIndex];

				if (templateDoc.IsClosed())
				{
					templateDoc = new PdfDocument(new PdfReader(templatePath), new PdfWriter(tempPath));
					form = PdfAcroForm.GetAcroForm(templateDoc, true);
				}

				// 遍历数据列
				foreach (DataColumn dc in dataTable.Columns)
				{
					string fieldName = dc.ColumnName;
					if (form.GetField(fieldName) != null)
					{
						PdfFormField field = form.GetField(fieldName);
						field.SetFontAndSize(PdfFontFactory.CreateFont("楷体_GB2312.ttf"), 10);
						field.SetValue(dr[dc.ColumnName].ToString());
					}
					else
					{
						throw new KeyNotFoundException($"找不到字段：{fieldName}，请确保大小写一致。");
					}
				}

				// 去除表单
				form.FlattenFields();
				templateDoc.Close();

				// 将临时文档的所有页合并到输出文档
				PdfDocument tempDoc = new PdfDocument(new PdfReader(tempPath));
				merger.Merge(tempDoc, 1, tempDoc.GetNumberOfPages());
				tempDoc.Close();
			}

			File.Delete(tempPath);

			// 保存修改后的PDF文档
			outputDoc.Close();

		}


		/// <summary>
		/// 将数据表输出为PDF文档
		/// </summary>
		/// <param name="outputPath">输出路径/名称</param>
		/// <param name="dataTable">数据表</param>
		public  void CreatePdfFromDataTable(string outputPath, DataTable dataTable)
		{
			// 创建PDF文档
			PdfWriter writer = new PdfWriter(outputPath);
			PdfDocument pdf = new PdfDocument(writer);
			Document document = new Document(pdf);

			// 创建表格
			Table table = new Table(dataTable.Columns.Count); // 3列的表格
			table.SetWidth(UnitValue.CreatePercentValue(100)); // 设置表格宽度为100%

			// 创建中文字体												  
			PdfFont font = PdfFontFactory.CreateFont("楷体_GB2312.ttf", PdfEncodings.IDENTITY_H);

			// 设置表格字体
			table.SetFont(font);

			// 设置列头背景色
			Color headerBackgroundColor = new DeviceRgb(192, 192, 192);

			// 设置表格内容居中
			table.SetTextAlignment(TextAlignment.CENTER);
			table.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);

			// 添加标题
			string title = "恒悦软件档案归还单【单号：{参数}】";
			//跨列数为dataTable.Columns.Count
			Cell titleCell = new Cell(1, dataTable.Columns.Count).SetBorder(Border.NO_BORDER).Add(new Paragraph(title)).SetFontSize(18);
			table.AddHeaderCell(titleCell);

			// 添加表头
			foreach (DataColumn column in dataTable.Columns)
			{
				Cell headerCell = new Cell().Add(new Paragraph(column.ColumnName)).SetFontSize(15);
				headerCell.SetBackgroundColor(headerBackgroundColor);
				table.AddHeaderCell(headerCell);
			}

			// 添加数据行
			foreach (DataRow row in dataTable.Rows)
			{
				foreach (DataColumn column in dataTable.Columns)
				{
					// 设置单元格不允许拆分到两页
					Cell cell = new Cell().SetKeepTogether(true)
						.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER)
						.SetVerticalAlignment(VerticalAlignment.MIDDLE)
						.Add(new Paragraph(row[column].ToString())).SetFontSize(12);
					table.AddCell(cell);
				}
			}

			// 将表格添加到文档中
			document.Add(table);

			// 关闭文档
			pdf.Close();
		}

	}
}
