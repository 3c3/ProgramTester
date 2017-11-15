using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramTester
{
    class RandomTriangleGenerator : ITestGenerator
    {
        private Random random = new Random();

        public string MakeData()
        {
            Random random = new Random();
            StringBuilder test = new StringBuilder();

            AddTriangle(test);
            AddTriangle(test);
            
            return test.ToString();
        }

        private void AddTriangle(StringBuilder builder)
        {
            double x;
            double y;
            for (int i = 0; i < 3; i++)
            {
                x = random.NextDouble() * 15 - 7;
                y = random.NextDouble() * 15 - 7;
                builder.AppendFormat("{0} {1}\n", x.ToString(CultureInfo.InvariantCulture), y.ToString(CultureInfo.InvariantCulture));
            }
        }
    }
}
