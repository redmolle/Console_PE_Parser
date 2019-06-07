using System;
using System.Collections.Generic;
using System.Linq;
using Console_PE_Parser.Images;
using Console_PE_Parser;
using ConsoleTables;

public static partial class Parser
{
    public static PE pe { get; set; }
    #region FileInfo
    public static List<KeyValuePair<byte, int>> entropy { get; set; }
    public static double entropy_double { get; set; }
    public static int compressed { get { return (int)(entropy_double / 8 * 100); } } 
    #endregion

    public static void CheckPE()
    {
        if (pe == null)
            PE();
    }

    public static void PE(byte[] buf = null)
    {

        buf = buf ?? file;
        if (buf == null)
            throw new Exception("Файл не выбран!");

        if (Packer.CheckApLib(buf))
            buf = DecompressApLib();

        pe = new PE(buf);

    }

    private static string PEPrintMenu()
    {
        var tbl = new ConsoleTable("Кнопка", "Действие");
        tbl
            .AddRow("0", "Целиком")
            .AddRow("1", "Dos заголовок")
            .AddRow("2", "NT заголовок")
            .AddRow("3", "Заголовки секций")
            .AddRow("4", "Импорт");
        Console.WriteLine("\n\n");
        tbl.Write(Format.MarkDown);
        bool IsEnd;
        do
        {
            IsEnd = true;
            Console.Write(Parser.input);
            string cmd = Console.ReadLine();
            switch (cmd)
            {
                case "0": return pe.Print();
                case "1": return pe.dos.Print();
                case "2": return pe.nt.Print();
                case "3": return pe.PrintSections();
                case "4": return pe.PrintImport();
                default:
                    Console.WriteLine($"{Parser.output}Неверная команда \"{cmd}\"");
                    IsEnd = false;
                    break;
            }
            Console.WriteLine("\n\n");
        } while (!IsEnd);
        return string.Empty;
    }

    public static void PrintPE()
    {
        CheckPE();
        string res = PEPrintMenu();
        Console.WriteLine("\n\n");
        if (!string.IsNullOrEmpty(res))
        {
            Console.WriteLine(res);
        }
    }

    public static void Entropy()
    {
        var entr =
            from f in file
            group f by f into g
            select new KeyValuePair<byte, int>(g.Key, g.Count());

        entropy = entr
            .OrderBy(o => o.Key)
            .ToList();

        entropy_double = entropy
            .Select(s => (double)s.Value / file.Length)
            .Sum(m => -m * Math.Log(m, 2));
    }

    public static void ShowEntropy()
    {
        CheckPE();
        
        Console.WriteLine($"{output}Энтропия = {entropy_double}");
        Console.WriteLine($"{output}Сжат на = {compressed}%");
        Entropy ent = new Entropy(entropy);
        ent.ShowDialog();
    }

    public static void ShowFileInfo()
    {
        CheckPE();
        Console.WriteLine($"{output}Полное имя файла = {filePath}");
        Console.WriteLine($"{output}Размер файла = {file.Length}");
        Console.WriteLine($"{output}{pe.PackedStatus}");
        ShowEntropy();
    }
}
