using System;
using System.Text;
using System.Collections.Generic;

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
            string[] s = Parser.GetDecHexAscii("Name", Name);
            var tbl = new ConsoleTables.ConsoleTable("Наименование", "Dec", "Hex", "ASCII");
            tbl
                .AddRow(Parser.GetDecHexAscii("Characteristics", Characteristics))
                .AddRow(Parser.GetDecHexAscii("OriginalFirstThunk", OriginalFirstThunk))
                .AddRow(Parser.GetDecHexAscii("TimaDateStamp", TimaDateStamp))
                .AddRow(Parser.GetDecHexAscii("ForwardChain", ForwardChain))
                .AddRow(new string[4] { s[0], s[1], s[2], String_Name})
                .AddRow(Parser.GetDecHexAscii("FirstThunk", FirstThunk));

            return "IMAGE_IMPORT_DESCRIPTOR [{i}]\n" + tbl.ToString();
        }
    }
}
