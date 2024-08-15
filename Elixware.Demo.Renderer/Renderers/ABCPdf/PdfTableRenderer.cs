using Demo.Common.Models;
using WebSupergoo.ABCpdf13;

namespace Demo.Renderer.Renderers.ABCPdf
{
    internal class PdfTableRenderer
    {
        public static void RenderTables(Doc pdf, InputData input)
        {
            if (input.Tables == null)
            {
                return;
            }
            if (input.Tables.Values.Count == 0)
            {
                return;
            }
            foreach (var tableInfo in input.Tables.Values)
            {
                // TODO: Customizable table locations
                pdf.Rect.Inset(tableInfo.Location.X, tableInfo.Location.Y);
                var table = new PDFTable(pdf, tableInfo.Values.First().Keys.Count);
                table.CellPadding = 5;
                table.HorizontalAlignment = 1;
                RenderTableHeader(table, tableInfo);
                RenderTableBody(table, tableInfo);
                table.Frame();
            }
        }

        private static void RenderTableHeader(PDFTable table, TableInfo input)
        {
            table.NextRow();
            var columns = new List<string>();
            foreach (var key in input.Values.First().Keys)
            {
                string label = GetHeaderLabel(key, input.Headers!);
                string content = string.Format("<stylerun hpos=0><strong>{0}</strong></stylerun>", label);
                columns.Add(content);
            }
            table.AddTextStyled(columns);
        }

        private static string GetHeaderLabel(string key, IDictionary<string, string> headers)
        {
            if (headers != null && headers.TryGetValue(key, out var label))
            {
                return label;
            }
            return key;
        }

        private static void RenderTableBody(PDFTable table, TableInfo input)
        {
            foreach (var rowData in input.Values)
            {
                table.NextRow();
                var columns = new List<string>();
                foreach (var rowValue in rowData.Values)
                {
                    string content = string.Format("<stylerun hpos=0>{0}</stylerun>", rowValue.ToString());
                    columns.Add(content.ToString());
                }
                table.AddTextStyled(columns);
            }
        }
    }
}
