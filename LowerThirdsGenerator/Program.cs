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
                // expected format: up to three lines of text, one line of empty text
                string[] lines = File.ReadAllLines(options.InputFile);

                List<LowerThird> lowerThirds = new List<LowerThird>();

                LowerThird currentLowerThird = new LowerThird();

                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) // empty space line signifies starting a new lower third
                    {
                        if (!string.IsNullOrWhiteSpace(currentLowerThird.LineOne)) // only create new instance if it has at least one line filled in
                        {
                            if (options.Verbose)
                            {
                                Console.WriteLine("Lower third parsed:");
                                Console.WriteLine(currentLowerThird.LineOne);
                                Console.WriteLine(currentLowerThird.LineTwo);
                                Console.WriteLine(currentLowerThird.LineThree);
                                Console.WriteLine("-----");
                            }

                            lowerThirds.Add(currentLowerThird);
                            currentLowerThird = new LowerThird();
                        }
                    }
                    else if (string.IsNullOrWhiteSpace(currentLowerThird.LineOne))
                    {
                        currentLowerThird.LineOne = line;
                    }
                    else if (string.IsNullOrWhiteSpace(currentLowerThird.LineTwo))
                    {
                        currentLowerThird.LineTwo = line;
                    }
                    else if (string.IsNullOrWhiteSpace(currentLowerThird.LineThree))
                    {
                        currentLowerThird.LineThree = line;
                    }
                }

                // check if there is one more lower third to add (if the file didn't end with an empty line)
                if (!string.IsNullOrWhiteSpace(currentLowerThird.LineOne))
                {
                    if (options.Verbose)
                    {
                        Console.WriteLine("Lower third parsed:");
                        Console.WriteLine(currentLowerThird.LineOne);
                        Console.WriteLine(currentLowerThird.LineTwo);
                        Console.WriteLine(currentLowerThird.LineThree);
                        Console.WriteLine("-----");
                    }

                    lowerThirds.Add(currentLowerThird);
                }
            }
        }
    }
}
