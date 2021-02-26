using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;

namespace TestWPF.Servcie
{
    public readonly struct ExcelRow : IEnumerable<ExcelCell>
    {
        private readonly OpenXmlPartReader _Reader;
        private readonly string[] _SharedStrings;
        private readonly int _Index;
        private readonly string _Spans;
        private readonly int? _Style;
        private readonly int? _CustomFormat;
        private readonly double? _Height;
        private readonly double? _CustomHeight;
        private readonly bool _Collapsed;
        private readonly int _OutlineLevel;
        private readonly bool _Hidden;

        public int Index => _Index;

        public bool Hidden => _Hidden;
        public bool Collapsed => _Collapsed;

        public IEnumerable<string> Values => this.Select(cell => cell.Value);

        private static void ReadRowAttributes(
            IEnumerable<OpenXmlAttribute> Attributes,
            out int Index,
            out string Spans,
            out int? Style,
            out int? CustomFormat,
            out double? Height,
            out double? CustomHeight,
            out bool Collapsed,
            out bool Hidden,
            out int OutlineLevel)
        {
            Index = default;
            Spans = default;
            Style = default;
            CustomFormat = default;
            Height = default;
            CustomHeight = default;
            Collapsed = default;
            Hidden = default;
            OutlineLevel = default;

            foreach (var attribute in Attributes)
                switch (attribute.LocalName)
                {
                    case "r":
                        Index = int.Parse(attribute.Value);
                        break;
                    case "spans":
                        Spans = attribute.Value;
                        break;
                    case "s":
                        Style = int.Parse(attribute.Value);
                        break;
                    case "customFormat":
                        CustomFormat = int.Parse(attribute.Value);
                        break;
                    case "hidden":
                        Hidden = attribute.Value == "1";
                        break;
                    case "outlineLevel":
                        OutlineLevel = int.Parse(attribute.Value);
                        break;
                    case "ht":
                        Height = double.Parse(attribute.Value, CultureInfo.InvariantCulture);
                        break;
                    case "collapsed":
                        Collapsed = attribute.Value == "1";
                        break;
                    case "customHeight":
                        CustomHeight = double.Parse(attribute.Value, CultureInfo.InvariantCulture);
                        break;
                }
        }

        public ExcelRow(OpenXmlPartReader Reader, string[] SharedStrings)
        {
            if (Reader.ElementType != typeof(Row))
                throw new FormatException();

            ReadRowAttributes(Reader.Attributes,
                out _Index,
                out _Spans,
                out _Style,
                out _CustomFormat,
                out _Height,
                out _CustomHeight,
                out _Collapsed,
                out _Hidden,
                out _OutlineLevel);

            _Reader = Reader;
            _SharedStrings = SharedStrings;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<ExcelCell> GetEnumerator()
        {
            if (_Reader.ElementType != typeof(Row) || !_Reader.IsStartElement)
                throw new InvalidOperationException("Некорректное состояние объекта чтения потока данных. Текущий элемент не является строкой.");

            var index = int.Parse(_Reader.Attributes.Value("r"));
            if (_Index != index)
                throw new InvalidOperationException("Попытка повторного перечисления ячеек строки невозможно");

            while (_Reader.Read())
                if (_Reader.ElementType == typeof(Cell) && _Reader.IsStartElement)
                    yield return new ExcelCell(_Reader, _SharedStrings);
                else if (_Reader.ElementType == typeof(Row))
                    break;
                else
                    _Reader.Skip();
        }
    }
}