using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;

namespace Console_PE_Parser.Images.Import
{
    public class WrappedImport
    {
        public IMAGE_IMPORT_DESCRIPTOR descriptor { get; set; }
        public IMAGE_THUNK_DATA[] thunk { get; set; }//iat
        public IMAGE_THUNK_DATA[] lookup { get; set; }//int
        public IMAGE_IMPORT_BY_NAME[] name { get; set; }
        
        public string Print()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(descriptor.Print());
            sb.AppendLine();

            var tbl = new ConsoleTable("Hint", "Name", "Addr");
            for(int i = 0; i < thunk.Length; i++)
            {
                tbl
                    .AddRow(name[i].Hint, Encoding.Default.GetString(name[i].Name), (descriptor.FirstThunk + i*4).ToString("X"));
            }
            

            return sb.ToString() + tbl.ToString();
        }

        //public string ToString(string tab = null)
        //{
        //    tab = tab ?? string.Empty;
        //    StringBuilder sb = new StringBuilder();

        //    sb.AppendLine(descriptor.ToString(tab));
        //    for(int i = 0; i < thunk.Length; i++)
        //    {
        //        sb.AppendLine(thunk[i].ToString(tab + tab));
        //        sb.AppendLine(name[i].ToString(tab + tab));
        //    }

        //    return sb.ToString();
        //}
    }
}
