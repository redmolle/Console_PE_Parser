using System;
using System.Text;

namespace Console_PE_Parser.Images.NT
{
    public class IMAGE_FILE_HEADER
    {
        public ushort Machine              { get; set; }
        public ushort NumberOfSections     { get; set; }
        public uint   TimeDateStamp        { get; set; }
        public uint   PointerToSymbolTable { get; set; }
        public uint   NumberOfSymbols      { get; set; }
        public ushort SizeOfOptionalHeader { get; set; }
        public ushort Characteristics      { get; set; }

        public IMAGE_FILE_HEADER(byte[] bytes)
        {
            Machine = BitConverter.ToUInt16(bytes, 0);
            NumberOfSections = BitConverter.ToUInt16(bytes, 2);
            TimeDateStamp = BitConverter.ToUInt32(bytes, 4);
            PointerToSymbolTable = BitConverter.ToUInt32(bytes, 8);
            NumberOfSymbols = BitConverter.ToUInt32(bytes, 12);
            SizeOfOptionalHeader = BitConverter.ToUInt16(bytes, 16);
            Characteristics = BitConverter.ToUInt16(bytes, 18);
        }

        public string Print()
        {
            var tbl = new ConsoleTables.ConsoleTable("Наименование", "Dec", "Hex", "ASCII");
            tbl
                .AddRow(Parser.GetDecHexAscii("Machine", Machine))
                .AddRow(Parser.GetDecHexAscii("NumberOfSections", NumberOfSections))
                .AddRow(Parser.GetDecHexAscii("TimeDateStamp", TimeDateStamp))
                .AddRow(Parser.GetDecHexAscii("PointerToSymbolTable", PointerToSymbolTable))
                .AddRow(Parser.GetDecHexAscii("NumberOfSymbols", NumberOfSymbols))
                .AddRow(Parser.GetDecHexAscii("SizeOfOptionalHeader", SizeOfOptionalHeader))
                .AddRow(Parser.GetDecHexAscii("Characteristics", Characteristics));

            return "IMAGE_FILE_HEADER\n" + tbl.ToString();
        }

        //public string ToString(string tab = null)
        //{
        //    tab = tab ?? string.Empty;
        //    StringBuilder sb = new StringBuilder();

        //    sb.AppendLine();
        //    sb.AppendLine($"{tab}---   IMAGE_FILE_HEADER   ---");
        //    sb.AppendLine();
        //    sb.AppendFormat("{0}{1,-20} : {2,-20}\n", tab, "Machine",              Machine);
        //    sb.AppendFormat("{0}{1,-20} : {2,-20}\n", tab, "NumberOfSections",     NumberOfSections);
        //    sb.AppendFormat("{0}{1,-20} : {2,-20}\n", tab, "TimeDateStamp",        TimeDateStamp);
        //    sb.AppendFormat("{0}{1,-20} : {2,-20}\n", tab, "PointerToSymbolTable", PointerToSymbolTable);
        //    sb.AppendFormat("{0}{1,-20} : {2,-20}\n", tab, "NumberOfSymbols",      NumberOfSymbols);
        //    sb.AppendFormat("{0}{1,-20} : {2,-20}\n", tab, "SizeOfOptionalHeader", SizeOfOptionalHeader);
        //    sb.AppendFormat("{0}{1,-20} : {2,-20}\n", tab, "Characteristics",      Characteristics);
        //    sb.AppendLine();
        //    sb.AppendLine($"{tab}---   IMAGE_FILE_HEADER   ---");
        //    sb.AppendLine();

        //    return sb.ToString();
        //}
    }
}
