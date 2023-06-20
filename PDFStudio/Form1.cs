using iText.Barcodes;
using iText.Forms;
using iText.Forms.Fields;
using iText.IO.Font;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Xobject;
using iText.Kernel.Utils;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Layout;
using iText.Layout.Properties;
using iText.Layout.Renderer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iText.Layout.Font;
using DevExpress.Drawing.Internal.Fonts;
using System.Linq;
using iText.Kernel.Pdf.Canvas.Parser.Data;

namespace PDFStudio
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();



        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string path = "E:/Study/pdf/QuestPDF.pdf";
            string path2 = "E:/Study/pdf/表单字段.pdf";
            string outputPath = "E:/Study/pdf/output.pdf";
            PdfReader reader = new PdfReader(path2);
            PdfWriter writer = new PdfWriter(outputPath);
            //PdfDocument pdfDoc = new PdfDocument(reader);
            PdfDocument pdfDoc = new PdfDocument(reader, writer);

            // 创建PdfAcroForm对象并关联到PdfDocument
            PdfAcroForm form = PdfAcroForm.GetAcroForm(pdfDoc, true);

            // 获取表单域并填充表单（根据之前的示例进行操作）

            // 获取表单域列表
            IDictionary<string, PdfFormField> fields = form.GetAllFormFields();


            string fontPath = "E:\\媒体资源\\字体\\仿宋_GB2312、楷体_GB2312、方正小标宋简体\\新建文件夹\\楷体_GB2312.ttf";
            PdfFont font = PdfFontFactory.CreateFont(fontPath, PdfEncodings.IDENTITY_H);

            fields["name"].SetValue("张三").SetFont(font).SetFontSize(10);
            fields["age"].SetValue("18").SetFont(font).SetFontSize(10);
            fields["tel"].SetValue("188888888").SetFont(font).SetFontSize(10);
            fields["sex"].SetValue("男").SetFont(font).SetFontSize(10);
            fields["height"].SetValue("188").SetFont(font).SetFontSize(10);
            fields["weight"].SetValue("88KG").SetFont(font).SetFontSize(10);
            fields["education"].SetValue("博士").SetFont(font).SetFontSize(10);
            //fields["addr"].IsMultiline=t
            fields["addr"].SetValue("中国江苏无锡梁溪区中国江苏无锡中国江苏无锡中国江苏无锡梁溪区中国江苏无锡中国江苏无锡中国江苏无锡" +
                "梁溪区中国江苏无锡中国江苏无锡中国江苏无锡梁溪区中国江苏无锡中国江苏无锡中国江苏无锡梁溪区中国江苏无锡中国江苏无锡中国" +
                "江苏无锡梁溪区中国江苏无锡中国江苏无锡中国江苏无锡中国江苏无锡中国江苏无锡中国江苏无锡中国江苏无锡中国江苏无锡中国江苏" +
                "无锡中国江苏无锡中国江苏无锡中国江苏无锡中国江苏无锡中国江苏无锡中国江苏无锡中国江苏无锡中国江苏无锡中国江苏无锡中国江" +
                "苏无锡中国江苏无锡中国江苏无锡中国江苏无锡中国江苏无锡中国江苏无锡中国江苏无锡中国江苏无锡中国江苏无锡中国江苏无锡中国" +
                "江苏无锡中国江苏无锡中国江苏无锡中国江苏无锡中国江苏无锡中国江苏无锡").SetFont(font);


            //fields["addr"].SetValue("中国江苏无锡中国江苏无锡中国江苏无锡中国江苏无锡").SetFont(font).SetFontSizeAutoScale();

            form.FlattenFields(); // 将表单字段内容写入 PDF 页面



            // 保存修改后的PDF文档
            pdfDoc.Close();
        }

        private void btn_LoadPdfFile_Click(object sender, EventArgs e)
        {
            /*OpenFileDialog openFileDialog = new OpenFileDialog();
			// 配置OpenFileDialog的属性
			openFileDialog.Title = "选择PDF文件";
			openFileDialog.Filter = "PDF文件|*.pdf|文本文件|*.txt|所有文件|*.*";
			// 显示文件对话框
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				// 获取选中的文件路径
				path = openFileDialog.FileName;

				// 在此处执行您希望在选中文件后执行的操作
				pdfViewer.DocumentFilePath = path;
			}*/


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            //string inputPath = "E:/Study/pdf/分页测试表单 - 副本.pdf";
            //string outputPath = "E:/Study/pdf/output1.pdf";

            //SqlHelper.ConnStr = "server=.;database=AdventureWorks2019;uid=sa;pwd=hhxxttxs123;";
            //DataTable dataTable = await SqlHelper.ExecuteQueryAsync("SELECT * FROM [Person].[PersonPhone]");
            //DataTable dataTable = SqlHelper.ExecuteTable("SELECT TOP 2 * FROM [Person].[PersonPhone]");
            //await Task.Run(() => CreatePdfFromTemplate(inputPath, outputPath, dataTable));


            SqlHelper.ConnStr = "server=.;uid=sa;pwd=hhxxttxs123;";
            string sqlPath = "E:/Study/pdf/sql语句.txt";
            // 使用UTF-8编码读取文件内容，如果文件采用其他编码，请相应地调整
            string sqlText = File.ReadAllText(sqlPath, Encoding.UTF8);
            string outputPath = "E:/Study/pdf/tableOutput.pdf";
            string savePath = "E:/Study/pdf/save.pdf";
            DataTable dt = SqlHelper.ExecuteTable(sqlText);

            int blankRowCount = 0;
            float blankCellHeight = 30;
            CreatePdfTable(outputPath, dt, blankRowCount, blankCellHeight);

            int pageCount = CreatePageInfo(outputPath, savePath);
            int tempPageCount = pageCount;
            while (pageCount == tempPageCount)
            {
                blankRowCount++;
                CreatePdfTable(outputPath, dt, blankRowCount, blankCellHeight);
                tempPageCount = CreatePageInfo(outputPath, savePath);
            }

            CreatePdfTable(outputPath, dt, blankRowCount - 2, blankCellHeight);
            CreatePageInfo(outputPath, savePath);




            pdfViewer.CloseDocument();
            File.Copy(savePath, "openPDF.pdf", true);
            pdfViewer.DocumentFilePath = "openPDF.pdf";


            /*string outputPath = "E:/Study/pdf/layout.pdf";
			CreateDocumentWithLayout(outputPath);*/


            Cursor = Cursors.Default;
        }

        public void CreateDocumentWithLayout(string outputPath)
        {
            PdfWriter writer = new PdfWriter(outputPath);
            PdfDocument pdfDoc = new PdfDocument(writer);
            Document document = new Document(pdfDoc);
            // 设置页面的边距
            document.SetMargins(10, 20, 20, 20);

            // 创建页面布局的根元素
            /*Rectangle[] columns = { new Rectangle(36, 36, 500, 750) };
			var renderer = new ColumnDocumentRenderer(document, columns);
			document.SetRenderer(renderer);*/

            // 创建中文字体												  
            PdfFont font = PdfFontFactory.CreateFont("楷体_GB2312.ttf", PdfEncodings.IDENTITY_H);

            document.SetFont(font);

            Div divTop = new Div();
            divTop.SetWidth(UnitValue.CreatePercentValue(100))
                  .SetHeight(UnitValue.CreatePercentValue(100));

            // 创建包含二维码和条形码的 DIV 元素
            Div barcodeContainer = new Div();
            barcodeContainer.SetProperty(Property.FLOAT, FloatPropertyValue.RIGHT);

            // 创建二维码并添加到 DIV 元素中
            BarcodeQRCode qrCode = new BarcodeQRCode("hhxxttxs");
            Image qrCodeImage = new Image(qrCode.CreateFormXObject(pdfDoc));
            barcodeContainer.Add(qrCodeImage);

            // 创建条形码并添加到 DIV 元素中
            Barcode128 barcode = new Barcode128(pdfDoc);
            barcode.SetCode("abcd1234");
            Image barcodeImage = new Image(barcode.CreateFormXObject(pdfDoc));

            // 设置 DIV 元素的位置和边距
            //barcodeContainer.SetFixedPosition(100, 700, 100);
            barcodeContainer.SetMargins(0, 0, 0, 0);


            // 将 DIV 元素添加到文档
            divTop.Add(barcodeContainer);

            // 创建包含二维码和条形码的 DIV 元素
            Div div2 = new Div();

            div2.Add(barcodeImage);

            divTop.Add(div2);

            document.Add(divTop);
            document.Add(new Paragraph("黄巾起义\r\n东汉建宁元年（168年）九月，宦官曹节、王甫等人专横，大将军窦武与太傅陈蕃因憎恶而意图除害，不料事迹败露，反被消灭[7]:34。汉桓帝、汉灵帝昏庸无道，宠信宦官，疏远良臣，致使朝政腐败，百姓生活困苦。灵帝时，张角自称曾遇见仙人，幸得天书，学得有效治病方法。后来他云游四方给人治病（他治病的方法带有宗教色彩，但在某些因素的作用下，这使他声名远播），借此聚众起事，欲图推翻东汉，爆发农民大暴动——黄巾起义。\r\n\r\n在大将军何进带领军队剿灭了黄巾军之后，又发生了十常侍之乱。这时汉灵帝已故，汉少帝刘辩即位。何进欲杀皇宫里所有宦官，反被宦官诱杀。袁绍和曹操带领众大臣冲入皇宫，杀绝宦官，还误杀不少不留胡须的男人。少帝与陈留王刘协于慌乱中逃出皇宫。\r\n\r\n董卓之乱\r\n各路诸侯分头寻找，最终由凉州刺史董卓找到少帝与陈留王。董卓自恃救驾有功，趁机专权弄政，废少帝，改立陈留王为汉献帝。董卓横行，残害忠良，引起大臣愤怒。曹操行刺董卓失败而逃走，假传圣旨，召各路诸侯结盟勤王，反抗董卓。斩华雄，败吕布，十八路诸侯在袁绍率领下杀向首都洛阳，迫使董卓劫持皇帝，迁都长安。董卓后来与其义子吕布争美女貂蝉。吕布在王允劝诱下，杀了董卓。\r\n\r\n群雄逐鹿\r\n长沙太守孙坚于洛阳发现了传国玉玺，欲私藏起来。袁绍大怒，逼孙坚交出玉玺。孙坚不肯，逃回长沙，途中遭荆州刘表袭击。孙坚为此与刘表结怨，后发兵进攻荆州，不料死于战中。在河北，袁绍与公孙瓒互争土地，爆发界桥之战，最后董卓假天子诏书和解才停战。同时，各路诸侯如刘备等正在建立起自己的实力。曹操广招贤才，建立起一支强悍军队，准备霸占中原。\r\n\r\n统一中原\r\n曹操奉迎汉献帝，拥护汉室，于许昌建新都，“挟天子以令诸侯”，剿灭中原群雄如吕布、张绣、袁术等，日渐强大，称霸中原。后来与袁绍在官渡之战中，以寡敌众，大败袁绍。曹操继续挥军北上，灭袁氏、败乌桓、平辽东，统一北方。曹操开疆拓土，建立日后魏国的基础。\r\n\r\n孙策立业\r\n孙策自父亲孙坚战死后，欲重振家业，称霸江东。他拿父亲的遗物传国玉玺，与淮南袁术交换兵马。孙策掌握兵马，又有追随先父的老将、结拜兄弟周瑜及智谋之士帮助，发兵江东，经多年苦战，终于称霸，坐镇江东，为后来建立吴国打好基础。不料，孙策遇刺重伤身亡，由弟弟孙权继承基业。孙权有周瑜、张昭等能臣勇将扶持，稳坐江东，势力逐渐强大。\r\n\r\n刘备扶汉\r\n\r\n颐和园长廊彩绘中的三英战吕布\r\n刘备与关羽、张飞曾在桃园结义时立下誓言，忠心辅助汉室，拯救天下苍生。刘备讨伐黄巾有功，后得到县令官职。后来刘备投奔公孙瓒，就任平原太守，曾相约讨伐董卓，于虎牢关前与关、张二人大战吕布。曹操之父曹嵩被徐州刺史陶谦部下张闿所杀，起兵攻打徐州雪耻。陶谦求助，刘备来到徐州协助陶谦抵挡曹操大军。曹操退兵后，陶谦去世前将徐州交与刘备，刘备就任徐州牧。吕布被曹操打败后，逃到徐州投奔刘备。刘备慷慨收留吕布，后来张飞惹怒吕布，吕布反夺徐州。袁术率兵打刘备，吕布便以辕门射戟的方式救了刘备，刘备与曹操联手消灭吕布。刘备同朝廷大臣密谋除掉专权欲篡位的曹操，不料事情暴露，刘备又叛曹操，斩杀曹将车胄夺徐州。刘备在徐州被曹操打败，逃去投奔袁绍，又叛袁绍，后在汝南建立实力。曹操再次于汝南大败刘备，迫使刘备逃往荆州投靠刘表。刘表让刘备镇守新野，以抵抗将要南征的曹操。刘备三顾茅庐，得到足智多谋的诸葛亮辅佐，如鱼得水。\r\n\r\n赤壁之战\r\n\r\n颐和园长廊彩绘的江东赴会情节\r\n曹操自封丞相，统一北方后，举兵南征荆州。刘备于新野两度杀退曹军，但彼众我寡，只得放弃城池，被迫南退荆州。刘表已故，荆州由幼子刘琮接管，长子刘琦留守江夏。刘备携民渡江，来到襄阳城外，求刘琮让他进城，被刘琮拒绝。刘琮后来投降曹操，荆州落入曹操手中，刘琮也被杀。刘备不得已南下到江夏投靠刘琦，途中遭曹军追杀，百姓死伤无数，虽然途中曾有很多谋士劝他放弃军民（百姓），但仁义的刘备说什么就是不肯放弃他们。\r\n\r\n为抵挡曹操凶猛的来势，刘备派遣诸葛亮前往江东说服孙权结盟抗曹。诸葛亮舌战群儒、智激孙权、庞统巧使连环计、孔明巧借东风、周瑜赤壁纵火，黄盖使苦肉计。周瑜不容诸葛亮在吴国，多次想加害诸葛亮，但都没成功。其中最出名的就是草船借箭。周瑜暂时放下杀诸葛亮的念头，专心策划对付曹操。最终，刘备与孙权的联军于赤壁之战中用火攻大败曹操，大获全胜，曹操败走华容。\r\n\r\n荆州争夺\r\n赤壁大战后，孙权、刘备相争荆州。周瑜领兵与曹军大战，取胜后以为自己已经攻下荆州，不料曹军中诸葛亮调虎离山之计，荆州落入刘备手中，吴军苦战一无所获。孙权不满，派遣鲁肃前往荆州向刘备讨还荆州。刘备多次在诸葛亮的劝说下推辞交出荆州，说等刘琦死后交出荆州。刘琦病逝后，刘备又说取得容身之地后再交出荆州。孙权、周瑜无奈，多次想办法夺取荆州。周瑜使美人计，欲骗刘备到东吴娶孙权之妹为妻，实为扣留刘备在东吴，逼诸葛亮交出荆州以交换主公。东吴招亲，弄假成真，刘备不但取得孙权之妹为妻，还安全地返回荆州，结果蜀国的士兵都说：“周郎妙计安天下，赔了夫人又折兵。”周瑜使出计谋屡屡被诸葛亮识破，未得成功。最终，诸葛亮三气周瑜，周瑜呕血而死，临死前还大喊三声：“既生瑜，何生亮！”\r\n\r\n马超归汉\r\n在西北方，猛将马超为被曹操杀害的父亲马腾报仇，联合韩遂、羌族兴兵讨伐曹操。马超于潼关、渭水之战中大败曹操，曹操割须弃袍、仓惶逃脱。曹操采纳谋士贾诩离间之计，成功地使马超、韩遂二人互相猜疑，最后大动干戈。马超被曹操打败，逃去投奔汉中张鲁，韩遂归降曹操。过后，刘备欲要攻打西川，刘璋向张鲁求救，张鲁派马超去葭萌关攻打刘备，刘备人马派出张飞抵御马超。结果马超和张飞挑灯夜战大打三百多回合。刘备把这一切看在眼里，很惜才的刘备心里很想马超纳入自己的军队，所以诸葛亮又使了离间计让马超最终投降刘备，成为五虎上将之一。\r\n\r\n刘备称帝\r\n周瑜死后，东吴与刘备关系恶劣，但未大动干戈。吴军与曹军开战，在濡须、合淝之战中有胜有败。在荆州，诸葛亮劝说刘备发兵进攻西川，夺取巴蜀。刘备打败刘璋，夺得西川，又从曹操手中夺取汉中。刘备自封汉中王，拥有益州全部和部分荆州，为建立蜀汉打好基础。至此，天下鼎足而三：刘备跨有荆、益；曹操称霸中原，进占关、陇；孙权坐镇江东。后来曹操病死，次子曹丕篡汉，东汉历二百余年而亡。曹丕改国号为魏，刘备闻讯，在巴蜀称帝，国号仍为汉，史称蜀汉。\r\n\r\n关张之死\r\n孙权仍然一心要荆州，决定发兵取荆州，并在与刘备谈判后得到江夏、桂阳、长沙。关羽攻打曹仁把守的樊城时，孙权与曹操联合，派遣大将吕蒙攻打荆州，吕蒙白衣渡江，混入荆州。关羽得知中了调虎离山之计，不得已退守麦城，并派人前往成都求刘备派援兵。吴军包围麦城，将关羽困在城中。关羽突围，中伏被擒。关羽被押往东吴，宁死不愿投降孙权，被孙权斩首。吕蒙因功受赏，但在酒席上突然被关羽灵魂附体，随即暴死。曹操封孙权为南昌侯。刘备痛失荆州、义弟关羽，不禁深感悲愤，欲起兵攻打东吴为关羽复仇。不料，张飞在睡梦中被部下范疆、张达暗杀，两人逃往东吴。刘备因过度悲伤而昏迷过去，醒后不听诸葛亮劝阻，一意孤行要为关羽复仇。\r\n\r\n夷陵之战\r\n刘备大军一路势如破竹，连败吴军，杀死关羽仇人潘璋、马忠、麋芳、傅士仁。孙权将张飞仇人范疆、张达交刘备处死，但刘备仍然怒气未息要灭吴。情急之中的孙权用人不疑，拜书生陆逊为大都督，号令东吴三军。刘备因连胜而大意，把大军布阵于树林中，犯下兵家大忌。陆逊以火攻，火烧连营七百里，蜀军大败。陆逊因深怕曹丕会趁吴军追杀惨败的蜀军时，发兵攻打东吴，因此放弃追击蜀军。果不出陆逊所料，魏军果然进攻东吴，反被吴军所败。\r\n\r\n刘备败军抵达白帝城，刘备因过度悲伤而病倒，命垂旦夕。临终前，托孤于诸葛亮，说如若儿子刘禅昏庸无能，诸葛亮可废掉刘禅，自立皇帝。诸葛亮誓言要忠心报答刘备的知遇之恩，尽心协力辅佐刘禅匡复汉室。\r\n\r\n七擒孟获\r\n曹丕趁机联合四路兵马杀向蜀汉，包括东吴、南蛮、羌族与孟达。诸葛亮派马超把守关口，使羌兵不敢进攻；派魏延制造疑兵，使南蛮多疑不敢进攻；派孟达至交李严写信，劝孟达退兵；派赵云把守关口，抵御魏军。此四路兵马不期而退，只剩东吴一路孤军，只得退兵。最后，诸葛亮派善言语的邓芝前往东吴，成功说服孙权再度与蜀汉和好结盟，共抗曹魏。\r\n\r\n南蛮王孟获反叛，诸葛亮亲领大军前往不毛之地平定南蛮。诸葛亮七擒七纵孟获，使孟获深感懊悔，决心永远归降蜀汉。\r\n\r\n六出祁山\r\n曹丕病逝，由其子曹叡继位。诸葛亮一出祁山，原本将直逼长安，因马谡丢失街亭而后败北，二次北伐粮尽而退，孙权于东吴称帝，国号吴。在蜀汉，诸葛亮上奏刘禅出师第三次讨伐魏国，决心完成刘备匡复汉室的遗愿，数次击败魏军及击杀大将张郃，但年数将尽，而且蜀汉之实力不足以讨伐魏国。诸葛亮决定将平生所学授予北伐期间收降的将才姜维。诸葛亮与魏司马懿斗智，双方僵持已久。最终，诸葛亮因过于操劳，在五丈原病逝。临终前，诸葛亮准备一樽木雕像吓跑以为诸葛亮未死的司马懿，争取时间让蜀军撤退。\r\n\r\n诸葛亮死后，大将魏延不满诸葛亮的身后安排，意图夺取军权继续北伐，更不惜烧毁栈道拦截自家军队归路及攻打南郑，但诸葛亮生前早有布置，魏延被杀。\r\n\r\n司马篡魏\r\n在魏国，曹睿死后曹芳继位。司马懿发动高平陵政变，夺取了掌握大权的曹爽的兵权，曹爽被杀。司马懿病死后，其子司马师、司马昭继续掌握魏国大权，常有篡位的野心。司马兄弟废了曹芳，立曹髦为帝。司马师因患眼疾病故，司马昭独揽大权。曹髦欲除国贼司马昭，不料被司马昭部下成济杀害，司马昭又假装悲伤，将弑君之罪名推到成济身上，将成济斩首。司马昭后立曹奂为帝。\r\n\r\n三国归晋\r\n姜维继承诸葛亮遗愿，继续兴兵与魏国作战。后主刘禅昏庸，听信谗言，导致姜维失败，后又废了姜维兵权。姜维为逃避奸臣加害，屯田沓中。魏将邓艾乘蜀汉大乱，发兵进攻。邓艾大军翻山越岭，终于抵达成都城前。刘禅不战而降，蜀汉灭亡。姜维为复兴蜀汉，乘魏将锺会与邓艾不合，欲联合除掉邓艾，谋划重建蜀汉。锺会欲除掉司马昭，不料事情被揭发，司马昭派兵围攻锺会、姜维。锺会死于乱箭之下、姜维身负重伤，感叹无法完成诸葛亮遗愿，便拔剑自刎。\r\n\r\n在东吴，孙权死后，吴宫内乱。诸葛恪专权，被孙峻所杀。孙峻和孙𬘭先后独揽大权，孙𬘭存篡位之心。并废了吴主孙亮，改立孙休为帝。孙休常怀有攘除国贼孙𬘭之心，联合东吴老将丁奉除掉孙𬘭，夺回大权。不过，东吴安宁并不长久。\r\n\r\n在魏国，司马昭之子司马炎受禅篡位称帝，改国号为晋，史称西晋，魏国灭亡。西晋起用老将杜预进攻东吴，经一场恶战后打败吴军。吴主孙皓投降，东吴灭亡。经过近百年战乱，三国时代终结。司马氏三分归一，建立西晋统一天下。"));





            document.Close();
        }



        /// <summary>
        /// 重新打开带表格的PDF 添加页码等信息
        /// </summary>
        /// <param name="fielPath"></param>
        /// <param name="savePath"></param>
        public static int CreatePageInfo(string fielPath, string savePath)
        {
            // 创建中文字体												  
            PdfFont font = PdfFontFactory.CreateFont("楷体_GB2312.ttf", PdfEncodings.IDENTITY_H);
            PdfDocument pdfDocument = new PdfDocument(new PdfReader(fielPath), new PdfWriter(savePath));
            int totalPage = pdfDocument.GetNumberOfPages();
            Document document = new Document(pdfDocument);
            FontProvider fontProvider = new FontProvider("楷体_GB2312.ttf");
            document.SetFontProvider(fontProvider);
            for (int i = 1; i <= totalPage; i++)
            {
                PdfPage page = pdfDocument.GetPage(i);
                if (i == totalPage)
                {

                    //获取文本内容
                    string pdfContentString = PdfTextExtractor.GetTextFromPage(page);
                    string[] ontent = pdfContentString.Split(' ');
                    //MessageBox.Show(pdfContentString);
                    /*Table table = new Table();
					table.SetWidth(UnitValue.CreatePercentValue(100)); // 设置表格宽度为100%
					table.SetHeight(UnitValue.CreatePercentValue(100)); // 设置表格宽度为100%
					table.SetFont(font);
					table.SetTextAlignment(TextAlignment.CENTER);*/
                }

                float pageWidth = page.GetPageSize().GetWidth();
                string text = "第" + i + "页  共" + totalPage + "页";
                float fontSize = 12;
                float textWidth = font.GetWidth(text) * fontSize / 1000;//获取字体的宽度
                PdfCanvas canvas = new PdfCanvas(page);
                canvas.BeginText()
                .SetFontAndSize(font, fontSize)
                .MoveText((pageWidth - textWidth) / 2, 20)//设置字体的位置
                .ShowText(text)
                .EndText();


            }
            pdfDocument.Close();
            return totalPage;
        }




        /// <summary>
        /// 创建带表格的PDF
        /// </summary>
        /// <param name="outputPath"></param>
        /// <param name="dataTable"></param>
        public static void CreatePdfTable(string outputPath, DataTable dataTable, int blankRowCount, float blankCellHeight)
        {
            // 创建PDF文档
            PdfWriter writer = new PdfWriter(outputPath);
            PdfDocument pdfDoc = new PdfDocument(writer);
            Document document = new Document(pdfDoc);
            //document.set
            document.SetMargins(20, 36, 50, 36);

            //添加页码事件  暂时不用此方法 已经用重新打开文档方法实现
            //PageXofY pageEvent = new PageXofY();
            //pdfDoc.AddEventHandler(PdfDocumentEvent.END_PAGE, pageEvent);
            //pdfDoc.AddEventHandler(PdfDocumentEvent.END_PAGE, new PageEventHandler());


            // 创建中文字体												  
            PdfFont font = PdfFontFactory.CreateFont("楷体_GB2312.ttf", PdfEncodings.IDENTITY_H);

            // 创建表格
            Table table = new Table(dataTable.Columns.Count);
            table.SetWidth(UnitValue.CreatePercentValue(100)); // 设置表格宽度为100%
            table.SetHeight(UnitValue.CreatePercentValue(100)); // 设置表格宽度为100%
            table.SetFont(font);
            //table.SetTextAlignment(TextAlignment.CENTER);


            //添加条码绘制事件
            PageEventHandler pageEvent = new PageEventHandler();
            pdfDoc.AddEventHandler(PdfDocumentEvent.END_PAGE, pageEvent);

            //设置表格顶部样式
            Paragraph paragraph = new Paragraph("恒悦软件档案归还单").SetFontSize(20).SetBold().SetItalic();
            Cell topCell = new Cell(1, dataTable.Columns.Count)
                .SetBorder(Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(paragraph);
            table.AddHeaderCell(topCell);
            //对表字段进行文本替换
            var mapTableColumnName = new Dictionary<string, string>();

            mapTableColumnName.Add("ID", "编号");
            mapTableColumnName.Add("ArcType", "类别");
            mapTableColumnName.Add("ArcNO", "档号");
            mapTableColumnName.Add("Owner", "公司名称");
            mapTableColumnName.Add("HouseLocated", "住址");
            mapTableColumnName.Add("OldArcNO", "旧档号");

            // 添加表头
            foreach (DataColumn column in dataTable.Columns)
            {
                Cell headerCell = new Cell()
                    .SetTextAlignment(TextAlignment.CENTER)
                //.SetWidth(80)
                //.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                //.Add(new Paragraph(column.ColumnName));
                .Add(new Paragraph(mapTableColumnName[column.ColumnName]));
                headerCell.SetBackgroundColor(ColorConstants.LIGHT_GRAY);
                table.AddHeaderCell(headerCell);
            }
            int dataCount = 0;

            // 添加数据行
            foreach (DataRow row in dataTable.Rows)
            {
                dataCount++;
                foreach (DataColumn column in dataTable.Columns)
                {
                    // 设置单元格不允许拆分到两页SetKeepTogether(true)
                    Cell cell = new Cell();
                    cell.SetKeepTogether(true)//.SetKeepWithNext(true)
                                              //.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER)
                        .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                        .SetHeight(UnitValue.CreatePercentValue(0))
                        .SetTextAlignment(TextAlignment.CENTER)
                        //.SetHeight(UnitValue.CreatePointValue(0))
                        .SetWidth(UnitValue.CreatePercentValue(0))
                        .Add(new Paragraph(row[column].ToString()));
                    table.AddCell(cell);
                    //cellHeight=cell.GetHeight().GetValue();
                }
                // 获取表格的实际高度
                //float tableHeight = table.GetRenderer().GetOccupiedArea().GetBBox().GetHeight();

                if (dataCount == dataTable.Rows.Count)
                {
                    int index = 0;
                    for (int i = 0; i < blankRowCount; i++)
                    {
                        index++;
                        foreach (DataColumn column in dataTable.Columns)
                        {
                            Cell cell = new Cell().SetHeight(UnitValue.CreatePointValue(blankCellHeight));
                            if (column.ColumnName.Equals("ID"))
                            {
                                int tempIndex = int.Parse(dataTable.Rows[dataTable.Rows.Count - 1]["ID"].ToString());
                                tempIndex += index;
                                cell.Add(new Paragraph(tempIndex.ToString()));
                            }
                            else
                            {
                                cell.Add(new Paragraph(""));
                            }
                            table.AddCell(cell);
                            //cellHeight=cell.GetHeight().GetValue();
                        }
                    }
                }

            }
            document.Add(table);

            // 关闭文档
            pdfDoc.Close();

        }


        /// <summary>
        /// 解析并填充数据表，输出PDF 一条数据填充一个模板
        /// </summary>
        /// <param name="templatePath">具有表单的PDF路径</param>
        /// <param name="outputPath">输出填充内容后的PDF</param>
        /// <param name="dataTable">要填充的数据表表</param>
        public static void CreatePdfFromTemplate(string templatePath, string outputPath, DataTable dataTable)
        {
            string tempPath = "temp.pdf";

            //以写入方式创建输出文档
            PdfDocument outputDoc = new PdfDocument(new PdfWriter(outputPath));//可写outputPath

            //读取模板文档 写入临时文档
            PdfDocument templateDoc = new PdfDocument(new PdfReader(templatePath), new PdfWriter(tempPath));//可写tempPath

            //获取模板文档的表单 
            PdfAcroForm form = PdfAcroForm.GetAcroForm(templateDoc, true);

            //遍历数据表
            for (int rowIndex = 0; rowIndex < dataTable.Rows.Count; rowIndex++)
            {
                DataRow dr = dataTable.Rows[rowIndex];

                if (templateDoc.IsClosed())
                {
                    templateDoc = new PdfDocument(new PdfReader(templatePath), new PdfWriter(tempPath));
                    form = PdfAcroForm.GetAcroForm(templateDoc, true);
                }

                //遍历数据列
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
                //去除表单
                form.FlattenFields();

                //关闭读写操作 不然后面无法复制
                templateDoc.Close();

                //以读操作打开临时文档进行复制
                PdfDocument tempDoc = new PdfDocument(new PdfReader(tempPath));
                int pageCount = tempDoc.GetNumberOfPages();
                for (int i = 1; i <= pageCount; i++)
                {
                    PdfPage page = tempDoc.GetPage(i).CopyTo(outputDoc);
                    outputDoc.AddPage(page);//将页加到指定位置
                }
                tempDoc.Close();
            }
            File.Delete(tempPath);
            // 保存修改后的PDF文档
            outputDoc.Close();
        }


    }
}
