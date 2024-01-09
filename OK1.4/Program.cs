var matrix1 = ReadMatrix1();
var n = matrix1.GetLength(1);
var matrix2 = ReadMatrix2(n);

var m = matrix1.GetLength(0);
var p = matrix2.GetLength(1);
var Dimensions = new List<int>() { m, n, p };
var maxDimension = Dimensions.Max();
var rightDimension = GetRightDimension(maxDimension);
var squareMatrix1 = MakeMatrixSquare(matrix1, rightDimension);
var squareMatrix2 = MakeMatrixSquare(matrix2, rightDimension);
var C = StrassenMultiply(squareMatrix1, squareMatrix2);
C = CutMatrix(C, m, p);
PrintMatrix(C);



int[,] CutMatrix(int[,] C, int m, int p)
{
    int[,] result = new int[m, p];
    for (int i = 0; i < m; i++)
    {
        for (int j = 0; j < p; j++)
        {
            result[i, j] = C[i, j];
        }
    }
    return result;
}
int GetRightDimension(int maxDimension)
{
    int n = 0;
    int value = 1;

    while (value < maxDimension)
    {
        value *= 2;
        n++;
    }

    int result = 1;
    for (int i = 0; i < n; i++)
    {
        result *= 2;
    }
    return result;
}


int[,] StrassenMultiply(int[,] A, int[,] B)
{
    int n = A.GetLength(0);
    if (n > 2)
    {
        int[,] A11 = new int[n / 2, n / 2];
        int[,] A12 = new int[n / 2, n / 2];
        int[,] A21 = new int[n / 2, n / 2];
        int[,] A22 = new int[n / 2, n / 2];
        int[,] B11 = new int[n / 2, n / 2];
        int[,] B12 = new int[n / 2, n / 2];
        int[,] B21 = new int[n / 2, n / 2];
        int[,] B22 = new int[n / 2, n / 2];

        for (int i = 0; i < n / 2; i++)
        {
            for (int j = 0; j < n / 2; j++)
            {
                A11[i, j] = A[i, j];
                A12[i, j] = A[i, j + n / 2];
                A21[i, j] = A[i + n / 2, j];
                A22[i, j] = A[i + n / 2, j + n / 2];

                B11[i, j] = B[i, j];
                B12[i, j] = B[i, j + n / 2];
                B21[i, j] = B[i + n / 2, j];
                B22[i, j] = B[i + n / 2, j + n / 2];
            }
        }
        var S1 = SubstractMatrix(B12, B22);
        var S2 = AddMatrix(A11, A12);
        var S3 = AddMatrix(A21, A22);
        var S4 = SubstractMatrix(B21, B11);
        var S5 = AddMatrix(A11, A22);
        var S6 = AddMatrix(B11, B22);
        var S7 = SubstractMatrix(A12, A22);
        var S8 = AddMatrix(B21, B22);
        var S9 = SubstractMatrix(A21, A11);
        var S10 = AddMatrix(B11, B12);

        var P1 = StrassenMultiply(A11, S1);
        var P2 = StrassenMultiply(S2, B22);
        var P3 = StrassenMultiply(S3, B11);
        var P4 = StrassenMultiply(A22, S4);
        var P5 = StrassenMultiply(S5, S6);
        var P6 = StrassenMultiply(S7, S8);
        var P7 = StrassenMultiply(S9, S10);


        var C11 = AddMatrix(SubstractMatrix(AddMatrix(P5, P4), P2), P6);
        var C12 = AddMatrix(P1, P2);
        var C21 = AddMatrix(P3, P4);
        var C22 = AddMatrix(SubstractMatrix(AddMatrix(P5, P1), P3), P7);





        int[,] C = new int[n, n];
        for (int i = 0; i < n / 2; i++)
        {
            for (int j = 0; j < n / 2; j++)
            {
                C[i, j] = C11[i, j];
                C[i, j + n / 2] = C12[i, j];
                C[i + n / 2, j] = C21[i, j];
                C[i + n / 2, j + n / 2] = C22[i, j];
            }
        }
        return C;
    }
    else
    {
        /*        var S1 = B[0, 1] - B[1, 1];
                var S2 = A[0, 0] + A[0, 1];
                var S3 = A[1, 0] + A[1, 1];
                var S4 = B[1, 0] - B[0, 0];
                var S5 = A[0, 0] + A[1, 1];
                var S6 = B[0, 0] + B[1, 1];
                var S7 = A[0, 1] - A[1, 1];
                var S8 = B[1, 0] + B[1, 1];
                var S9 = A[1, 0] - A[0, 0];
                var S10 = B[0, 0] + B[0, 1];

                var P1 = A[0, 0] * S1;
                var P2 = S2 * B[1, 1];
                var P3 = S3 * B[0, 0];
                var P4 = A[1, 1] * S4;
                var P5 = S5 * S6;
                var P6 = S7 * S8;
                var P7 = S9 * S10;

                var C11 = P5 + P4 - P2 + P6;
                var C12 = P1 + P2;
                var C21 = P3 + P4;
                var C22 = P5 + P1 - P3 + P7;
        */
        int[,] C = new int[2, 2];
        C[0, 0] = A[0, 0] * B[0, 0] + A[0, 1] * B[1, 0];
        C[0, 1] = A[0, 0] * B[0, 1] + A[0, 1] * B[1, 1];
        C[1, 0] = A[1, 0] * B[0, 0] + A[1, 1] * B[1, 0];
        C[1, 1] = A[1, 0] * B[0, 1] + A[1, 1] * B[1, 1];
        return C;
    }
}

int[,] AddMatrix(int[,] A, int[,] B)
{
    var n = A.GetLength(0);
    int[,] C = new int[n, n];
    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < n; j++)
        {
            C[i, j] = A[i, j] + B[i, j];
        }
    }
    return C;
}
int[,] SubstractMatrix(int[,] A, int[,] B)
{
    var n = A.GetLength(0);
    int[,] C = new int[n, n];
    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < n; j++)
        {
            C[i, j] = A[i, j] - B[i, j];
        }
    }
    return C;
}




int[,] MakeMatrixSquare(int[,] matrix, int rightDimension)
{
    if (matrix.GetLength(0) == rightDimension && matrix.GetLength(1) == rightDimension)
    {
        return matrix;
    }

    int[,] sMatrix = new int[rightDimension, rightDimension];
    for (int i = 0; i < rightDimension; i++)
    {
        for (int j = 0; j < rightDimension; j++)
        {
            if (i < matrix.GetLength(0) && j < matrix.GetLength(1))
            {
                sMatrix[i, j] = matrix[i, j];
            }
            else
            {
                sMatrix[i, j] = 0;
            }
        }
    }
    return sMatrix;
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