using CLOC_Tools.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CLOC_Tools
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args is null)
                throw new ArgumentNullException(nameof(args));

            LOCCounter counter = new LOCCounter();

            while (true)
            {
                Console.WriteLine("[Current dir] |" + counter.CurrentDir);
                Console.Write("> ");
                var cmd = Console.ReadLine();

                if (cmd == "exit")
                    break;
                Console.Clear();
                counter.Command(cmd);
            }
        }
    }
}
