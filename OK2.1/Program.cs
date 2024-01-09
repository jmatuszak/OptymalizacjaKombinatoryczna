using System;
using System.Collections.Generic;
using System.Linq;

public class MainClass
{
    public static void Main()
    {
        //var matrix = ReadFromFile();
        //int[,] matrix1 = new int[,] { { 0, 1, 1 }, { 1, 0, 1 }, { 1, 1, 0 } };
        //int[,] matrix2 = new int[,] { { 0, 1, 0 }, { 1, 0, 1 }, { 0, 1, 0 } };
        //var matrix = ReadMatrix();
        var matrix1 = ReadFromFile("C:\\Users\\Jan\\Source\\Repos\\OK2.1\\OK2.1\\matrix2.txt", 6);
        PrintMatrix(matrix1);
        var result = VertexCoverGreedy(matrix1);
        PrintList(result);


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
        List<int> UpdateVertexesToCover(List<int> VertexesToCover, int[,] matrix)
        {
            var VertexesToRemove = new List<int>();
            var n = matrix.GetLength(0);
            foreach (var item in VertexesToCover)
            {
                var count = 0;
                for (int j = 0; j < n; j++)
                {
                    if (matrix[item, j] == 1) count++;
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
            foreach (var item in result)
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
    }
}