using System;
using System.Linq;
using System.Text;

namespace Console_PE_Parser.Images.Import
{
    public class IMAGE_IMPORT_BY_NAME
    {
        public ushort Hint;
        public byte[] Name;

        public IMAGE_IMPORT_BY_NAME(byte[] bytes)
        {
            Hint = BitConverter.ToUInt16(bytes, 0);
            Name = bytes.SubArray(2, bytes.Length).TakeWhile(t => t != 0).ToArray();
        }
        
        public string ToString(string tab = null)
        {
            tab = tab ?? string.Empty;
            StringBuilder sb = new StringBuilder();

            sb.AppendLine();
            sb.AppendLine($"{tab}---   IMAGE_IMPORT_BY_NAME   ---");
            sb.AppendLine();
            sb.AppendFormat("{0}{1,-20} : {2,-20}\n",         tab, "Hint", Hint);
            sb.AppendFormat("{0}{1,-20} : {2,-20} {3,-20}\n", tab, "Name", Name, BitConverter.ToString(Name));
            sb.AppendLine();
            sb.AppendLine($"{tab}---   IMAGE_IMPORT_BY_NAME   ---");
            sb.AppendLine();

            return sb.ToString();
        }
    }
}
