using System;
using System.Text;

namespace Console_PE_Parser.Images.NT
{
    public class IMAGE_DATA_DIRECTORY
    {
        public uint VirtualAddress { get; set; }
        public uint Size           { get; set; }

        public IMAGE_DATA_DIRECTORY(byte[] bytes)
        {
            VirtualAddress = BitConverter.ToUInt32(bytes, 0);
            Size = BitConverter.ToUInt32(bytes, 4);
        }

        public string Print()
        {
            var tbl = new ConsoleTables.ConsoleTable("Наименование", "Значение");
            tbl
                .AddRow("VirtualAddress", VirtualAddress)
                .AddRow("Size", Size);

            return "IMAGE_DATA_DIRECTORY {i}\n" + tbl.ToString();
        }

        //public string ToString(string tab = null)
        //{
        //    tab = tab ?? string.Empty;
        //    StringBuilder sb = new StringBuilder();
        //    sb.AppendLine();
        //    sb.AppendLine($"{tab}---   DATA-DIRECTORY   ---");
        //    sb.AppendLine();
        //    sb.AppendFormat("{0}{1,-20} : {2,-20}\n", tab, "VirtualAddress", VirtualAddress);
        //    sb.AppendFormat("{0}{1,-20} : {2,-20}\n", tab, "Size",           Size);
        //    sb.AppendLine();
        //    sb.AppendLine($"{tab}---   DATA-DIRECTORY   ---");
        //    sb.AppendLine();

        //    return sb.ToString();
        //}
    }
}
