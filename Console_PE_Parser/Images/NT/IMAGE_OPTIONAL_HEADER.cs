using System;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Console_PE_Parser.Images.NT
{
    public class IMAGE_OPTIONAL_HEADER
    {
        public ushort Magic { get; set; }
        public byte MajorLinkerVersion { get; set; }
        public byte MinorLinkerVersion { get; set; }
        public uint SizeOfCode { get; set; }
        public uint SizeOfInitializedData { get; set; }
        public uint SizeOfUninitializedData { get; set; }
        public uint AddressOfEntryPoint { get; set; }
        public uint BaseOfCode { get; set; }
        public uint BaseOfData { get; set; }
        public uint ImageBase { get; set; }
        public uint SectionAlignment { get; set; }
        public uint FileAlignment { get; set; }
        public ushort MajorOperatingSystemVersion { get; set; }
        public ushort MinorOperatingSystemVersion { get; set; }
        public ushort MajorImageVersion { get; set; }
        public ushort MinorImageVersion { get; set; }
        public ushort MajorSubsystemVersion { get; set; }
        public ushort MinorSubsystemVersion { get; set; }
        public uint Win32VersionValue { get; set; }
        public uint SizeOfImage { get; set; }
        public uint SizeOfHeaders { get; set; }
        public uint CheckSum { get; set; }
        public ushort Subsystem { get; set; }
        public ushort DllCharacteristics { get; set; }
        public uint SizeOfStackReserve { get; set; }
        public uint SizeOfStackCommit { get; set; }
        public uint SizeOfHeapReserve { get; set; }
        public uint SizeOfHeapCommit { get; set; }
        public uint LoaderFlags { get; set; }
        public uint NumberOfRvaAndSizes { get; set; }
        public IMAGE_DATA_DIRECTORY[] DataDirectory { get; set; }

        public IMAGE_OPTIONAL_HEADER(byte[] bytes)
        {
            Magic = BitConverter.ToUInt16(bytes, 0);
            MajorLinkerVersion = bytes[2];
            MinorLinkerVersion = bytes[3];
            SizeOfCode = BitConverter.ToUInt32(bytes, 4);
            SizeOfInitializedData = BitConverter.ToUInt32(bytes, 8);
            SizeOfUninitializedData = BitConverter.ToUInt32(bytes, 12);
            AddressOfEntryPoint = BitConverter.ToUInt32(bytes, 16);
            BaseOfCode = BitConverter.ToUInt32(bytes, 20);
            BaseOfData = BitConverter.ToUInt32(bytes, 24);
            ImageBase = BitConverter.ToUInt32(bytes, 28);
            SectionAlignment = BitConverter.ToUInt32(bytes, 32);
            FileAlignment = BitConverter.ToUInt32(bytes, 36);
            MajorOperatingSystemVersion = BitConverter.ToUInt16(bytes, 40);
            MinorOperatingSystemVersion = BitConverter.ToUInt16(bytes, 42);
            MajorImageVersion = BitConverter.ToUInt16(bytes, 44);
            MinorImageVersion = BitConverter.ToUInt16(bytes, 46);
            MajorSubsystemVersion = BitConverter.ToUInt16(bytes, 48);
            MinorSubsystemVersion = BitConverter.ToUInt16(bytes, 50);
            Win32VersionValue = BitConverter.ToUInt32(bytes, 52);
            SizeOfImage = BitConverter.ToUInt32(bytes, 56);
            SizeOfHeaders = BitConverter.ToUInt32(bytes, 60);
            CheckSum = BitConverter.ToUInt32(bytes, 64);
            Subsystem = BitConverter.ToUInt16(bytes, 68);
            DllCharacteristics = BitConverter.ToUInt16(bytes, 70);
            SizeOfStackReserve = BitConverter.ToUInt32(bytes, 72);
            SizeOfStackCommit = BitConverter.ToUInt32(bytes, 76);
            SizeOfHeapReserve = BitConverter.ToUInt32(bytes, 80);
            SizeOfHeapCommit = BitConverter.ToUInt32(bytes, 84);
            LoaderFlags = BitConverter.ToUInt32(bytes, 88);
            NumberOfRvaAndSizes = BitConverter.ToUInt32(bytes, 92);
            DataDirectory = new IMAGE_DATA_DIRECTORY[NumberOfRvaAndSizes];
            for (int i = 0; i < NumberOfRvaAndSizes; i++)
                DataDirectory[i] = new IMAGE_DATA_DIRECTORY(bytes.SubArray(96 + 8 * i, 8));
        }

        public string Print()
        {
            var tbl = new ConsoleTables.ConsoleTable("Наименование", "Значение");
            tbl
                .AddRow("Magic", Magic)
                .AddRow("MajorLinkerVersion", MajorLinkerVersion)
                .AddRow("MinorLinkerVersion", MinorLinkerVersion)
                .AddRow("SizeOfCode", SizeOfCode)
                .AddRow("SizeOfInitializedData", SizeOfInitializedData)
                .AddRow("SizeOfUninitializedData", SizeOfUninitializedData)
                .AddRow("AddressOfEntryPoint", AddressOfEntryPoint)
                .AddRow("BaseOfCode", BaseOfCode)
                .AddRow("BaseOfData", BaseOfData)
                .AddRow("ImageBase", ImageBase)
                .AddRow("SectionAlignment", SectionAlignment)
                .AddRow("FileAlignment", FileAlignment)
                .AddRow("MajorOperatingSystemVersion", MajorOperatingSystemVersion)
                .AddRow("MinorOperatingSystemVersion", MinorOperatingSystemVersion)
                .AddRow("MajorImageVersion", MajorImageVersion)
                .AddRow("MinorImageVersion", MinorImageVersion)
                .AddRow("MajorSubsystemVersion", MajorSubsystemVersion)
                .AddRow("MinorSubsystemVersion", MinorSubsystemVersion)
                .AddRow("Win32VersionValue", Win32VersionValue)
                .AddRow("SizeOfImage", SizeOfImage)
                .AddRow("SizeOfHeaders", SizeOfHeaders)
                .AddRow("CheckSum", CheckSum)
                .AddRow("Subsystem", Subsystem)
                .AddRow("DllCharacteristics", DllCharacteristics)
                .AddRow("SizeOfStackReserve", SizeOfStackReserve)
                .AddRow("SizeOfStackCommit", SizeOfStackCommit)
                .AddRow("SizeOfHeapReserve", SizeOfHeapReserve)
                .AddRow("SizeOfHeapCommit", SizeOfHeapCommit)
                .AddRow("LoaderFlags", LoaderFlags)
                .AddRow("NumberOfRvaAndSizes", NumberOfRvaAndSizes);

            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < DataDirectory.Length; i++)
            {
                sb.AppendLine(
                    DataDirectory[i]
                    .Print()
                    .Replace("{i}", i.ToString())
                    );
                sb.AppendLine();
            }

            return "IMAGE_OPTIONAL_HEADER\n" + tbl.ToString() + "\n" + sb.ToString();
        }

        //public string ToString(string tab = null)
        //{
        //    tab = tab ?? string.Empty;
        //    StringBuilder sb = new StringBuilder();

        //    sb.AppendLine();
        //    sb.AppendLine($"{tab}---   IMAGE_OPTIONAL_HEADER   ---");
        //    sb.AppendLine();
        //    sb.AppendFormat("{0}{1,-20} : {2,-20}\n", tab, "Magic",                       Magic);
        //    sb.AppendFormat("{0}{1,-20} : {2,-20}\n", tab, "MajorLinkerVersion",          MajorLinkerVersion);
        //    sb.AppendFormat("{0}{1,-20} : {2,-20}\n", tab, "MinorLinkerVersion",          MinorLinkerVersion);
        //    sb.AppendFormat("{0}{1,-20} : {2,-20}\n", tab, "SizeOfCode",                  SizeOfCode);
        //    sb.AppendFormat("{0}{1,-20} : {2,-20}\n", tab, "SizeOfInitializedData",       SizeOfInitializedData);
        //    sb.AppendFormat("{0}{1,-20} : {2,-20}\n", tab, "SizeOfUninitializedData",     SizeOfUninitializedData);
        //    sb.AppendFormat("{0}{1,-20} : {2,-20}\n", tab, "AddressOfEntryPoint",         AddressOfEntryPoint);
        //    sb.AppendFormat("{0}{1,-20} : {2,-20}\n", tab, "BaseOfCode",                  BaseOfCode);
        //    sb.AppendFormat("{0}{1,-20} : {2,-20}\n", tab, "BaseOfData",                  BaseOfData);
        //    sb.AppendFormat("{0}{1,-20} : {2,-20}\n", tab, "ImageBase",                   ImageBase);
        //    sb.AppendFormat("{0}{1,-20} : {2,-20}\n", tab, "SectionAlignment",            SectionAlignment);
        //    sb.AppendFormat("{0}{1,-20} : {2,-20}\n", tab, "FileAlignment",               FileAlignment);
        //    sb.AppendFormat("{0}{1,-20} : {2,-20}\n", tab, "MajorOperatingSystemVersion", MajorOperatingSystemVersion);
        //    sb.AppendFormat("{0}{1,-20} : {2,-20}\n", tab, "MinorOperatingSystemVersion", MinorOperatingSystemVersion);
        //    sb.AppendFormat("{0}{1,-20} : {2,-20}\n", tab, "MajorImageVersion",           MajorImageVersion);
        //    sb.AppendFormat("{0}{1,-20} : {2,-20}\n", tab, "MinorImageVersion",           MinorImageVersion);
        //    sb.AppendFormat("{0}{1,-20} : {2,-20}\n", tab, "Win32VersionValue",           Win32VersionValue);
        //    sb.AppendFormat("{0}{1,-20} : {2,-20}\n", tab, "SizeOfImage",                 SizeOfImage);
        //    sb.AppendFormat("{0}{1,-20} : {2,-20}\n", tab, "SizeOfHeaders",               SizeOfHeaders);
        //    sb.AppendFormat("{0}{1,-20} : {2,-20}\n", tab, "CheckSum",                    CheckSum);
        //    sb.AppendFormat("{0}{1,-20} : {2,-20}\n", tab, "Subsystem",                   Subsystem);
        //    sb.AppendFormat("{0}{1,-20} : {2,-20}\n", tab, "DllCharacteristics",          DllCharacteristics);
        //    sb.AppendFormat("{0}{1,-20} : {2,-20}\n", tab, "SizeOfStackReserve",          SizeOfStackReserve);
        //    sb.AppendFormat("{0}{1,-20} : {2,-20}\n", tab, "SizeOfStackCommit",           SizeOfStackCommit);
        //    sb.AppendFormat("{0}{1,-20} : {2,-20}\n", tab, "SizeOfHeapReserve",           SizeOfHeapReserve);
        //    sb.AppendFormat("{0}{1,-20} : {2,-20}\n", tab, "SizeOfHeapCommit",            SizeOfHeapCommit);
        //    sb.AppendFormat("{0}{1,-20} : {2,-20}\n", tab, "LoaderFlags",                 LoaderFlags);
        //    sb.AppendFormat("{0}{1,-20} : [", tab, "DataDirectory");
        //    for (int i = 0; i < NumberOfRvaAndSizes; i++)
        //        sb.AppendLine(DataDirectory[i].ToString(tab + tab));
        //    sb.AppendFormat("{0}{1,-20}\n", tab, "]");
        //    sb.AppendLine();
        //    sb.AppendLine($"{tab}---   IMAGE_OPTIONAL_HEADER   ---");
        //    sb.AppendLine();

        //    return sb.ToString();
        //}
    }
}
