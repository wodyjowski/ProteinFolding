using System;

namespace ProteinFolding
{
    public class Algorithm
    {
        public string Input { get; }
        public Algorithm(string input)
        {
            Input = input ?? throw new System.ArgumentNullException(nameof(input));
        }

        public (string, int) Calculate()
        {
            throw new NotImplementedException();
        }

    }
}