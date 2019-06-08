using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;

namespace ConsoleApplication3
{
    class Program
    {
        static string UseDefaultFilePath { get { return @"C:\Users\redmo\Documents\Study\Uni\8\Компьютерная криминалистика\PE\Detect Import\pe-tut06.exe"; } }

        static string ChooseCmd()
        {
            var tbl = new ConsoleTable("Кнопка", "Действие");
            tbl
                .AddRow("0", "Выход")
                .AddRow("1", "Выбор файл")
                .AddRow("2", "Использовать файл по умолчанию")
                .AddRow("3", "Сканировать файл")
                .AddRow("4", "Печать PE")
                .AddRow("5", "Инфо о файле")
                .AddRow("6", "Энтропия")
                .AddRow("7", "Сохранить в файл");
            tbl.Write(Format.MarkDown);

            Console.Write(Parser.input);
            return Console.ReadLine();

        }

        static void StayOpen()
        {
            while (true)
            {
                Console.ReadKey();
            }
        }




        [STAThread]
        static void Main(string[] args)
        {
            bool isEnd = false;
            while (!isEnd)
            {
                try
                {
                    string cmd = ChooseCmd();

                    switch (cmd)
                    {
                        case "0":
                            isEnd = true;
                            break;
                        case "1":
                            Parser.OpenFile();
                            break;
                        case "2":
                            Parser.OpenFile(UseDefaultFilePath);
                            break;
                        case "3":
                            Parser.PE();
                            break;
                        case "4":
                            Parser.PrintPE();
                            break;
                        case "5":
                            Parser.ShowFileInfo();
                            break;
                        case "6":
                            Parser.ShowEntropy();
                            break;
                        case "7":
                            Parser.SavePEParsed();
                            break;
                        default:
                            Console.WriteLine($"{Parser.output}Неверная команда \"{cmd}\"");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{Parser.output}{ex.Message}");
                }
                Console.WriteLine("\n\n");
            }
            StayOpen();
        }
    }
}