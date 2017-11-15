using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramTester
{
    public class Tester
    {
        private string program1 = "task7.exe";
        private string program2 = "pixel.exe";

        public List<ITestGenerator> generators = new List<ITestGenerator>();
        private Random random = new Random();

        public void MakeAndRunTest(bool showInput)
        {
            string testString = generators[random.Next(generators.Count)].MakeData();

            if (showInput)
            {
                Console.WriteLine("Test:");
                Console.WriteLine(testString);
            }            
            RunTest(testString);
        }

        private void RunTest(string testData)
        {
            Executable subject = new Executable(program1);
            Executable checker = new Executable(program2);

            checker.Start();
            checker.Feed(testData);
            string expectedString = checker.GetOutput();

            subject.Start();
            subject.Feed(testData);
            string subjectString = subject.GetOutput();

            double expected, real;
            if (double.TryParse(expectedString.Substring(0, expectedString.Length-1).Replace('.', ','), out expected) && double.TryParse(subjectString.Substring(0, subjectString.Length - 1).Replace('.', ','), out real))
            {
                double diff = real - expected;
                double pDiff = diff * 100.0 / expected;

                bool bad = (Math.Abs(diff) > 0.05) && (Math.Abs(pDiff) > 10.0);

                if (bad)
                {
                    ConsoleColor orig = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Got: {0:F3}, expected: {1:F3}, diff: {2:F3} ({3:F0}%)",
                    real, expected, diff, pDiff);
                    Console.ForegroundColor = orig;

                    Console.WriteLine("Test:");
                    Console.WriteLine(testData);

                    Console.WriteLine("Press ENTER to continue testing");
                    Console.ReadLine();
                }
                else
                {
                    ConsoleColor orig = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("ok({0:F0}%)\t\t{1:F3} / {2:F3}", pDiff, real, expected);
                    Console.ForegroundColor = orig;
                }
                
            }
            else
            {
                if (expectedString == subjectString)
                {
                    ConsoleColor orig = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("ok\t{0} / {1}", expectedString, subjectString);
                    Console.ForegroundColor = orig;
                }
                else
                {
                    ConsoleColor orig = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("fail\t{0} / {1}", expectedString, subjectString);
                    Console.ForegroundColor = orig;

                    Console.WriteLine("Test:");
                    Console.WriteLine(testData);

                    Console.WriteLine("Press ENTER to continue testing");
                    Console.ReadLine();
                }
            }
        }
    }
}
