using System;
using System.Text;
namespace Console_PE_Parser.Images.Sections
{
    public class IMAGE_SECTION_HEADER
    {
        public ulong      Name                  { get; set; }
        public IMAGE_MISC Misc                  { get; set; }
        public uint       VirtualAddress        { get; set; }
        public uint       SizeOfRawData         { get; set; }
        public uint       PointerToRawData      { get; set; }
        public uint       PointerToRelocations  { get; set; }
        public uint       PointerToLineenumbers { get; set; }
        public uint       NumberOfRelocations   { get; set; }
        public ushort     NumberOfLinenumbers   { get; set; }
        public uint       Characteristics       { get; set; }

        public IMAGE_SECTION_HEADER(byte[] bytes)
        {
            Name = BitConverter.ToUInt64(bytes, 0);
            Misc = new IMAGE_MISC(bytes.SubArray(8, 4));
            VirtualAddress = BitConverter.ToUInt32(bytes, 12);
            SizeOfRawData = BitConverter.ToUInt32(bytes, 16);
            PointerToRawData = BitConverter.ToUInt32(bytes, 20);
            PointerToRelocations = BitConverter.ToUInt32(bytes, 24);
            PointerToLineenumbers = BitConverter.ToUInt32(bytes, 28);
            NumberOfRelocations = BitConverter.ToUInt16(bytes, 32);
            NumberOfLinenumbers = BitConverter.ToUInt16(bytes, 34);
            Characteristics = BitConverter.ToUInt32(bytes, 36);
        }

        public string Print()
        {
            var tbl = new ConsoleTables.ConsoleTable("Наименование", "Dec", "Hex", "ASCII");
            tbl
                .AddRow(Parser.GetDecHexAscii("Name", Name))
                .AddRow(Parser.GetDecHexAscii("VirtualAddress", VirtualAddress))
                .AddRow(Parser.GetDecHexAscii("SizeOfRawData", SizeOfRawData))
                .AddRow(Parser.GetDecHexAscii("PointerToRawData", PointerToRawData))
                .AddRow(Parser.GetDecHexAscii("PointerToRelocations", PointerToRelocations))
                .AddRow(Parser.GetDecHexAscii("PointerToLineenumbers", PointerToLineenumbers))
                .AddRow(Parser.GetDecHexAscii("NumberOfRelocations", NumberOfRelocations))
                .AddRow(Parser.GetDecHexAscii("NumberOfLinenumbers", NumberOfLinenumbers))
                .AddRow(Parser.GetDecHexAscii("Characteristics", Characteristics));

            return "IMAGE_SECTION_HEADER [{i}]\n" + tbl.ToString() + "\n" + Misc.Print();
        }
    }
}
