using System.Linq;

namespace MailSender.lib.Service
{
    public static class TextEncoder
    {
        public static string Encode(string str, int key = 1)
        {
            return new(str.Select(c => (char) (c + key)).ToArray());
        }

        public static string Decode(string str, int key = 1) => Encode(str, -key);
    }
}
