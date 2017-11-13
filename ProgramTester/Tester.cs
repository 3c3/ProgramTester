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
        private static double maxLength = 20;

        private string program1 = "task7.exe";
        private string program2 = "pixel.exe";

        public void MakeAndRunTest()
        {
            Random random = new Random();
            StringBuilder test = new StringBuilder();

            double length = random.NextDouble() * maxLength;
            double x = random.NextDouble() * 20 - 10;
            double y = random.NextDouble() * 20 - 10;

            test.AppendFormat("{0} {1}\n", x.ToString(CultureInfo.InvariantCulture), y.ToString(CultureInfo.InvariantCulture));
            test.AppendFormat("{0} {1}\n", (x+length).ToString(CultureInfo.InvariantCulture), y.ToString(CultureInfo.InvariantCulture));
            x += length / 2.0;
            y += Math.Sqrt(3) * length / 2.0;
            test.AppendFormat("{0} {1}\n", x.ToString(CultureInfo.InvariantCulture), y.ToString(CultureInfo.InvariantCulture));

            length = random.NextDouble() * maxLength;
            x = random.NextDouble() * 20 - 10;
            y = random.NextDouble() * 20 - 10;

            test.AppendFormat("{0} {1}\n", x.ToString(CultureInfo.InvariantCulture), y.ToString(CultureInfo.InvariantCulture));
            test.AppendFormat("{0} {1}\n", (x + length).ToString(CultureInfo.InvariantCulture), y.ToString(CultureInfo.InvariantCulture));
            x += length / 2.0;
            y += Math.Sqrt(3) * length / 2.0;
            test.AppendFormat("{0} {1}\n", x.ToString(CultureInfo.InvariantCulture), y.ToString(CultureInfo.InvariantCulture));

            string testString = test.ToString();
            Console.WriteLine("Test:");
            Console.WriteLine(testString);
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
                Console.WriteLine("Got: {0:F3}, expected: {1:F3}, diff: {2:F3} ({3:F0}%)",
                    real, expected, real - expected, (real - expected) * 100.0 / expected);
            }
            else
            {
                Console.WriteLine("Failed to parse");
                Console.WriteLine(expectedString);
                Console.WriteLine(subjectString);
            }
        }
    }
}
