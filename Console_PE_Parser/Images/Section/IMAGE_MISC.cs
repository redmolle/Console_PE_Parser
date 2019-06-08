using System;
using System.Text;
namespace Console_PE_Parser.Images.Sections
{
    public class IMAGE_MISC
    {
        public uint PhysicalAddress { get; set; }
        public uint VirtualSize     { get; set; }

        public IMAGE_MISC(byte[] bytes)
        {
            PhysicalAddress = BitConverter.ToUInt32(bytes, 0);
            VirtualSize = BitConverter.ToUInt32(bytes, 0);
        }

        public string Print()
        {
            var tbl = new ConsoleTables.ConsoleTable("Наименование", "Dec", "Hex", "ASCII");
            tbl
                .AddRow(Parser.GetDecHexAscii("PhysicalAddress", PhysicalAddress))
                .AddRow(Parser.GetDecHexAscii("VirtualSize", VirtualSize));

            return "IMAGE_MISC\n" + tbl.ToString();
        }
    }
}
