using System;
using System.Collections.Generic;
using System.Linq;

public class MainClass
{
    public static void Main()
    {
        var ListOfCycles = new List<List<int>>();
        var matrix = ReadMatrix();
        bool ifContains = false;
        for (int w = 0; w < matrix.GetLength(0); w++)
        {
            var Visited = new List<int>();
            var Stack = new List<int>();
            int v = w;
            ifContains = DfsFindCycle(matrix, v, ListOfCycles);
            if (ifContains)
                w = matrix.GetLength(0);
        }
        if (ifContains) Console.WriteLine("TAK");
        else Console.WriteLine("NIE");

        bool DfsFindCycle(int[,] matrix, int start, List<List<int>> ListOfCycles)
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
                                    return true;
                            }
                        }
                    }
                    DfsOrder.Add(v);
                }
            }
            return false;
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
    }
}