using System;
using System.Collections.Generic;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;

namespace TestWPF.Servcie
{
    public readonly struct ExcelCell
    {
        private readonly string[] _SharedStrings;
        private readonly string _Formula;
        private readonly string _Value;
        private readonly string _Type;
        private readonly string _Index;
        private readonly int _Style;

        public string Index => _Index;

        public string Formula => _Formula;

        public bool HasValue { get; }

        public string Value => _Type switch
        {
            "s" => _SharedStrings[int.Parse(_Value)],
            "str" => _Value,
            _ => _Value
        };

        private static void ReadCellAttributes(IEnumerable<OpenXmlAttribute> Attributes, out string r, out int s, out string t)
        {
            r = default;
            s = default;
            t = default;

            foreach (var attribute in Attributes)
                switch (attribute.LocalName)
                {
                    case "r":
                        r = attribute.Value;
                        break;
                    case "s":
                        s = int.Parse(attribute.Value);
                        break;
                    case "t":
                        t = attribute.Value;
                        break;
                }
        }

        public ExcelCell(OpenXmlPartReader Reader, string[] SharedStrings)
        {
            if (Reader.ElementType != typeof(Cell))
                throw new FormatException();

            ReadCellAttributes(Reader.Attributes, out _Index, out _Style, out _Type);

            _SharedStrings = SharedStrings;
            _Formula = null;
            _Value = null;

            if (!Reader.Read())
                throw new FormatException();

            if (Reader.IsEndElement)
            {
                HasValue = false;
                return;
            }

            HasValue = true;

            do
            {
                if (Reader.ElementType == typeof(CellValue))
                {
                    _Value = Reader.GetText();
                    Reader.Skip();
                }
                else if (Reader.ElementType == typeof(CellFormula))
                {
                    _Formula = Reader.GetText();
                    Reader.Skip();
                }
            }
            while (Reader.ElementType != typeof(Cell));
        }
    }
}