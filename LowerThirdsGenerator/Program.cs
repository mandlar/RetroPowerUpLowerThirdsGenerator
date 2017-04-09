using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LowerThirdsGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new Options();
            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {
                if (options.Verbose) 
                    Console.WriteLine("Filename: {0}", options.InputFile);

                if (!File.Exists(options.InputFile))
                {
                    Console.WriteLine("Input file does not exist!");
                    return;
                }

                // parse lower thirds text file
                // expected format: three lines of text, one line of empty text
                string[] lines = File.ReadAllLines(options.InputFile);
            }
        }
    }
}
