using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProteinFolding
{
    public class Algorithm
    {
        public string Input { get; }
        public Algorithm(string input)
        {
            Input = input ?? throw new System.ArgumentNullException(nameof(input));
        }

        public (AminoAcid, int result) FindBestResultInTime(int maxTimeMs)
        {
            Task[] taskArray = new Task[Environment.ProcessorCount];


            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;

            for (int i = 0; i < taskArray.Length; i++)
            {
                taskArray[i] =
                Task.Run(() => RandomSearchInTime(token), token);
            }

            Task.WaitAll(taskArray, maxTimeMs);
            source.Cancel();

            Console.WriteLine($"iterations: {iterations}");
            return (bestProtein, bestResult);
        }

        public (AminoAcid, int result) FindBestResult(int maxGenerations)
        {
            iterations = maxGenerations;

            Task[] taskArray = new Task[8];

            for (int i = 0; i < taskArray.Length; i++)
            {
                taskArray[i] =
                Task.Run(() => RandomSearch());
            }

            Task.WaitAll(taskArray);

            return (bestProtein, bestResult);
        }


        AminoAcid bestProtein = null;
        int bestResult = 0;
        object lockObj = new object();
        long iterations = 0;

        public void RandomSearch()
        {
            while (true)
            {
                AminoAcid generated = GenerateProtein();
                if(generated == null)
                {
                    continue;
                }

                int genPower = GetProteinValue(generated);
                lock (lockObj)
                {
                    --iterations;
                    if (genPower > bestResult)
                    {
                        bestResult = genPower;
                        bestProtein = generated;
                    }
                    if (iterations <= 0)
                        break;
                }
            }
        }

        public void RandomSearchInTime(CancellationToken cancelToken)
        {
            while (true)
            {
                if (cancelToken.IsCancellationRequested)
                {
                    return;
                }

                AminoAcid generated = GenerateProtein();
                if (generated == null)
                {
                    continue;
                }

                int genPower = GetProteinValue(generated);
                lock (lockObj)
                {
                    ++iterations;
                    if (genPower > bestResult)
                    {
                        bestResult = genPower;
                        bestProtein = generated;
                    }
                }
            }
        }

        public int GetProteinValue(AminoAcid proteinHead)
        {
            int result = 0;
            AminoAcid node = proteinHead;
            while (node != null)
            {
                if (node.Type == AcidType.H)
                {
                    AminoAcid testNode = proteinHead;
                    while (testNode != null)
                    {
                        if (testNode.Type == AcidType.H && testNode != node
                            && node != testNode.Previous && testNode != node.Previous)
                        {
                            int testValue = Math.Abs((node.X - testNode.X)) + Math.Abs((node.Y - testNode.Y));
                            if (testValue == 1)
                            {
                                ++result;
                            }
                        }
                        testNode = testNode.Previous;
                    }
                }
                node = node.Previous;
            }
            return result / 2;
        }

        public AminoAcid GenerateProtein()
        {
            AminoAcid node = new AminoAcid(Input[0]);
            Random random = new Random();

            int rvalue = -1;

            // To check if node is not locked
            int count = 0;

            for (int i = 1; i < Input.Length; i++)
            {
                if(count == 4)
                {
                    return null;
                }

                AminoAcid newNode = new AminoAcid(Input[i])
                {
                    X = node.X,
                    Y = node.Y
                };   
                if(rvalue == -1)
                {
                    do
                    {
                        rvalue = random.Next(4);
                    } while (rvalue == node.PrevDirection);
                }
                else
                {
                    rvalue = (rvalue + 1) % 4;
                }
                switch (rvalue)
                {
                    case 0:
                        newNode.Up();
                        break;
                    case 1:
                        newNode.Down();
                        break;
                    case 2:
                        newNode.Left();
                        break;
                    case 3:
                        newNode.Right();
                        break;
                }
                AminoAcid testNode = node;
                bool notRepeated = true;
                while (testNode != null)
                {
                    if (testNode.Equals(newNode))
                    {
                        ++count;
                        notRepeated = false;
                        --i;
                        break;
                    }
                    testNode = testNode.Previous;
                }

                if (notRepeated)
                {
                    newNode.Previous = node;
                    node = newNode;
                    rvalue = -1;
                }
            }
            return node;
        }

    }
}