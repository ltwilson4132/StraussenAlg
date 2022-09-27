namespace StraussenAlg
{
    public class Program
    {
        public static void Main()
        {
            int[,] a = new int[,] { { 1, 2 }, { 3, 4 } };
            int[,] b = new int[,] { { 5, 6}, { 7, 8} };
            int n = 2;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.WriteLine($"A{i + 1}{j + 1}: {a[i, j]}");
                }
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.WriteLine($"B{i + 1}{j + 1}: {b[i, j]}");
                }
            }

            int[,] c = StrassenMultiplication(a, b, n);

            for (int i = 0; i < n; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    Console.WriteLine($"C{i + 1}{j + 1}: {c[i, j]}");
                }
            }
        }

        public static int[,] StrassenMultiplication(int[,] A, int[,] B, int n)
        {
            int[,] C = new int[n, n];

            if (n == 1)
            {
                C[0, 0] += A[0, 0] * B[0, 0];
                return C;
            }
            int k = n / 2;

            int[,] A11 = new int[k, k];
            int[,] A12 = new int[k, k];
            int[,] A21 = new int[k, k];
            int[,] A22 = new int[k, k];
            int[,] B11 = new int[k, k];
            int[,] B12 = new int[k, k];
            int[,] B21 = new int[k, k];
            int[,] B22 = new int[k, k];
            for (int i = 0; i < k; i++)
            {
                for (int j = 0; j < k; j++)
                {
                    A11[i, j] = A[i, j];
                    A12[i, j] = A[i, k + j];
                    A21[i, j] = A[k + i, j];
                    A22[i, j] = A[k + i, k + j];
                    B11[i, j] = B[i, j];
                    B12[i, j] = B[i, k + j];
                    B21[i, j] = B[k + i, j];
                    B22[i, j] = B[k + i, k + j];
                }
            }

            int[,] P1 = StrassenMultiplication(A11, Subtract(B12, B22, k), k);
            int[,] P2 = StrassenMultiplication(Add(A11, A12, k), B22, k);
            int[,] P3 = StrassenMultiplication(Add(A21, A22, k), B11, k);
            int[,] P4 = StrassenMultiplication(A22, Subtract(B21, B11, k), k);
            int[,] P5 = StrassenMultiplication(Add(A11, A22, k), Add(B11, B22, k), k);
            int[,] P6 = StrassenMultiplication(Subtract(A12, A22, k), Add(B21, B22, k), k);
            int[,] P7 = StrassenMultiplication(Subtract(A11, A21, k), Add(B11, B12, k), k);

            int[,] C11 = Subtract(Add(Add(P5, P4, k), P6, k), P2, k);
            int[,] C12 = Add(P1, P2, k);
            int[,] C21 = Add(P3, P4, k);
            int[,] C22 = Subtract(Subtract(Add(P5, P1, k), P3, k), P7, k);

            for(int i = 0; i < k; i++)
            {
                for(int j = 0; j < k; j++)
                {
                    C[i, j] = C11[i, j];
                    C[i, j + k] = C12[i, j];
                    C[k + i, j] = C21[i, j];
                    C[k + i, k + j] = C22[i, j];
                }
            }

            return C;
        }

        private static int[,] Add(int[,] a, int[,] b, int n)
        {
            int[,] temp = new int[n, n];
            for(int i = 0;i < n; i++)
            {
                for(int j =0; j < n; j++)
                {
                    temp[i, j] = a[i, j] + b[i, j];
                }
            }
            return temp;
        }

        private static int[,] Subtract(int[,] a, int[,] b, int n)
        {
            int[,] temp = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    temp[i, j] = a[i, j] - b[i, j];
                }
            }
            return temp;
        }
    }
}