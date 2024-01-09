var matrix1 = ReadMatrix1();
var n = matrix1.GetLength(1);
var matrix2 = ReadMatrix2(n);
var result = MultiplyMatrix(matrix1, matrix2);

PrintMatrix(result);


int[,] MultiplyMatrix(int[,] matrix1, int[,] matrix2)
{
    var m = matrix1.GetLength(0);
    var n = matrix1.GetLength(1);
    var p = matrix2.GetLength(1);

    var result = new int[m, p];

    for (int h = 0; h < m; h++)
    {
        for (int i = 0; i < p; i++)
        {
            var sum = 0;
            for (int j = 0; j < n; j++)
            {
                sum += matrix1[h, j] * matrix2[j, i];
                ;
            }
            result[h, i] = sum;
        }
    }
    return result;
}
int[,] ReadMatrix1()
{
    var list = new List<string[]>();

    string line = Console.ReadLine();

    while (!string.IsNullOrEmpty(line))
    {

        var trimmed = line.Split(' ');
        if (trimmed.Length > 0)
            list.Add(trimmed);
        line = Console.ReadLine();
    }
    int m = list.Count();
    int n = list[0].Length;
    var matrix = new int[m, n];
    for (int i = 0; i < m; i++)
    {
        for (int j = 0; j < n; j++)
        {
            var text = list[i];
            matrix[i, j] = int.Parse(text[j]);
        }
    }

    return matrix;
}
int[,] ReadMatrix2(int n)
{
    var list = new List<string[]>();

    for (int i = 0; i < n; i++)
    {
        var line = Console.ReadLine();
        var trimmed = line.Split(' ');
        list.Add(trimmed);
    }

    int p = list[0].Length;
    var matrix = new int[n, p];
    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < p; j++)
        {
            matrix[i, j] = int.Parse(list[i][j]);
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