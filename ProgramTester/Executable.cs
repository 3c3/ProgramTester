using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramTester
{
    public class Executable
    {
        private Process process;
        private string filename;

        private StreamWriter input;
        private StreamReader output;

        public Executable(string filename)
        {
            this.filename = filename;
        }

        public void Start()
        {
            process = new Process();

            process.StartInfo.FileName = filename;

            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.ErrorDialog = false;

            process.Start();

            input = process.StandardInput;
            output = process.StandardOutput;

            
        }

        public void Feed(string data)
        {
            input.Write(data);
        }

        public string GetOutput()
        {
            process.WaitForExit();
            string result = output.ReadToEnd();

            int newLineIdx = result.LastIndexOf('\n', result.Length-2);
            if (newLineIdx == -1) return result;

            return result.Substring(newLineIdx + 1);
        }
    }
}
