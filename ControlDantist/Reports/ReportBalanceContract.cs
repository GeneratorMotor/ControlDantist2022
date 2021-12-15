using System;
using ControlDantist.Classes;
using System.Collections.Generic;
using System.Linq;
using ControlDantist.BalanceContract;
using System.Text;

namespace ControlDantist.Reports
{
    public class ReportBalanceContract : IPrintReport
    {
        private List<ReportBalance> listPrint;

        private string dateStart = string.Empty;
        private string dateEnd = string.Empty;
        private string hospital = string.Empty;


        public ReportBalanceContract(List<ReportBalance> listPrint, string dateStart, string dateEnd, string nameHospital)
        {
            this.listPrint = new List<ReportBalance>();

            if (listPrint == null)
            {
                 throw new ArgumentNullException(nameof(listPrint));
            }
            else
            {
                this.listPrint.AddRange(listPrint);
            }

            if(dateStart == null)
            {
                throw new ArgumentException("Не установлена дата начала отчета");
            }

            if (dateEnd == null)
            {
                throw new ArgumentException("Не установлена дата окончания отчета");
            }

            if(nameHospital == null)
            {
                throw new ArgumentException("Не установлен наименование поликлинники");
            }

            this.dateStart = dateStart;

            this.dateEnd = dateEnd;

            this.hospital = nameHospital;

        }

        public void Print()
        {
            // Выведим на бумагу.
            GenerateExcelFile excel = new GenerateExcelFile();

            // Создадим книгу.
            excel.ExcelWorkbook();

            // Создадим лист.
            excel.ExcelWorksheet(1);

            string cellA1 = "a1";
            excel.ExcelCellsAddValueFontBold(cellA1, "Баланс по количеству и суммам договоров " + this.hospital.Trim() + " в диапазоне с - " + dateStart.Trim() + " до " + dateEnd.Trim());

            // Выведим шапку таблицы.
            excel.ExcelCellsAddValueFontBold("a3", "Льготная категория");
            excel.ExcelCellsAddValueFontBold("b3", "Количество догворов");
            excel.ExcelCellsAddValueFontBold("c3", "Сумма");

            // Индекс элемента в списке данных для отчёта.
            int k = 0;

            // Счётчик порядкового номера.
            int count = 4;

            // Номер по порядку.
            int countPP = 1;

            foreach (var item in listPrint)
            {
                // Сгенерируем содержимое письма.
                for (int i = 1; i <= 3; i++)
                {

                    if (i == 1)
                    {
                        string cellA = "a" + count.ToString();
                        excel.ExcelCellsAddValueFormat(cellA, item.ЛьготнаяКатегория, "@");
                    }

                    if (i == 2)
                    {
                        string cellB = "b" + count.ToString();
                        excel.ExcelCellsAddValueFormat(cellB, item.КоличествоДоговоров, "@");
                    }

                    if (i == 3)
                    {
                        string cellB = "c" + count.ToString();
                        excel.ExcelCellsAddValueFormat(cellB, item.СуммаДоговоров, "@");
                    }

                    k++;

                }

                count++;

                countPP++;
            }

            // Выведим общее количество договоров и общую сумму.

            count++;

            string cellИтого = "b" + count.ToString();
            excel.ExcelCellsAddValueFontBold(cellИтого, "Итого : " + listPrint.Sum(w => w.КоличествоДоговоров).ToString());

            string cellСумма = "c" + count.ToString();
            excel.ExcelCellsAddValueFontBold(cellСумма, "Сумма : " + listPrint.Sum(w => w.СуммаДоговоров).ToString("c"));


        }
    }
}
