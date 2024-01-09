using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

public class MainClass
{
    public static void Main()
    {
        var ListOfCycles = new List<List<int>>();
        //int[,] matrix1 = new int[,] { { 0, 1, 1 }, { 1, 0, 1 }, { 1, 1, 0 } };
        //int[,] matrix2 = new int[,] { { 0, 1, 0 }, { 1, 0, 1 }, { 0, 1, 0 } };
        //var matrix = ReadMatrix();
        var matrix1 = ReadFromFile();
        //int v = 0;
        //int w = v;

        for (int w = 0; w < matrix1.GetLength(0); w++)
        {
            var Visited = new List<int>();
            var Stack = new List<int>();
            int v = w;
            ListOfCycles = DfsFindCycle(matrix1, v, ListOfCycles);
        }

        Console.WriteLine(ListOfCycles.Count);
        for (int i = 0; i < ListOfCycles.Count; i++)
        {
            Console.WriteLine(ListOfCycles.ElementAt(i).ElementAt(0) + " " + ListOfCycles.ElementAt(i).ElementAt(1) + " " + ListOfCycles.ElementAt(i).ElementAt(2) + " ");
        }
        Console.Write(ListOfCycles.Count);




        List<List<int>> DfsFindCycle(int[,] matrix, int start, List<List<int>> ListOfCycles)
        {
            List<int> DfsOrder = new List<int>();
            Stack<int> DfsStack = new Stack<int>();

            DfsStack.Push(start);
            //DfsOrder.Add(start);
            var n = matrix.GetLength(0);
            while (DfsStack.Count > 0)
            {
                int v = DfsStack.Pop();
                if (!DfsOrder.Contains(v))
                {
                    for (int i = n - 1; i >= 0; i--)
                    {
                        if (matrix[v, i] >= 1)
                        {
                            DfsStack.Push(i);
                            for (int j = 0; j < n; j++)
                            {
                                if (matrix[i, j] >= 1 && matrix[j, v] >= 1)
                                    if (!CheckIfContainsCycle(ListOfCycles, i, j, v))
                                        ListOfCycles.Add(new List<int> { i, j, v });
                            }
                        }
                    }
                    DfsOrder.Add(v);
                }
            }
            return ListOfCycles;
        }
        bool CheckIfContainsCycle(List<List<int>> ListOfCycles, int i, int j, int k)
        {
            if (ListOfCycles.Count == 0) return false;
            else
            {
                for (int c = 0; c < ListOfCycles.Count; c++)
                {
                    if (ListOfCycles[c].Contains(i) &&
                        ListOfCycles[c].Contains(j) &&
                        ListOfCycles[c].Contains(k))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        int[,] ReadMatrix()
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
        int[,] ReadFromFile()
        {
            string projectFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\");
            string inputFilePath = Path.Combine(projectFolderPath, "matrix.txt");

            string input = File.ReadAllText(inputFilePath);

            int i = 0, j = 0;
            int[,] result = new int[30, 30];
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
    }
}