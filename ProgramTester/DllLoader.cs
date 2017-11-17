using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestGen;

namespace ProgramTester
{
    public class DllLoader
    {
        private Assembly dll;

        public DllLoader(string fileName)
        {
            dll = Assembly.LoadFile(fileName);
        }

        public List<ITestGenerator> LoadGenerators()
        {
            List<ITestGenerator> generators = new List<ITestGenerator>();

            uint idx = 0;

            foreach (Type type in dll.GetTypes())
            {
                if (type.GetInterface("TestGen.ITestGenerator") != null)
                {
                    ITestGenerator current = (ITestGenerator)Activator.CreateInstance(type);
                    Console.WriteLine("{0}. {1}: {2}", idx++, current.DisplayName, current.Description);
                    generators.Add(current);
                }
            }

            List<ITestGenerator> result = new List<ITestGenerator>();

            while (true)
            {
                Console.WriteLine("Enter a non-empty selection of generators: ");
                string[] parts = Console.ReadLine().Split(',');
                if (parts.Length == 0) continue;

                foreach (string selectedIdx in parts)
                {
                    result.Add(generators[int.Parse(selectedIdx)]);
                }

                break;
            }

            return result;
        }
    }
}
