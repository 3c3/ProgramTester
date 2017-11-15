﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramTester
{
    public class CoolTriangleGenerator : ITestGenerator
    {
        private static double maxLength = 20;

        public string MakeData()
        {
            Random random = new Random();
            StringBuilder test = new StringBuilder();

            double length = random.NextDouble() * maxLength;
            double x = random.NextDouble() * 15 - 7;
            double y = random.NextDouble() * 15 - 7;

            test.AppendFormat("{0} {1}\n", x.ToString(CultureInfo.InvariantCulture), y.ToString(CultureInfo.InvariantCulture));
            test.AppendFormat("{0} {1}\n", (x + length).ToString(CultureInfo.InvariantCulture), y.ToString(CultureInfo.InvariantCulture));
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

            return test.ToString();
        }
    }
}
