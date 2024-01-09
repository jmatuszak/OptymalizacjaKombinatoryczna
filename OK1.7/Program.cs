
//int[,] matrix1 = new int[,] { { 0, 1, 1 }, { 1, 0, 1 }, { 1, 1, 0 } };
//int[,] matrix2 = new int[,] { { 0, 1, 0 }, { 1, 0, 1 }, { 0, 1, 0 } };
var matrix = ReadMatrix();

var multipliedMatrix = MultipleMatrix(matrix);
FindCycle(multipliedMatrix, matrix);
void FindCycle(int[,] multipliedMatrix, int[,] matrix)
{
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
                    matrix[i, j] == 1 && matrix[j, k] == 1 && matrix[k, i] == 1) count++;
            }
        }
    }
    if (count > 0) Console.WriteLine("TAK");
    else Console.WriteLine("NIE");
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
                ;
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