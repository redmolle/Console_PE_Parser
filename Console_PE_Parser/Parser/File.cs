using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ConsoleTables;
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics;
using System.Threading;

public static partial class Parser
{
    #region UserInterface
    public static string input { get { return ">>   "; } }
    public static string output { get { return "<<   "; } }
    #endregion

    #region File
    private static string fileFilter { get { return "exe files (*.exe)|*.exe|sys file (*.sys)|*.sys|dll files (*.dll)|*.dll|All files (*.*)|*.*"; } }
    public static byte[] file { get; private set; }
    public static string filePath { get; private set; } 
    #endregion

    public static byte[] DecompressApLib()
    {
        string decomressedFile = filePath.Replace(".exe", ".DecompressedApLib.exe");

        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.CreateNoWindow = false;
        startInfo.UseShellExecute = false;
        startInfo.FileName = "appack.exe";
        startInfo.WindowStyle = ProcessWindowStyle.Hidden;
        startInfo.Arguments = $"d \"{filePath}\" \"{decomressedFile}\"";

        Console.WriteLine($"{output}Начата распаковка ApLib\n------------ Инфо распаковки ----------");
        using (Process exeProcess = Process.Start(startInfo))
        {
            exeProcess.WaitForExit();
            Console.WriteLine("------------ Инфо распаковки ----------");
            filePath = decomressedFile;
            return ReadFile(filePath);
        }
    }

    #region FileWork
    public static byte[] ReadFile(string addr = null)
    {
        if (string.IsNullOrEmpty(addr))
            return new byte[1];
        FileInfo f = new FileInfo(addr);
        long size = f.Length;
        byte[] buf = new byte[size];
        using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            fs.Read(buf, 0, Convert.ToInt32(size));
            return buf;
        }
    }

    public static bool OpenFile(string addr = null)
    {
        if (string.IsNullOrEmpty(addr))
        {
            OpenFileDialog dlg = new OpenFileDialog { Filter = fileFilter };
            if (dlg.ShowDialog() == DialogResult.OK)
                filePath = dlg.FileName;
            else
                return false;
        }
        filePath = addr ?? filePath;

        file = ReadFile(filePath);
        pe = null;
        Entropy();
        Console.WriteLine($"{output}Выбран файл {filePath}");
        return true;

    } 
    #endregion

    public static void SavePEParsed()
    {
        if (pe == null)
            PE();

        SaveFileDialog dlg = new SaveFileDialog() { Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*", FileName = "Entropy" };
        if(dlg.ShowDialog() == DialogResult.OK)
        {
            string _filePath = dlg.FileName;
            File.WriteAllText(_filePath, pe.Print() + $"\n\nЭнтропия = {entropy_double}");

            Console.WriteLine("Файл успешно сохранен\n\n");
        }
    }
    
}
