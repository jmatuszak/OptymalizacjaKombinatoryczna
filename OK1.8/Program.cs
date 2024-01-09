
//int[,] matrix = new int[,] { { 0, 1, 1 }, { 1, 0, 1 }, { 1, 1, 0 } };
//int[,] matrix2 = new int[,] { { 0, 1, 0 }, { 1, 0, 1 }, { 0, 1, 0 } };
//int[,] matrix = new int[,] { { 0, 1, 1, 0 }, { 1, 0, 1, 0 }, { 1, 1, 0, 1 }, { 0, 0, 1, 0 } };
//int[,] matrix = new int[,] { { 0, 0, 1, 1, 1 }, { 0, 0, 1, 1, 1 }, { 1, 1, 0, 1, 1 }, { 1, 1, 1, 0, 1 }, { 1, 1, 1, 1, 0 } };


//var matrix = ReadMatrix();
int[,] matrix = ReadFromFile();
var multipliedMatrix = MultipleMatrix(matrix);
FindCycle(multipliedMatrix, matrix);
void FindCycle(int[,] multipliedMatrix, int[,] matrix)
{
    var ListOfCycles = new List<List<int>>();
    int count = 0;
    var n = matrix.GetLength(0);
    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < n; j++)
        {
            if (multipliedMatrix[i, j] >= 1 && multipliedMatrix[j, i] >= 1)
            {
                if (matrix[i, j] == 1 || matrix[j, i] == 1)
                {
                    for (int k = 0; k < n; k++)
                    {
                        /*                        if (!(multipliedMatrix[i, k] != 0 || multipliedMatrix[j, k] != 0|| 
                                                    multipliedMatrix[k, i] != 0 || multipliedMatrix[k, j] != 0)) break;*/
                        if (i == k || i == j || k == j) break;
                        if (multipliedMatrix[i, k] >= 1 && multipliedMatrix[k, i] >= 1 &&
                            multipliedMatrix[j, k] >= 1 && multipliedMatrix[k, j] >= 1)
                        {
                            if ((matrix[i, k] == 1 || matrix[k, i] == 1) &&
                                (matrix[j, k] == 1 || matrix[k, j] == 1))
                            {
                                if (CheckIfContainsCycle(ListOfCycles, i, j, k) == false)
                                    ListOfCycles.Add(new List<int>() { i, j, k });
                                //Console.WriteLine(ListOfCycles.Count);
                            }

                        }
                    }
                }
            }
        }
    }
    Console.WriteLine(ListOfCycles.Count);
}
bool CheckIfContainsCycle(List<List<int>> ListOfCycles, int i, int j, int k)
{
    if (ListOfCycles.Count == 0) return false;
    else
    {
        for (int c = 0; c < ListOfCycles.Count; c++)
        {
            if (ListOfCycles[c].Contains(i) && ListOfCycles[c].Contains(j) && ListOfCycles[c].Contains(k))
            {
                return true;
            }
        }
        Console.WriteLine(i + " " + j + " " + k);
        return false;
    }
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

int[,] ReadFromFile()
{
    string projectFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\");
    string inputFilePath = Path.Combine(projectFolderPath, "matrix.txt");

    String input = File.ReadAllText(inputFilePath);

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