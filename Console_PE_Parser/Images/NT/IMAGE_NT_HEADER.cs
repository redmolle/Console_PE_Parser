using System.Linq;
using System;
using System.Text;

namespace Console_PE_Parser.Images.NT
{
    public class IMAGE_NT_HEADER
    {
        public uint Signature                       { get; set; }
        public IMAGE_FILE_HEADER FileHeader         { get; set; }
        public IMAGE_OPTIONAL_HEADER OptionalHeader { get; set; }


        public IMAGE_NT_HEADER(byte[] bytes)
        {
            Signature = BitConverter.ToUInt32(bytes, 0);
            FileHeader = new IMAGE_FILE_HEADER(bytes.SubArray( 4, 20));
            OptionalHeader = new IMAGE_OPTIONAL_HEADER(bytes.SubArray(24, bytes.Length));
        }

        public string Print()
        {
            var tbl = new ConsoleTables.ConsoleTable("Наименование", "Значение");
            tbl
                .AddRow("Signature", Signature);
            return "IMAGE_NT_HEADER\n" + tbl.ToString() + "\n" + FileHeader.Print() + "\n" + OptionalHeader.Print();
        }

        //public string ToString(string tab = null)
        //{
        //    tab = tab ?? string.Empty;
        //    StringBuilder sb = new StringBuilder();


        //    sb.AppendLine();
        //    sb.AppendLine($"---   IMAGE_NT_HEADER   ---");
        //    sb.AppendLine();
        //    sb.AppendFormat("{0}{1,-20} : {2,-20}\n", tab, "Signature", Signature);

        //    sb.AppendLine(FileHeader.ToString(tab + tab));

        //    sb.AppendLine(OptionalHeader.ToString(tab + tab));

        //    sb.AppendLine($"---   IMAGE_NT_HEADER   ---");
        //    sb.AppendLine();
            
        //    return sb.ToString();
        //}
    }
}
