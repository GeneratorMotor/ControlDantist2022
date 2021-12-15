using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Word;
using System.IO;
using ControlDantist.ReportCountYear;

namespace ControlDantist.Reports
{
    public class ОстатокЛимитовОтчет : IPrintReport
    {
        private List<ШаблонДляПечати> list;

        private string nameFile = "ОтчетЛимитыОстаток";

        public ОстатокЛимитовОтчет(List<ШаблонДляПечати> list)
        {
            this.list = list ?? throw new ArgumentNullException(nameof(list));
        }

        public void Print()
        {
            if(File.Exists(System.Windows.Forms.Application.StartupPath + @"\Документы\" + nameFile + ".doc")== true)
            {
                File.Delete(System.Windows.Forms.Application.StartupPath + @"\Документы\" + nameFile + ".doc");
            }

            FileInfo fn = new FileInfo(System.Windows.Forms.Application.StartupPath + @"\Шаблон\ОтчетЛимитыОстаток.doc");
            fn.CopyTo(System.Windows.Forms.Application.StartupPath + @"\Документы\" + nameFile + ".doc", true);
            
            ////Создаём новый Word.Application
            Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();

            //Загружаем документ
            Microsoft.Office.Interop.Word.Document doc = null;

            object fileNameOb = System.Windows.Forms.Application.StartupPath + @"\Документы\" + nameFile + ".doc";
            object falseValue = false;
            object trueValue = true;
            object missing = Type.Missing;

            doc = app.Documents.Open(ref fileNameOb, ref missing, ref trueValue,
            ref missing, ref missing, ref missing, ref missing, ref missing,
            ref missing, ref missing, ref missing, ref missing, ref missing,
            ref missing, ref missing, ref missing);

            //зададим левый отступ
            doc.PageSetup.LeftMargin = 40f;

            doc.PageSetup.Orientation = WdOrientation.wdOrientLandscape;


            //счётчик строк
            int i = 1;

            //Вставить таблицу
            object bookNaziv = "таблица";
            Range wrdRng = doc.Bookmarks.get_Item(ref bookNaziv).Range;

            object behavior = Microsoft.Office.Interop.Word.WdDefaultTableBehavior.wdWord8TableBehavior;
            object autobehavior = Microsoft.Office.Interop.Word.WdAutoFitBehavior.wdAutoFitWindow;


            Microsoft.Office.Interop.Word.Table table = doc.Tables.Add(wrdRng, 1, 8, ref behavior, ref autobehavior);
            table.Range.ParagraphFormat.SpaceAfter = 8;

            table.Columns[1].Width = 60;
            table.Columns[2].Width = 100;
            table.Columns[3].Width = 60;
            table.Columns[4].Width = 100;
            table.Columns[5].Width = 60;
            table.Columns[6].Width = 100;
            table.Columns[7].Width = 100;
            table.Columns[8].Width = 150;

            table.Borders.Enable = 1; // Рамка - сплошная линия
            table.Range.Font.Name = "Times New Roman";
            table.Range.Font.Size = 10;

            //запишем данные в таблицу
            foreach (var item in list)
            {
                //table.Cell(i, 1).Column.Width = 10f;
                table.Cell(i, 1).Range.Text = item.КоличествоДоговоровБезАкта.ToString();
                table.Cell(i, 2).Range.Text = item.СуммаДоговоровБезАкт.ToString();
                //table.Cell(i, 2).Range.FormattedText.HorizontalInVertical = 

                table.Cell(i, 3).Range.Text = item.КоличествоДоговоровАкт.ToString();
                table.Cell(i, 4).Range.Text = item.СуммаДоговоровАкт.ToString();

                table.Cell(i, 5).Range.Text = item.CountДоговоров.ToString();
                table.Cell(i, 6).Range.Text = item.СуммаДоговоров.ToString();
                table.Cell(i, 7).Range.Text = item.ЛьготнаяКатегория.ToString();
                table.Cell(i, 8).Range.Text = item.ЛимитГод?.ToString();

                //doc.Words.Count.ToString();
                Object beforeRow1 = Type.Missing;
                table.Rows.Add(ref beforeRow1);

                i++;
            }

            //удалим последную строку
            table.Rows[i].Delete();

            //откроем получившейся документ
            app.Visible = true;

        }
    }
}
