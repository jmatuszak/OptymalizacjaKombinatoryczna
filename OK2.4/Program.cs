using System;
using System.Collections.Generic;
using System.Linq;

public class MainClass
{
    public static void Main()
    {
        for (int i = 1; i <= 50; i++)
        {
            var n = 10 * i;
            var matrix = GenerateMatrix(n);
            int[,] matrixCopy = matrix.Clone() as int[,];

            DateTime start;
            DateTime end;
            //TimeSpan ts = (end - start);
            //Console.WriteLine("\n-----------------------------------------");

            //Weighted
            start = DateTime.Now;
            var result2ApproximatedWeighteed = VertexCover2ApproximatedWeightedEdges(matrix);
            end = DateTime.Now;
            TimeSpan tsWeight = (end - start);


            //Not Weighted
            start = DateTime.Now;
            var result2Approximated = VertexCover2Approximated(matrixCopy);
            end = DateTime.Now;
            TimeSpan tsNoWeight = (end - start);


            Console.Write(n + ";" + result2ApproximatedWeighteed.Count + ";");
            Console.Write(result2Approximated.Count + ";");
            Console.Write(tsWeight.TotalMilliseconds + ";");
            Console.WriteLine(tsNoWeight.TotalMilliseconds
                );
        }



        List<int> VertexCover2Approximated(int[,] matrix)
        {

            var random = new Random();
            for (int x = 0; x < 10; x++)
            {
                int r = random.Next(0, 2);
            }
            var n = matrix.GetLength(0);
            List<int> VertexesToCover = new List<int>();
            List<int> CoveredVertexes = new List<int>();
            for (int i = 0; i < matrix.GetLength(0); i++) VertexesToCover.Add(i);
            while (VertexesToCover.Count > 0)
            {
                int u = 0;
                List<int> Neighbours = new List<int>();
                VertexesToCover = UpdateVertexesToCover(VertexesToCover, matrix);
                int count = VertexesToCover.Count;
                if (count == 0) return CoveredVertexes;
                //Finding random vertex
                var v = VertexesToCover[random.Next(0, count)];
                //Finding random neighbour
                for (int k = 0; k < VertexesToCover.Count; k++)
                {
                    u = VertexesToCover[k];
                    if (matrix[v, u] != 0 || matrix[u, v] != 0)
                    {
                        if (u != v && !Neighbours.Contains(u))
                            Neighbours.Add(u);
                    }
                }
                count = Neighbours.Count;
                u = Neighbours[random.Next(0, count)];

                CoveredVertexes.Add(v);
                CoveredVertexes.Add(u);
                for (int k = 0; k < n; k++)
                {
                    matrix[v, k] = 0;
                    matrix[k, v] = 0;
                    matrix[u, k] = 0;
                    matrix[k, u] = 0;
                }
                VertexesToCover = UpdateVertexesToCover(VertexesToCover, matrix);
            }
            return CoveredVertexes;
        }

        List<int> VertexCover2ApproximatedWeightedEdges(int[,] matrix)
        {
            var random = new Random();
            List<int[]> BestEdges;
            List<int> VertexesToCover = new List<int>();
            List<int> CoveredVertexes = new List<int>();
            for (int i = 0; i < matrix.GetLength(0); i++) VertexesToCover.Add(i);
            while (VertexesToCover.Count > 0)
            {
                VertexesToCover = UpdateVertexesToCover(VertexesToCover, matrix);
                if (VertexesToCover.Count == 0) return CoveredVertexes;
                BestEdges = FindMinWeigthEdges(matrix, VertexesToCover);
                if (BestEdges.Count > 0)
                {
                    int randomEdge = random.Next(0, BestEdges.Count);
                    int u = BestEdges[randomEdge][0];
                    int v = BestEdges[randomEdge][1];
                    for (int x = 0; x < VertexesToCover.Count; x++)
                    {
                        int k = VertexesToCover[x];
                        matrix[u, k] = 0;
                        matrix[k, u] = 0;
                        matrix[v, k] = 0;
                        matrix[k, v] = 0;
                    }
                    CoveredVertexes.Add(u);
                    CoveredVertexes.Add(v);
                }
                VertexesToCover = UpdateVertexesToCover(VertexesToCover, matrix);
            }
            return CoveredVertexes;
        }


        List<int> UpdateVertexesToCover(List<int> VertexesToCover, int[,] matrix)
        {
            List<int> TempVertexesToCover = new List<int>();
            for (int i = 0; i < VertexesToCover.Count; i++)
            {
                for (int j = 0; j < VertexesToCover.Count; j++)
                {
                    int u = VertexesToCover[i];
                    int v = VertexesToCover[j];
                    if (matrix[u, v] != 0)
                    {
                        if (!TempVertexesToCover.Contains(u)) TempVertexesToCover.Add(u);
                        if (!TempVertexesToCover.Contains(v)) TempVertexesToCover.Add(v);
                    }
                }

            }
            return TempVertexesToCover;
        }
        List<int[]> FindMinWeigthEdges(int[,] matrix, List<int> VertexesToCover)
        {
            var minWeight = 0;
            List<int[]> BestEdges = new List<int[]>();

            for (int i = 0; i < VertexesToCover.Count; i++)
            {
                for (int j = i + 1; j < VertexesToCover.Count; j++)
                {
                    var v = VertexesToCover[i];
                    var u = VertexesToCover[j];
                    int sumWeight = matrix[v, u] + matrix[u, v];

                    if (sumWeight == 0) break;

                    //Setting minWeight value for the first time
                    if (minWeight == 0)
                    {
                        minWeight = sumWeight;
                        BestEdges.Add(new int[] { u, v });
                    }
                    else if (sumWeight < minWeight)
                    {
                        BestEdges.RemoveRange(0, BestEdges.Count);
                        minWeight = sumWeight;
                        BestEdges.Add(new int[] { u, v });
                    }
                    else if (sumWeight == minWeight)
                    {
                        if (!(BestEdges.Contains(new int[] { u, v })
                            && BestEdges.Contains(new int[] { v, u })))
                            BestEdges.Add(new int[] { u, v });
                    }
                }
            }
            return BestEdges;
        }

        static int[,] ReadMatrix()
        {
            var line = Console.ReadLine();

            var trimmed = line.Split(' ');
            int n = trimmed.Length;
            int[,] matrix = new int[n, n];
            for (int j = 0; j < n; j++)
            {
                matrix[0, j] = int.Parse(trimmed[j]);
            }
            for (int i = 1; i < n; i++)
            {
                line = Console.ReadLine();
                trimmed = line.Split(' ');
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = int.Parse(trimmed[j]);
                }

            }
            return matrix;
        }
        int[,] ReadFromFile(string txt, int n)
        {
            string projectFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\");
            string inputFilePath = Path.Combine(projectFolderPath, txt);

            string input = File.ReadAllText(inputFilePath);

            int i = 0, j = 0;
            int[,] result = new int[n, n];
            foreach (var row in input.Split('\n'))
            {
                j = 0;
                foreach (var col in row.Trim().Split(' '))
                {
                    result[i, j] = int.Parse(col.Trim());
                    j++;
                }
                i++;
            }
            return result;
        }

        void PrintList(List<int> Vertexes)
        {
            foreach (var item in Vertexes)
            {
                Console.Write(item + " ");
            }
        }
        void PrintMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {

                    Console.Write(matrix[i, j]);
                    if (j < matrix.GetLength(1) - 1) Console.Write(" ");
                }
                Console.WriteLine();
            }

        }
        int[,] GenerateMatrix(int n)
        {
            var numbers = new int[] { 0, 0, 0, 0, 1, 2, 3, 1, 1, 0, 0, 0, 0 };
            var random = new Random();
            int[,] matrix = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == j) matrix[i, j] = 0;
                    else
                    {
                        matrix[i, j] = numbers[random.Next(0, numbers.Length)];
                    }
                }
            }
            //PrintMatrix(matrix);
            return matrix;
        }
    }
}