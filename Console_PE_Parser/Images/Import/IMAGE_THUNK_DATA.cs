using System;
using System.Text;

namespace Console_PE_Parser.Images.Import
{
    public class IMAGE_THUNK_DATA
    {
        public uint ForwardString { get; set; }
        public uint Function { get; set; }
        public uint Ordinal { get; set; }
        public uint AddressOfData { get; set; }

        public IMAGE_THUNK_DATA(byte[] bytes)
        {
            ForwardString = BitConverter.ToUInt32(bytes, 0);
            Function = BitConverter.ToUInt32(bytes, 0);
            Ordinal = BitConverter.ToUInt32(bytes, 0);
            AddressOfData = BitConverter.ToUInt32(bytes, 0);
        }

        public string ToString(string tab = null)
        {
            tab = tab ?? string.Empty;
            StringBuilder sb = new StringBuilder();
            
            sb.AppendLine();
            sb.AppendLine($"{tab}---   IMAGE_THUNK_DATA   ---");
            sb.AppendLine();
            sb.AppendFormat("{0}{1,-20} : {2,-20}\n", tab, "ForwardString", ForwardString);
            sb.AppendFormat("{0}{1,-20} : {2,-20}\n", tab, "Function", Function);
            sb.AppendFormat("{0}{1,-20} : {2,-20}\n", tab, "Ordinal", Ordinal);
            sb.AppendFormat("{0}{1,-20} : {2,-20}\n", tab, "AddressOfData", AddressOfData);
            sb.AppendLine();
            sb.AppendLine($"{tab}---   IMAGE_THUNK_DATA   ---");
            sb.AppendLine();

            return sb.ToString();
        }
    }
}
