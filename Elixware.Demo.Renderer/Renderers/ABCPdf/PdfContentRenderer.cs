using Demo.Common.Models;
using WebSupergoo.ABCpdf13;
using WebSupergoo.ABCpdf13.Objects;

namespace Demo.Renderer.Renderers.ABCPdf
{
    internal class PdfContentRenderer
    {
        public static void RenderContent(Doc pdf, InputData input)
        {
            if (input.Content == null)
            {
                return;
            }
            if (input.Content.Values.Count == 0)
            {
                return;
            }
            pdf.Form.NeedAppearances = true;
            var formFields = pdf.Form.GetFieldNames();
            foreach (var fieldName in formFields)
            {
                var field = pdf.Form[fieldName];
                if (input.Content.TryGetValue(fieldName, out var value))
                {
                    field.Value = value.ToString() ?? string.Empty;
                }
                field.Flags = Field.FieldFlags.ReadOnly;
            }
        }
    }
}
