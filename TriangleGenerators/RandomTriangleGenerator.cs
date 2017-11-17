using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TestGen;

namespace TriangleGenerators
{
    class RandomTriangleGenerator : ITestGenerator
    {
        private Random random = new Random();

        public string DisplayName
        {
            get { return "RandomTriangleGenerator"; }
        }

        public string Description
        {
            get { return "Makes random triangles"; }
        }

        public string MakeTest()
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
