using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

public class MainClass
{
    struct Dpv
    {
        public int distance;
        public int previous;
        public bool visited;
    };
    public static void Main()
    {

        int[,] matrix = new int[6, 6]
            { {0,3,0,0,3,0 },
              {0,0,1,0,0,0 },
              {0,0,0,3,0,1 },
              {0,3,0,0,0,0 },
              {0,0,0,0,0,2 },
              {5,0,0,1,0,0 } };

        /*        var n = 10;
                var random = new Random();
                var matrix = GenerateMatrix(n);
                var v = random.Next(0, n);*/


        var v = 0;
        var d = Dijkstra(matrix, v);
        var X = new List<int>() { 0, 1, 4, 5 };
        var sum = SteinerGreedy(matrix, X);
        Console.WriteLine(sum);

        int SteinerGreedy(int[,] matrix, List<int> X)
        {
            var sum = 0;
            var t = 0;

            while (X.Count > 0)
            {
                int xToRemove = t;
                var dpvArray = Dijkstra(matrix, t);
                X.Remove(t);
                var minDistance = int.MaxValue;
                foreach (var x in X)
                {
                    if (dpvArray[x].distance < minDistance)
                    {
                        minDistance = dpvArray[x].distance;
                        xToRemove = x;
                    }
                }
                sum += minDistance;
                X.Remove(xToRemove);
                t = xToRemove;
            }
            return sum;
        }

        Dpv[] Dijkstra(int[,] matrix, int v)
        {
            var n = matrix.GetLength(0);
            Dpv[] dpvArray = new Dpv[n];
            for (int i = 0; i < n; i++)
            {
                dpvArray[i].distance = (i == v) ? 0 : int.MaxValue;
                dpvArray[i].previous = -1;
                dpvArray[i].visited = false;
            }
            int u = v;
            while (u != -1)
            {
                dpvArray[u].visited = true;
                for (int i = 0; i < n; i++)
                {
                    if (matrix[u, i] > 0 && dpvArray[u].distance + matrix[u, i] < dpvArray[i].distance)
                    {
                        dpvArray[i].distance = dpvArray[u].distance + matrix[u, i];
                        dpvArray[i].previous = u;
                    }
                }
                u = FindMin(ref dpvArray);
            }
            return dpvArray;
        }


        int FindMin(ref Dpv[] dpvArray)
        {
            int min = -1;
            int minDistance = int.MaxValue;
            var n = matrix.GetLength(0);
            for (int i = 0; i < n; i++)
            {
                if (!dpvArray[i].visited && dpvArray[i].distance < minDistance)
                {
                    min = i;
                    minDistance = dpvArray[i].distance;
                }
            }
            return min;
        }




        /*
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
                }*/
    }
}