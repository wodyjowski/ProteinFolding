using System;
using System.Text.RegularExpressions;

namespace ProteinFolding
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = ReadInput();
            Algorithm algorithm = new Algorithm(input);
            (string resultFold, int power) output = algorithm.Calculate();
        }

        private static string ReadInput()
        {
            System.Console.WriteLine("Input string (P/H):");
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                return null;

            input = input.ToUpper();
            if(!Regex.IsMatch(input, "^[HP]*"))
            {
                return null;
            }
            return input;
        }
    }
}
