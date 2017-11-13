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
            while (true)
            {
                Console.ReadLine();
                tester.MakeAndRunTest();
            }
        }
    }
}
