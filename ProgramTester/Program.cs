using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramTester
{
    class Program
    {
        static void Main(string[] args)
        {
            string checker, subject;

            if (args.Length == 2)
            {
                Console.WriteLine("Choose checker:\n[0]{0}\n[1]{1}", args[0], args[1]);
                int checkerId = int.Parse(Console.ReadLine());
                checker = args[checkerId];
                subject = args[checkerId ^ 1];
            }
            else
            {
                Console.WriteLine("Enter checker:");
                checker = Console.ReadLine();
                Console.WriteLine("Enter test subject:");
                subject = Console.ReadLine();
            }

            bool showBad = false;
            bool breakOnBad = false;

            Console.WriteLine("Show bad tests?[y/n]");
            showBad = Console.ReadLine()[0] == 'y';

            Console.WriteLine("Break on bad tests?[y/n]");
            breakOnBad = Console.ReadLine()[0] == 'y';

            Tester tester = new Tester(subject, checker, showBad, breakOnBad);

            Console.WriteLine("Include triangles that meet the requirements?[y/n]");
            if (Console.ReadLine()[0]=='y') tester.generators.Add(new CoolTriangleGenerator());

            Console.WriteLine("Include random triangles?[y/n]");
            if (Console.ReadLine()[0] == 'y') tester.generators.Add(new RandomTriangleGenerator());

            Console.WriteLine("Include jewish triangles?[y/n]");
            if (Console.ReadLine()[0] == 'y') tester.generators.Add(new JewTriangleGenerator());

            int nTests = 0;
            while (true)
            {
                Console.WriteLine("Enter number of tests to do: ");
                nTests = int.Parse(Console.ReadLine());
                while (nTests-- > 0) tester.MakeAndRunTest(false);
            }
        }
    }
}
