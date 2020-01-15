using System;
using System.Text;
using System.Text.RegularExpressions;

namespace ProteinFolding
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = ReadInput();
            Algorithm algorithm = new Algorithm(input);
            AminoAcid output = algorithm.Calculate();
            PrintProtein(output, input.Length);
        }

        private static void PrintProtein(AminoAcid proteinHead, int size)
        {
            StringBuilder sb = new StringBuilder();
            AminoAcid node = proteinHead;
            string[,] charArray = new string[size * 2, size * 2];
            int halfSize = size / 2;

            while (node != null)
            {
                string nodeName = node.Type.ToString() + size;
                charArray[node.X + halfSize + 1, node.Y + halfSize + 1] = nodeName;
                node = node.Previous;
                --size;
            }

            for (int i = 0; i < charArray.GetLength(0); i++)
            {
                for (int j = 0; j < charArray.GetLength(1); j++)
                {
                    if (charArray[i, j] != null)
                    {
                        sb.Append(charArray[i, j].PadRight(3));
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
