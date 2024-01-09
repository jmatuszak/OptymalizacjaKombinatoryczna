using System;
using System.Collections.Generic;
using System.Linq;

public class MainClass
{
    public static void Main()
    {

        var n = 2500;
        var matrixE = GenerateMatrix(n);
        var matrixG = new int[n, n];
        matrixG = matrixE.Clone() as int[,];
        //2-Przybliżony
        //var matrix1 = ReadFromFile("C:\\Users\\Jan\\source\\repos\\OK2.2\\OK2.2\\matrix.txt", 10);
        Console.WriteLine("N: " + n);
        var watchE = System.Diagnostics.Stopwatch.StartNew();
        var result2Estimated = VertexCover2Estimated(matrixE);
        watchE.Stop();
        var elapsedMsE = watchE.ElapsedMilliseconds;
        Console.WriteLine("2n-Przybliżony:");
        //PrintList(result2Estimated);
        Console.WriteLine("Czas: " + elapsedMsE);
        Console.WriteLine("Ilość wierzchołków pokrycia: " + result2Estimated.Count);


        //Greedy
        //matrix1 = ReadFromFile("C:\\Users\\Jan\\source\\repos\\OK2.2\\OK2.2\\matrix.txt", 10);

        var watchG = System.Diagnostics.Stopwatch.StartNew();
        var resultGreedy = VertexCoverGreedy(matrixG);
        watchG.Stop();
        var elapsedMsG = watchG.ElapsedMilliseconds;
        Console.WriteLine("Zachłanny:");
        //PrintList(resultGreedy);
        Console.WriteLine("Czas: " + elapsedMsG);
        Console.WriteLine("Ilość wierzchołków pokrycia: " + resultGreedy.Count);



        List<int> VertexCoverGreedy(int[,] matrix)
        {
            var n = matrix.GetLength(0);
            var Vertexes = new List<int>();
            int bestVertex = 0, maxCount = 0;
            List<int> VertexesToCover = new List<int>();
            List<int> CoveredVertexes = new List<int>();
            for (int i = 0; i < matrix.GetLength(0); i++) VertexesToCover.Add(i);
            while (VertexesToCover.Count > 0)
            {
                maxCount = 0;
                //finding Vertex with most edges
                for (int i = 0; i < VertexesToCover.Count; i++)
                {
                    int count = 0;
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        if (matrix[VertexesToCover.ElementAt(i), j] == 1 || matrix[j, VertexesToCover.ElementAt(i)] == 1) count++;
                    }
                    if (count == 0) VertexesToCover.Remove(VertexesToCover.ElementAt(i));
                    else if (count > maxCount)
                    {
                        maxCount = count;
                        bestVertex = VertexesToCover.ElementAt(i);
                    }
                }
                //Removing vertex edges
                for (int j = 0; j < n; j++)
                {
                    matrix[bestVertex, j] = 0;
                    matrix[j, bestVertex] = 0;
                }
                VertexesToCover.Remove(bestVertex);
                CoveredVertexes.Add(bestVertex);
                VertexesToCover = UpdateVertexesToCover(VertexesToCover, matrix);
            }
            return CoveredVertexes;
        }

        List<int> VertexCover2Estimated(int[,] matrix)
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

                List<int> Neighbours = new List<int>();
                VertexesToCover = UpdateVertexesToCover(VertexesToCover, matrix);
                int count = VertexesToCover.Count;
                if (count == 0) return CoveredVertexes;
                //Finding random vertex i
                int iRange = random.Next(0, count);
                var i = VertexesToCover.ElementAt(iRange);
                //Finding random neighbour - k
                for (int k = 0; k < n; k++)
                {
                    if (matrix[i, k] == 1 || matrix[k, i] == 1) Neighbours.Add(k);
                }
                count = Neighbours.Count;
                int j;
                do
                {
                    int jRange = random.Next(0, count);
                    j = Neighbours.ElementAt(jRange);
                } while (j == i);
                //Update
                CoveredVertexes.Add(i);
                CoveredVertexes.Add(j);
                for (int k = 0; k < n; k++)
                {
                    matrix[i, k] = 0;
                    matrix[k, i] = 0;
                    matrix[j, k] = 0;
                    matrix[k, j] = 0;
                }
                /*PrintMatrix(matrix);
                Console.WriteLine("i: " + i + "    j: " + j);*/
            }
            return CoveredVertexes;
        }

        List<int> UpdateVertexesToCover(List<int> VertexesToCover, int[,] matrix)
        {
            var VertexesToRemove = new List<int>();
            var n = matrix.GetLength(0);
            foreach (var item in VertexesToCover)
            {
                var count = 0;
                for (int j = 0; j < n; j++)
                {
                    if (matrix[item, j] == 1 || matrix[j, item] == 1) count++;
                }
                if (count == 0) VertexesToRemove.Add(item);
            }
            foreach (var item in VertexesToRemove)
            {
                VertexesToCover.Remove(item);
            }
            return VertexesToCover;
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
            String input = File.ReadAllText(txt);

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
            var random = new Random();
            int[,] matrix = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == j) matrix[i, j] = 0;
                    else
                    {
                        matrix[i, j] = random.Next(0, 2);
                    }
                }
            }
            //PrintMatrix(matrix);
            return matrix;
        }
    }
}