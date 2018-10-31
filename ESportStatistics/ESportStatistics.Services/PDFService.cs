using System;
using System.Collections.Generic;
using System.Text;
using ESportStatistics.Core.Providers.Contracts;
using ESportStatistics.Services.Contracts;
using SautinSoft.Document;
using SautinSoft.Document.Tables;

namespace ESportStatistics.Services
{
    public class PDFService : IPDFService
    {
        public PDFService(ILoggerService logger)
        {
            this.Logger = logger ?? throw new ArgumentNullException();
        }

        private ILoggerService Logger { get; }

        public void ToPDF<T>(IEnumerable<T> entities, List<string> columns, string fileName)
            where T : class
        {
            // Filter input
            Type elementType = typeof(T);

            string documentPath = $"{fileName}-{DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss")}.pdf";

            DocumentCore dc = new DocumentCore();

            Section s = new Section(dc);
            dc.Sections.Add(s);

            Table table = this.CreatePDF(entities, columns, dc);

            s.Blocks.Add(table);

            dc.Save(documentPath, new PdfSaveOptions() { Compliance = PdfCompliance.PDF_A });
        }

        private Table CreatePDF<T>(IEnumerable<T> entities, IList<string> columns, /*PdfPTable table,*/ DocumentCore dc)
        {
            Table table = new Table(dc);
            double width = LengthUnitConverter.Convert(100, LengthUnit.Millimeter, LengthUnit.Point);
            table.TableFormat.PreferredWidth = new TableWidth(width, TableWidthUnit.Point);
            table.TableFormat.Alignment = HorizontalAlignment.Center;

            int cols = columns.Count;

            TableRow row = new TableRow(dc);
            for (int i = 0; i < cols; i++)
            {
                if (typeof(T).GetProperty(columns[i]) == null)
                {
                    columns.RemoveAt(i);
                    continue;
                }

                TableCell cell = new TableCell(dc);

                cell.CellFormat.Borders.SetBorders(MultipleBorderTypes.Outside, BorderStyle.Dotted, Color.Black, 1.0);

                cell.CellFormat.PreferredWidth = new TableWidth(width / cols, TableWidthUnit.Point);

                row.Cells.Add(cell);

                Paragraph p = new Paragraph(dc);
                p.ParagraphFormat.Alignment = HorizontalAlignment.Center;
                p.ParagraphFormat.SpaceBefore = LengthUnitConverter.Convert(3, LengthUnit.Millimeter, LengthUnit.Point);
                p.ParagraphFormat.SpaceAfter = LengthUnitConverter.Convert(3, LengthUnit.Millimeter, LengthUnit.Point);

                p.Content.Start.Insert(String.Format("{0}", columns[i]), new CharacterFormat() { FontName = "Arial", FontColor = new Color("#3399FF"), Size = 12.0 });
                cell.Blocks.Add(p);
            }
            table.Rows.Add(row);

            foreach (var entity in entities)
            {
                TableRow currentRow = new TableRow(dc);
                foreach (var propName in columns)
                {
                    TableCell cell = new TableCell(dc);

                    cell.CellFormat.Borders.SetBorders(MultipleBorderTypes.Outside, BorderStyle.Dotted, Color.Black, 1.0);

                    cell.CellFormat.PreferredWidth = new TableWidth(width / cols, TableWidthUnit.Point);

                    currentRow.Cells.Add(cell);

                    Paragraph p = new Paragraph(dc);
                    p.ParagraphFormat.Alignment = HorizontalAlignment.Center;
                    p.ParagraphFormat.SpaceBefore = LengthUnitConverter.Convert(3, LengthUnit.Millimeter, LengthUnit.Point);
                    p.ParagraphFormat.SpaceAfter = LengthUnitConverter.Convert(3, LengthUnit.Millimeter, LengthUnit.Point);

                    var propValue = entity.GetType().GetProperty(propName).GetValue(entity, null);

                    try
                    {
                        p.Content.Start.Insert(String.Format("{0}", propValue.ToString()), new CharacterFormat() { FontName = "Arial", FontColor = new Color("#3399FF"), Size = 12.0 });
                        cell.Blocks.Add(p);
                    }
                    catch (Exception ex)
                    {
                        Logger.LogToFileAsync(ex.Message);
                        throw;
                    }

                }
                table.Rows.Add(currentRow);
            }

            return table;
        }
    }
}
