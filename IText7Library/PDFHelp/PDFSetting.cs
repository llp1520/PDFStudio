using iText.IO.Font;
using iText.Kernel.Font;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace IText7Library.PDFHelp
{
	public static  class PDFSettings
	{
		/// <summary>
		/// 生成的PDF名称 默认为PDF.pdf
		/// </summary>
		public static string DocumentName { get; set; } = "PDF.pdf";

		/// <summary>
		/// 文档保存路径 默认为空即根目录
		/// </summary>
        public static string? SavePath { get; set; }

		/// <summary>
		/// 设置文档字体 默认为根目录下的"楷体_GB2312.ttf"
		/// </summary>
		public static string FontPath { get; set; } = "楷体_GB2312.ttf";

		/// <summary>
		/// PDF表格的表头 PDF的每一页都将显示该内容
		/// </summary>
		public static string? Title { get; set; }

		/// <summary>
		/// PDF表格列名与对于数据表的字段名 key未数据表的字段名 value为表格列名
		/// </summary>
		public static Dictionary<string, string>? MapTableColumnName { get; set; }

		
    }
}
