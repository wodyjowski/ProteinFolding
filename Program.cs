using System;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace ProteinFolding
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true){

            
            string input = ReadInput();
            Algorithm algorithm = new Algorithm(input);
            // Stopwatch sw = new Stopwatch();
            // sw.Start();
            // int proteinPower = algorithm.GetProteinValue(outProtein);
            // sw.Stop();

            (AminoAcid proteinHead, int proteinValue) result = algorithm.FindBestResult(100);

            PrintProtein(result.proteinHead, input.Length);
            
            Console.WriteLine($"value: {result.proteinValue}");
            }
        }

        private static void PrintProtein(AminoAcid proteinHead, int size)
        {
            StringBuilder sb = new StringBuilder();
            AminoAcid node = proteinHead;
            string[,] stringArray = new string[size * 2, size * 2];
            int halfSize = size;

            while (node != null)
            {
                string nodeName = node.Type.ToString() + size;
                stringArray[node.X + halfSize, node.Y + halfSize ] = nodeName;
                node = node.Previous;
                --size;
            }

            for (int i = 0; i < stringArray.GetLength(0); i++)
            {
                for (int j = 0; j < stringArray.GetLength(1); j++)
                {
                    if (stringArray[i, j] != null)
                    {
                        sb.Append(stringArray[i, j].PadRight(3));
                    }
                    else
                    {
                        sb.Append(' ', 3);
                    }
                }
                sb.Append(Environment.NewLine);
            }

            string output = sb.ToString();
            System.Console.WriteLine(output);
        }

        private static string ReadInput()
        {
            System.Console.WriteLine("Input string (P/H):");
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                return null;

            input = input.ToUpper();
            if (!Regex.IsMatch(input, "^[HP]*"))
            {
                return null;
            }
            return input;
        }
    }
}
