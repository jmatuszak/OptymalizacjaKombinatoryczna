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
        var matrix1 = ReadFromFile("C:\\Users\\Jan\\source\\repos\\OK2.2\\OK2.2\\matrix.txt", 10);
        PrintMatrix(matrix1);
        Console.WriteLine();

        var result = VertexCover2Closed(matrix1);
        PrintList(result);


        List<int> VertexCover2Closed(int[,] matrix)
        {

            var random = new Random();
            for (int x = 0; x < 10; x++)
            {
                int r = random.Next(0, 2);
                Console.WriteLine(r);
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
                PrintMatrix(matrix);
                Console.WriteLine("i: " + i + "    j: " + j);
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
            string projectFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\");
            string inputFilePath = Path.Combine(projectFolderPath, "matrix.txt");

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