using System.Collections.ObjectModel;
using DocumentFormat.OpenXml;

namespace TestWPF.Servcie
{
    internal static class ExcelEx
    {
        public static string Value(this ReadOnlyCollection<OpenXmlAttribute> Attributes, string Name)
        {
            string result = null;

            foreach (var attribute in Attributes)
                if (attribute.LocalName == Name)
                    result = attribute.Value;

            return result;
        }
    }
}