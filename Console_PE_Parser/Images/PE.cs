using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;
using System.Text.RegularExpressions;
#region Images
using Console_PE_Parser.Images.DOS;
using Console_PE_Parser.Images.NT;
using Console_PE_Parser.Images.Sections;
using Console_PE_Parser.Images.Import;
#endregion

namespace Console_PE_Parser.Images
{
    public class PE
    {
        public byte[] buf { get; set; }

        #region Packer
        public Packer.Type? PackingMethod { get; private set; }
        public string PackedStatus
        {
            get
            {
                string PackInfo = PackingMethod == null ? "не упакован" : $"упакован с помощью {PackingMethod.Value}";
                return $"Файл {PackInfo}";
            }
        } 
        #endregion

        #region Images
        public IMAGE_DOS_HEADER dos { get; set; }
        public IMAGE_NT_HEADER nt { get; set; }
        public IMAGE_SECTION_HEADER[] sec { get; set; }
        public WrappedImport[] imp { get; set; }
        #endregion

        #region Offsets
        public uint nt_offset { get { return dos.e_lfanew; } }
        public uint sec_offset { get { return nt_offset + 4 + 20 + nt.FileHeader.SizeOfOptionalHeader; } }
        public uint imp_offset { get { return GetRVAOffset(nt.OptionalHeader.DataDirectory[1].VirtualAddress); } }
        #endregion

        public PE(byte[] b)
        {
            buf = b;

            dos = new IMAGE_DOS_HEADER(buf.SubArray(0, 64));

            nt = new IMAGE_NT_HEADER(buf.SubArray(nt_offset, buf.Length));

            sec = new IMAGE_SECTION_HEADER[nt.FileHeader.NumberOfSections];
            for (int i = 0; i < nt.FileHeader.NumberOfSections; i++)
                sec[i] = new IMAGE_SECTION_HEADER(buf.SubArray(sec_offset + i * 40, 40));
            
            imp = ReadImp();

            if (Packer.CheckUPX(sec))
                PackingMethod = Packer.Type.UPX;
        }

        public WrappedImport[] ReadImp()
        {

            List<WrappedImport> lst = new List<WrappedImport>();
            byte[] Bdesc;
            byte[] Bthunk;
            byte[] Bname;

            Bdesc = buf.SubArray(imp_offset, 20);
            for (int i = 1; Bdesc.Any(a => a != (byte)0); i++)
            {
                WrappedImport w = new WrappedImport();
                List<IMAGE_THUNK_DATA> thLst = new List<IMAGE_THUNK_DATA>();
                List<IMAGE_IMPORT_BY_NAME> nmLst = new List<IMAGE_IMPORT_BY_NAME>();

                w.descriptor = new IMAGE_IMPORT_DESCRIPTOR(Bdesc);
                w.descriptor.String_Name = GetNameByOffset(GetRVAOffset(w.descriptor.Name));

                while (true)
                {
                    uint Addressoffset = (uint)thLst.Count * 4 + GetRVAOffset(w.descriptor.FirstThunk);
                    uint Nameoffset = (uint)nmLst.Count*4 + GetRVAOffset(w.descriptor.OriginalFirstThunk == 0 ? w.descriptor.FirstThunk : w.descriptor.OriginalFirstThunk);
                    byte[] _bufAddr = buf.SubArray(Addressoffset, 4);
                    byte[] _bufName = buf.SubArray(Nameoffset, 4);
                    if (_bufAddr.All(a => a == (byte)0) || _bufName.All(a => a == (byte)0))
                        break;

                    thLst.Add(new IMAGE_THUNK_DATA(_bufAddr));
                    IMAGE_THUNK_DATA th = new IMAGE_THUNK_DATA(_bufName);
                    nmLst.Add(
                        new IMAGE_IMPORT_BY_NAME(
                            buf.SubArray(GetRVAOffset(th.AddressOfData), buf.Length)
                            )
                        );
                }
                w.thunk = thLst.ToArray();
                w.name = nmLst.ToArray();
                lst.Add(w);
                Bdesc = buf.SubArray(imp_offset + i * 20, 20);
            }

            return lst.ToArray();
        }

        #region WorkWithOffset
        private string GetNameByOffset(uint offset)
        {
            return Parser.GetStringFromBytes(buf.SubArray(offset, buf.Length));
        }

        public uint GetRVAOffset(uint VA)
        {
            for (int i = 1; i < nt.FileHeader.NumberOfSections; i++)
            {
                if (VA < sec[i].VirtualAddress)
                {
                    i--;
                    return VA - sec[i].VirtualAddress + sec[i].PointerToRawData;
                }
            }
            return VA - sec[sec.Length - 1].VirtualAddress + sec[sec.Length - 1].PointerToRawData;
        } 
        #endregion

        #region Print
        public string Print()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(dos.Print());
            sb.AppendLine(nt.Print());
            sb.AppendLine(PrintSections());
            sb.AppendLine(PrintImport());

            return sb.ToString();
        }

        public string PrintSections()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < sec.Length; i++)
            {
                sb.AppendLine(
                    sec[i].Print()
                    .Replace("{i}", i.ToString())
                    );
                sb.AppendLine();
            }

            return sb.ToString();
        }

        public string PrintImport()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < imp.Length; i++)
            {
                sb.AppendLine(
                    imp[i].Print()
                    .Replace("{i}", i.ToString())
                    );
                sb.AppendLine();
            }

            return sb.ToString();
        }
        #endregion
    }
}
