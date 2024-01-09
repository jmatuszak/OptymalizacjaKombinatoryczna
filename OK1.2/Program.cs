using System;
using System.Collections.Generic;
using System.Linq;
public class MainClass
{
    public static void Main()
    {
        /*        try
                {*/
        //var matrix = ReadMatrix();
        var matrix = ReadFromFile();
        int startVertex = int.Parse(Console.ReadLine()) - 1;

        var DfsOrder = Dfs(matrix, startVertex);
        if (DfsOrder.Count == matrix.GetLength(0))
        {
            Console.Write("Porządek DFS:");
            foreach (var item in DfsOrder)
            {
                Console.Write(" " + (item + 1));
            }
            Console.WriteLine("\nGraf jest spójny");
        }
        else
        {
            Console.WriteLine("\nGraf jest niespójny");
            foreach (var item in DfsOrder)
            {
                Console.Write(" " + (item + 1));
            }
        }
        /*        }
                catch (Exception)
                {
                    Console.WriteLine("BŁĄD");
                }*/




        //var matrix = ReadFromFile();




        List<int> Dfs(int[,] matrix, int start)
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
                        }
                    }
                    DfsOrder.Add(v);
                }
            }
            return DfsOrder;
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
                if (int.Parse(trimmed[j]) < 0) Console.WriteLine("BŁ!!!!!!!!!!!!!");
            }
            for (int i = 1; i < n; i++)
            {
                line = Console.ReadLine();
                trimmed = line.Split(' ');
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = int.Parse(trimmed[j]);
                    if (int.Parse(trimmed[j]) < 0) Console.WriteLine("BŁ!!!!!!!!!!!!!");
                }
            }
            return matrix;
        }
        int[,] ReadFromFile()
        {
            String input = File.ReadAllText("matrix.txt");

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