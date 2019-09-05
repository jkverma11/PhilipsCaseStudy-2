using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextWriterLib;
using DataModelsLib;
using System.Reflection;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            TextWriter txt = new TextWriter();
            List<DataModel> lst = new List<DataModel>();
            var dta = new DataModel();
            dta.ErrorCertainity = 12;
            dta.ErrorCount = 12;
            lst.Add(dta);
            txt.Write(lst);
            txt.Write(lst);

            
        }
        
    }
}
