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
            Tester tester = new Tester();
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
