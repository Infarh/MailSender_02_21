using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace TestWPF.Servcie
{
    public class Excel : IEnumerable<ExcelSheet>
    {
        public static Excel File(string FileName) => new(FileName);

        public string FileName { get; }

        public IEnumerable<string> Sheets
        {
            get
            {
                using var document = SpreadsheetDocument.Open(FileName, false);
                var workbook = document.WorkbookPart;

                foreach (var sheet in workbook.Workbook.Sheets.Cast<Sheet>())
                    yield return sheet.Name;
            }
        }

        public int SheetsCount
        {
            get
            {
                using var document = SpreadsheetDocument.Open(FileName, false);
                var workbook = document.WorkbookPart;
                return workbook.Workbook.Sheets.Count();
            }
        }

        public ExcelSheet this[string SheetName]
        {
            get
            {
                using var document = SpreadsheetDocument.Open(FileName, false);
                var workbook = document.WorkbookPart;

                var sheet = workbook.Workbook.Sheets
                   .OfType<Sheet>()
                   .FirstOrDefault(s => s.Name.Value == SheetName);

                return sheet is null ? null : new ExcelSheet(this, sheet);
            }
        }

        public Excel(string FileName) => this.FileName = FileName;

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<ExcelSheet> GetEnumerator()
        {
            using var document = SpreadsheetDocument.Open(FileName, false);
            var workbook = document.WorkbookPart;

            foreach (var sheet in workbook.Workbook.Sheets.Cast<Sheet>())
                yield return new ExcelSheet(this, sheet);
        }
    }
}
