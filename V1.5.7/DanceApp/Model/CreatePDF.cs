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

namespace DanceApp.Model
{
    public class CreatePDF
    {
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
    }
}
