using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText.Layout;
using DanceApp.Model.Data;

#nullable disable
namespace DanceApp.Model
{
    public class CreatePDF
    {
        public DataBaseContext db = GlobalClass.db;
        public void Protocol1(string path, List<Pair> selectedPairs, List<Judge> selectedJudges, string round, string group, string dance, string performance)
        {
            // Создание PDF
            PdfWriter writer = new PdfWriter(path);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            // Установка русского шрифта
            var font = PdfFontFactory.CreateFont("C:\\Windows\\Fonts\\times.ttf", "Identity-H");
            document.SetFont(font);

            // Отделяющая линия
            LineSeparator ls = new LineSeparator(new SolidLine());
            ls.SetMarginBottom(6);

            float value = (float)selectedJudges.Count / 3;
            int pageCount = (int)Math.Ceiling(value);

            for (int i = 0; i < selectedJudges.Count; i++)
            {
                // Строка 1
                Paragraph judge = new Paragraph("Судья: " + selectedJudges[i].Character)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                    .SetFontSize(14);
                document.Add(judge);

                // Строка 2
                Paragraph paragraphRound = new Paragraph("Тур: " + round)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                    .SetFontSize(14);
                document.Add(paragraphRound);

                // Строка 3
                Paragraph paragraphGroup = new Paragraph("Группа: " + group)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                    .SetFontSize(14);
                document.Add(paragraphGroup);

                // Строка 4
                Paragraph paragraphDance = new Paragraph("Танец: " + dance)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                    .SetFontSize(14);
                document.Add(paragraphDance);

                // Строка 5
                Paragraph paragraphPerformance = new Paragraph("Заход: " + performance)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                    .SetFontSize(14);
                paragraphPerformance.SetMarginBottom(10);
                document.Add(paragraphPerformance);

                // Создание таблицы
                float[] columnWidths = new float[selectedPairs.Count + 1];
                columnWidths[0] = 50f;
                for (int c = 1; c < selectedPairs.Count + 1; c++)
                {
                    columnWidths[c] = 25f;
                }
                Table table = new Table(columnWidths);

                // Ячейка в первой строке первого столбца
                Cell cell11 = new Cell(1, 1)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .Add(new Paragraph("Пара"));
                table.AddCell(cell11);

                // Запись в ячейки номера пар
                foreach (var row in selectedPairs)
                {
                    table.AddCell(new Cell(1, 1)
                        .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                        .Add(new Paragraph(row.Number)));
                }

                // Ячейка во второй строке первого столбца
                Cell cell21 = new Cell(1, 1)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .Add(new Paragraph("Место"));
                table.AddCell(cell21);

                // Добавление пустых ячеек
                foreach (var row in selectedPairs)
                {
                    table.AddCell(new Cell(1, 1).Add(new Paragraph()));
                }

                table.SetMarginBottom(15);
                document.Add(table);
                document.Add(ls);

                if (((float)i + 1) % 3 == 0)
                {
                    // Переход на следующую страницу
                    document.Add(new AreaBreak());
                }
            }
            // Завершение работы с документом
            document.Close();
        }

        public void Protocol2(string path, List<Judge> selectedJudges, string round, string group, string performance)
        {
            
        }

        public void FinalProtocol(string path, int RoundID, int GroupID)
        {
            // Создание PDF
            PdfWriter writer = new PdfWriter(path);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            // Отделяющая линия
            LineSeparator ls = new LineSeparator(new SolidLine());
            ls.SetMarginBottom(2);

            var competition = db.Competition.Find(1);
            var roundTitle = db.Round.Find(RoundID).Title;
            var CompetitionTitle = competition.Title; 
            var groupsInRound = db.Group.Where(x => x.RoundID == RoundID).ToList(); // Ещё надо программу группы и количество пар, пара в группе, номер пары, фио, клуб, город, тренер, место
            var manager = competition.Manager;
            var city = competition.City;

            // Установка русского шрифта
            var font = PdfFontFactory.CreateFont("C:\\Windows\\Fonts\\times.ttf", "Identity-H");
            document.SetFont(font);

            

            // Название соревнования
            Paragraph Title = new Paragraph(CompetitionTitle)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFontSize(16);
            document.Add(Title);

            for (int i = 0; i < groupsInRound.Count; i++)
            {
                // Группа
                Paragraph Group = new Paragraph("Категория: " + groupsInRound[i].Title)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                    .SetFontSize(14);
                document.Add(Group);

                // Организатор
                Paragraph Manager = new Paragraph("Организатор: " + manager)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                    .SetFontSize(14);
                document.Add(Manager);

                // Количество пар
                var pairsCount = (groupsInRound[i].PairsCount).ToString();
                Paragraph PairsCount = new Paragraph("Всего пар: " + pairsCount)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                    .SetFontSize(12);
                document.Add(PairsCount);

                // Информация
                Paragraph Info = new Paragraph()
                    .SetFontSize(12);
                Info.Add(new Text("Номер     Фамилия           Клуб                    Руководитель/тренер                     Место"));

                document.Add(Info);

                document.Add(ls);

                var pairs = db.PairsInGroup.Where(x => x.GroupID == groupsInRound[i].ID).Select(x => x.PairID);
                List<Pair> pairsInGroup = new List<Pair>();
                foreach (var p in pairs)
                {
                    var pair = db.Pair.Find(p);
                    pairsInGroup.Add(pair);
                }

                for (int j = 0; j < pairsInGroup.Count(); j++)
                {
                    // Пара
                    Paragraph Pair = new Paragraph()
                        .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                        .SetFontSize(12);

                    Pair.Add(new Text(pairsInGroup[j].Number).SetFontSize(14));

                    Pair.Add(new Tab());
                    Pair.Add(new Text(pairsInGroup[j].MaleSurname));
                    Pair.Add(new Text("\n"));
                    Pair.Add(new Text("     " + pairsInGroup[j].FemaleSurname));

                    document.Add(Pair);
                }
            }




            // Завершение работы с документом
            document.Close();
        }
    }
}
