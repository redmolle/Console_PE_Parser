using System;
using System.Text;

namespace Console_PE_Parser.Images.DOS
{
    public class IMAGE_DOS_HEADER
    {
        public ushort e_magic { get; set; }
        public ushort e_cblp { get; set; }
        public ushort e_cp { get; set; }
        public ushort e_crlc { get; set; }
        public ushort e_cparhdr { get; set; }
        public ushort e_minalloc { get; set; }
        public ushort e_maxalloc { get; set; }
        public ushort e_ss { get; set; }
        public ushort e_sp { get; set; }
        public ushort e_csum { get; set; }
        public ushort e_ip { get; set; }
        public ushort e_cs { get; set; }
        public ushort e_lfarlc { get; set; }
        public ushort e_ovno { get; set; }
        public ushort[] e_res { get; set; }
        public ushort e_oemid { get; set; }
        public ushort e_oemidinfo { get; set; }
        public ushort[] e_res2 { get; set; }
        public uint e_lfanew { get; set; }

        public IMAGE_DOS_HEADER(byte[] bytes)
        {
            e_magic = BitConverter.ToUInt16(bytes, 0);
            e_cblp = BitConverter.ToUInt16(bytes, 2);
            e_cp = BitConverter.ToUInt16(bytes, 4);
            e_crlc = BitConverter.ToUInt16(bytes, 6);
            e_cparhdr = BitConverter.ToUInt16(bytes, 8);
            e_minalloc = BitConverter.ToUInt16(bytes, 10);
            e_maxalloc = BitConverter.ToUInt16(bytes, 12);
            e_ss = BitConverter.ToUInt16(bytes, 14);
            e_sp = BitConverter.ToUInt16(bytes, 16);
            e_csum = BitConverter.ToUInt16(bytes, 18);
            e_ip = BitConverter.ToUInt16(bytes, 20);
            e_cs = BitConverter.ToUInt16(bytes, 22);
            e_lfarlc = BitConverter.ToUInt16(bytes, 24);
            e_ovno = BitConverter.ToUInt16(bytes, 26);
            e_res = new ushort[4]
            {
                BitConverter.ToUInt16(bytes, 28),
                BitConverter.ToUInt16(bytes, 30),
                BitConverter.ToUInt16(bytes, 32),
                BitConverter.ToUInt16(bytes, 34)
            };
            e_oemid = BitConverter.ToUInt16(bytes, 36);
            e_oemidinfo = BitConverter.ToUInt16(bytes, 38);
            e_res2 = new ushort[10]
            {
                BitConverter.ToUInt16(bytes, 40),
                BitConverter.ToUInt16(bytes, 42),
                BitConverter.ToUInt16(bytes, 44),
                BitConverter.ToUInt16(bytes, 46),
                BitConverter.ToUInt16(bytes, 48),
                BitConverter.ToUInt16(bytes, 50),
                BitConverter.ToUInt16(bytes, 52),
                BitConverter.ToUInt16(bytes, 54),
                BitConverter.ToUInt16(bytes, 56),
                BitConverter.ToUInt16(bytes, 58)
            };
            e_lfanew = BitConverter.ToUInt32(bytes, 60);
        }

        public string Print()
        {
            var tbl = new ConsoleTables.ConsoleTable("Наименование", "Dec", "Hex", "ASCII");
            tbl.AddRow(Parser.GetDecHexAscii("e_magic", e_magic))
               .AddRow(Parser.GetDecHexAscii("e_cblp", e_cblp))
               .AddRow(Parser.GetDecHexAscii("e_cp", e_cp))
               .AddRow(Parser.GetDecHexAscii("e_crlc", e_crlc))
               .AddRow(Parser.GetDecHexAscii("e_cparhdr", e_cparhdr))
               .AddRow(Parser.GetDecHexAscii("e_minalloc", e_minalloc))
               .AddRow(Parser.GetDecHexAscii("e_maxalloc", e_maxalloc))
               .AddRow(Parser.GetDecHexAscii("e_ss", e_ss))
               .AddRow(Parser.GetDecHexAscii("e_sp", e_sp))
               .AddRow(Parser.GetDecHexAscii("e_csum", e_csum))
               .AddRow(Parser.GetDecHexAscii("e_ip", e_ip))
               .AddRow(Parser.GetDecHexAscii("e_cs", e_cs))
               .AddRow(Parser.GetDecHexAscii("e_lfarlc", e_lfarlc))
               .AddRow(Parser.GetDecHexAscii("e_ovno", e_ovno));

            for (int i = 0; i < e_res.Length; i++)
                tbl.AddRow(Parser.GetDecHexAscii($"e_res[{i}]", e_res[i]));

            tbl.AddRow(Parser.GetDecHexAscii("e_oemid", e_ovno))
               .AddRow(Parser.GetDecHexAscii("e_oemidinfo", e_ovno));

            for (int i = 0; i < e_res2.Length; i++)
                tbl.AddRow(Parser.GetDecHexAscii($"e_res2[{i}]", e_res2[i]));

               tbl.AddRow(Parser.GetDecHexAscii("e_lfanew", e_lfanew));

            return "IMAGE_DOS_HEADER\n" + tbl.ToString();
        }
    }
}