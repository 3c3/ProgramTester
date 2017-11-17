using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestGen;

namespace ProgramTester
{
    /// <summary>
    /// Tests one program (subject) with another (checker)
    /// Tests are supplied by test generators
    /// </summary>
    public class GenerationTester
    {
        private string subjectFileName;
        private string checkerFileName;

        private bool breakOnBad;
        private bool showBadTests;

        private uint testCounter;

        public GenerationTester(string subject, string checker, bool showBad, bool breakOnBad)
        {
            this.subjectFileName = subject;
            this.checkerFileName = checker;
            this.breakOnBad = breakOnBad;
            showBadTests = showBad;
        }

        public List<ITestGenerator> generators = new List<ITestGenerator>();
        private Random random = new Random();

        public void MakeAndRunTest(bool showInput)
        {
            string testString = generators[random.Next(generators.Count)].MakeTest();

            if (showInput)
            {
                Console.WriteLine("Test:");
                Console.WriteLine(testString);
            }            
            RunTest(testString);
        }

        private void RunTest(string testData)
        {
            Executable subject = new Executable(subjectFileName);
            Executable checker = new Executable(checkerFileName);

            checker.Start();
            checker.Feed(testData);
            string expectedString = checker.GetOutput().Trim(new char[] { '\n', '\r', ' ' });

            subject.Start();
            subject.Feed(testData);
            string subjectString = subject.GetOutput().Trim(new char[] { '\n', '\r', ' ' });

            double expected, real;
            if (double.TryParse(expectedString.Substring(0, expectedString.Length-1).Replace('.', ','), out expected) && 
                double.TryParse(subjectString.Substring(0, subjectString.Length - 1).Replace('.', ','), out real))
            {
                double diff = real - expected;
                double pDiff = diff * 100.0 / expected;

                bool bad = (Math.Abs(diff) > 0.05) && (Math.Abs(pDiff) > 10.0);

                if (bad)
                {
                    ConsoleColor orig = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("{0}. ", testCounter++);
                    Console.WriteLine("еxpected: {1:F3}, got: {0:F3}, diff: {2:F3} ({3:F0}%)",
                    real, expected, diff, pDiff);
                    Console.ForegroundColor = orig;

                    if (showBadTests)
                    {
                        Console.WriteLine("Test:");
                        Console.WriteLine(testData);
                    }
                    
                    if (breakOnBad)
                    {
                        Console.WriteLine("Press ENTER to continue testing");
                        Console.ReadLine();
                    }                    
                }
                else
                {
                    ConsoleColor orig = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("{0}. ", testCounter++);
                    Console.WriteLine("ok({0:F0}%)\t\t{1:F3} / {2:F3}", pDiff, expected, real);
                    Console.ForegroundColor = orig;
                }
                
            }
            else
            {
                if (expectedString == subjectString)
                {
                    ConsoleColor orig = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("{0}. ", testCounter++);
                    Console.WriteLine("ok\t{0} / {1}", expectedString, subjectString);
                    Console.ForegroundColor = orig;
                }
                else
                {
                    ConsoleColor orig = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("{0}. ", testCounter++);
                    Console.WriteLine("fail\t{0} / {1}", expectedString, subjectString);
                    Console.ForegroundColor = orig;

                    if (showBadTests)
                    {
                        Console.WriteLine("Test:");
                        Console.WriteLine(testData);
                    }                    

                    if (breakOnBad)
                    {
                        Console.WriteLine("Press ENTER to continue testing");
                        Console.ReadLine();
                    }
                    
                }
            }
        }
    }
}
