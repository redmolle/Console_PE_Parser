using System;
using System.Text;

namespace Console_PE_Parser.Images.Import
{
    public class IMAGE_IMPORT_DESCRIPTOR
    {
        public uint Characteristics { get; set; }
        public uint OriginalFirstThunk { get; set; }
        public uint TimaDateStamp { get; set; }
        public uint ForwardChain { get; set; }
        public uint Name { get; set; }
        public uint FirstThunk { get; set; }

        public string String_Name { get; set; }

        public IMAGE_IMPORT_DESCRIPTOR(byte[] bytes)
        {
            Characteristics = BitConverter.ToUInt32(bytes, 0);
            OriginalFirstThunk = BitConverter.ToUInt32(bytes, 0);
            TimaDateStamp = BitConverter.ToUInt32(bytes, 4);
            ForwardChain = BitConverter.ToUInt32(bytes, 8);
            Name = BitConverter.ToUInt32(bytes, 12);
            FirstThunk = BitConverter.ToUInt32(bytes, 16);

        }

        public string Print()
        {
            var tbl = new ConsoleTables.ConsoleTable("Наименование", "Значение");
            tbl
                .AddRow("Characteristics", Characteristics)
                .AddRow("OriginalFirstThunk", OriginalFirstThunk)
                .AddRow("TimaDateStamp", TimaDateStamp)
                .AddRow("ForwardChain", ForwardChain)
                .AddRow("Name", Name)
                .AddRow("String_Name", String_Name)
                .AddRow("FirstThunk", FirstThunk);

            return "IMAGE_IMPORT_DESCRIPTOR [{i}]\n" + tbl.ToString();
        }
    }
}
