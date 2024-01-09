using System;
using System.Collections.Generic;
using System.Linq;

public class MainClass
{
    public static void Main()
    {
        var matrix = ReadFromFile();
        //int[,] matrix1 = new int[,] { { 0, 1, 1 }, { 1, 0, 1 }, { 1, 1, 0 } };
        //int[,] matrix2 = new int[,] { { 0, 1, 0 }, { 1, 0, 1 }, { 0, 1, 0 } };
        //var matrix = ReadMatrix();

        var multipliedMatrix = MultipleMatrix(matrix);
        FindCycle(multipliedMatrix, matrix);
        void FindCycle(int[,] multipliedMatrix, int[,] matrix)
        {
            List<List<int>> ListOfCycles = new List<List<int>>();
            List<int> C3 = new List<int>();
            int count = 0;
            var n = matrix.GetLength(0);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    for (int k = 0; k < n; k++)
                    {
                        if (multipliedMatrix[i, j] >= 1 && multipliedMatrix[j, k] >= 1 && multipliedMatrix[k, i] >= 1 &&
                            i != k && k != j && i != j &&
                            matrix[i, j] == 1 && matrix[j, k] == 1 && matrix[k, i] == 1)
                        {
                            count++;
                            C3 = new List<int>() { i, j, k };
                            if (!CheckIfContainsCycle(ListOfCycles, C3))
                            {
                                ListOfCycles.Add(C3);
                            }
                        }
                    }
                }
            }
            Console.WriteLine(ListOfCycles.Count);

            for (int i = 0; i < ListOfCycles.Count; i++)
            {
                Console.WriteLine(ListOfCycles.ElementAt(i).ElementAt(0) + " " + ListOfCycles.ElementAt(i).ElementAt(1) + " " + ListOfCycles.ElementAt(i).ElementAt(2) + " ");
            }
        }

        bool CheckIfContainsCycle(List<List<int>> ListOfCycles, List<int> C3)
        {
            for (int i = 0; i < ListOfCycles.Count; i++)
            {
                if (ListOfCycles.ElementAt(i).Contains(C3.ElementAt(0)) &&
                    ListOfCycles.ElementAt(i).Contains(C3.ElementAt(1)) &&
                    ListOfCycles.ElementAt(i).Contains(C3.ElementAt(2)))
                    return true;
            }
            return false;
        }

        int[,] MultipleMatrix(int[,] matrix)
        {
            var n = matrix.GetLength(0);
            var result = new int[n, n];

            for (int h = 0; h < n; h++)
            {
                for (int i = 0; i < n; i++)
                {
                    var sum = 0;
                    for (int j = 0; j < n; j++)
                    {
                        sum += matrix[h, j] * matrix[j, i];
                    }
                    result[h, i] = sum;
                }
            }
            return result;
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
