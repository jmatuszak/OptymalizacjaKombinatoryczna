
var matrix = ReadMatrix();
CountStuff(matrix);



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

static void CountStuff(int[,] matrix)
{
    var n = matrix.GetLength(0);
    double count = 0;
    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < n; j++)
        {
            if (matrix[i, j] >= 1 && matrix[j, i] >= 1) count += 0.5;
            if (matrix[i, j] >= 1 && matrix[j, i] < 1) count += 1;

            //Console.WriteLine(matrix[i, j]);
        }
    }
    Console.WriteLine("Ilość wierzchołków: " + n);
    Console.WriteLine("Ilość krawędzi: " + count);
}