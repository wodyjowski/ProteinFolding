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

        public (AminoAcid, int result) FindBestResult(int maxGenerations)
        {
            AminoAcid bestProtein = null;
            int bestResult = 0;

            for (int i = 0; i < maxGenerations; i++)
            {
                AminoAcid generated = GenerateProtein();
                int genPower = GetProteinValue(generated);
                if(genPower > bestResult)
                {
                    bestResult = genPower;
                    bestProtein = generated;
                }
            }

            return (bestProtein, bestResult);
        }

        public int GetProteinValue(AminoAcid proteinHead)
        {
            int result = 0;
            AminoAcid node = proteinHead;
            while(node != null)
            {
                if(node.Type == AcidType.H)
                {
                    AminoAcid testNode = proteinHead;
                    while(testNode != null)
                    {
                        if(testNode.Type == AcidType.H && testNode != node
                            && node != testNode.Previous && testNode != node.Previous)
                        {
                            int testValue = Math.Abs((node.X - testNode.X)) + Math.Abs((node.Y - testNode.Y));
                            if(testValue == 1)
                            {
                                ++result;
                            }
                        }
                        testNode = testNode.Previous;
                    }
                }
                node = node.Previous;
            }
            return result/2;
        }

        public AminoAcid GenerateProtein()
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