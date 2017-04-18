using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Svg;

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

                if (!File.Exists(options.TemplateFile))
                {
                    Console.WriteLine("Template file does not exist!");
                    return;
                }

                // parse lower thirds text file
                // expected format: up to three lines of text, one line of empty text
                Console.WriteLine("Parsing input file...");
                string[] lines = File.ReadAllLines(options.InputFile);

                List<LowerThird> lowerThirds = new List<LowerThird>();

                LowerThird currentLowerThird = new LowerThird();

                foreach (string line in lines)
                {
                    string newLine = SecurityElement.Escape(line); // encode special characters for XML (SVG uses XML formatting)

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

                            if(currentLowerThird.LineOne.Length > 40 || currentLowerThird.LineTwo.Length > 80 || currentLowerThird.LineThree.Length > 90)
                                Console.WriteLine("WARNING: " + currentLowerThird.GetLowerThirdFileName() + " might have cut off text!");

                            lowerThirds.Add(currentLowerThird);
                            currentLowerThird = new LowerThird();
                        }
                    }
                    else if (string.IsNullOrWhiteSpace(currentLowerThird.LineOne))
                    {
                        currentLowerThird.LineOne = newLine;
                    }
                    else if (string.IsNullOrWhiteSpace(currentLowerThird.LineTwo))
                    {
                        currentLowerThird.LineTwo = newLine;
                    }
                    else if (string.IsNullOrWhiteSpace(currentLowerThird.LineThree))
                    {
                        currentLowerThird.LineThree = newLine;
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

                    if (currentLowerThird.LineOne.Length > 40 || currentLowerThird.LineTwo.Length > 80 || currentLowerThird.LineThree.Length > 90)
                        Console.WriteLine("WARNING: " + currentLowerThird.GetLowerThirdFileName() + " might have cut off text!");

                    lowerThirds.Add(currentLowerThird);
                }

                // read in the template SVG vector file and create a vector file for each lower third
                Console.WriteLine("Generating SVG and PNG files...");

                string templateSVG = File.ReadAllText(options.TemplateFile);

                foreach (LowerThird lowerThird in lowerThirds)
                {
                    // Convert three dashes to an empty line. This is to prevent the program from creating a new Lower Third due to an empty line when you *really* want an empty line (e.g. Lines 1 and 3 have text, but not Line 2)
                    if (lowerThird.LineOne == "---")
                        lowerThird.LineOne = "";
                    if (lowerThird.LineTwo == "---")
                        lowerThird.LineTwo = "";
                    if (lowerThird.LineThree == "---")
                        lowerThird.LineThree = "";

                    string newSVG = templateSVG;

                    newSVG = newSVG.Replace("[LINE ONE]", lowerThird.LineOne);
                    newSVG = newSVG.Replace("[LINE TWO]", !string.IsNullOrWhiteSpace(lowerThird.LineTwo) ? lowerThird.LineTwo : "");
                    newSVG = newSVG.Replace("[LINE THREE]", !string.IsNullOrWhiteSpace(lowerThird.LineOne) ? lowerThird.LineThree : "");

                    string fileNameSVG = lowerThird.GetLowerThirdFileName() + ".svg";
                    string fileNamePNG = lowerThird.GetLowerThirdFileName() + ".png";

                    if (File.Exists(fileNameSVG) && !options.Overwrite)
                    {
                        Console.WriteLine("ERROR: " + fileNameSVG + " already exists! SVG not generated. To overwrite the file, use -o true");
                    }
                    else
                    {
                        FileStream file = File.Create(fileNameSVG);
                        file.Close();

                        File.WriteAllText(fileNameSVG, newSVG);

                        if (options.Verbose)
                            Console.WriteLine("Writing lower third SVG to " + fileNameSVG);
                    }

                    if (File.Exists(fileNamePNG) && !options.Overwrite)
                    {
                        Console.WriteLine("ERROR: " + fileNamePNG + " already exists! PNG not generated. To overwrite the file, use -o true");
                    }
                    else
                    {
                        SvgDocument svgDocument = SvgDocument.Open(fileNameSVG);
                        Bitmap bitmap = svgDocument.Draw();
                        bitmap.Save(fileNamePNG, ImageFormat.Png);

                        if (options.Verbose)
                            Console.WriteLine("Writing lower third PNG to " + fileNamePNG);
                    }
                }

                Console.WriteLine("Done!");
            }
        }
    }
}
