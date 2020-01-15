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

        public AminoAcid Calculate()
        {
            AminoAcid node = new AminoAcid(Input[0]);
            Random random = new Random();

            for (int i = 1; i < Input.Length; i++)
            {
                AminoAcid newNode = new AminoAcid(Input[i])
                {
                    X = node.X,
                    Y = node.Y
                };
                int rvalue = random.Next(4);
                switch (rvalue)
                {
                    case 0:
                        ++newNode.X;
                        break;
                    case 1:
                        --newNode.X;
                        break;
                    case 2:
                        ++newNode.Y;
                        break;
                    case 3:
                        --newNode.Y;
                        break;
                }
                AminoAcid testNode = node;
                bool notRepeated = true;
                while(testNode != null)
                {
                    if(testNode.Equals(newNode))
                    {
                        notRepeated = false;
                        --i;
                        break;
                    }
                    testNode = testNode.Previous;
                }

                if(notRepeated)
                {
                    newNode.Previous = node;
                    node = newNode;
                }
            }
            return node;
        }

    }
}