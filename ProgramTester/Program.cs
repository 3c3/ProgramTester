using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestGen;

namespace ProgramTester
{
    class Program
    {
        static void Main(string[] args)
        {
            string checker = null;
            string subject = null;
            string dllName = null;

            bool breakOnBad = false;
            bool showBad = false;

            bool badFlagsSet = false;

            #region Pesky Input

            for (int i = 0; i < args.Length; i++)
            {
                string current = args[i];
                if (current == "-sb")
                {
                    showBad = true;
                    badFlagsSet = true;
                }
                else if (current == "-bob")
                {
                    breakOnBad = true;
                    badFlagsSet = true;
                }
                else if (current == "-d")
                {
                    i++;
                    if (i < args.Length) dllName = args[i];
                    else
                    {
                        Console.WriteLine("You didn't give me a dll, now you have to enter it");
                        break;
                    }
                }
                else if (current == "-c")
                {
                    i++;
                    if (i < args.Length) checker = args[i];
                    else
                    {
                        Console.WriteLine("You didn't give me a checker, now you have to enter it");
                        break;
                    }
                }
                else if (current == "-s")
                {
                    i++;
                    if (i < args.Length) subject = args[i];
                    else
                    {
                        Console.WriteLine("You didn't give me a subject, now you have to enter it");
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("{0} - The fuck is this?!", current);
                }
            }

            if (checker == null)
            {
                Console.WriteLine("Enter the checker: ");
                checker = Console.ReadLine();
            }
            if (subject == null)
            {
                Console.WriteLine("Enter the subject: ");
                subject = Console.ReadLine();
            }
            if (dllName == null)
            {
                Console.WriteLine("Enter the dll: ");
                dllName = Console.ReadLine();
            }

            if (!badFlagsSet)
            {
                Console.WriteLine("Show bad tests?[y/n]");
                showBad = Console.ReadLine()[0] == 'y';

                Console.WriteLine("Break on bad tests?[y/n]");
                breakOnBad = Console.ReadLine()[0] == 'y';
            }            

            // dll path must be absolute
            dllName = Path.GetFullPath(dllName);

            #endregion

            DllLoader loader = new DllLoader(dllName);
            List<ITestGenerator> generators = loader.LoadGenerators();

            GenerationTester tester = new GenerationTester(subject, checker, showBad, breakOnBad);
            tester.generators.AddRange(generators);

            int nTests = 0;
            while (true)
            {
                Console.WriteLine("Enter number of tests: ");
                nTests = int.Parse(Console.ReadLine());

                while (nTests-- > 0) tester.MakeAndRunTest(false);
            }
        }
    }
}
