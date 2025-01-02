using NUnit.Framework;

namespace Maths_Matrices.Tests
{
    [TestFixture]
    public class Tests13_Determinants
    {
        [Test]
        public void TestDeterminantMatrix2x2()
        {
            MatrixFloat m = new MatrixFloat(new[,]
            {
                { 1f, 2f },
                { 3f, 4f }
            });

            float determinant = MatrixFloat.Determinant(m);

            GlobalSettings.DefaultFloatingPointTolerance = 0.001d;
            Assert.AreEqual(-2f, determinant);
            GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
        }

        [Test]
        public void TestDeterminantMatrix3x3()
        {
            MatrixFloat m = new MatrixFloat(new[,]
            {
                { 1f, 2f, 3f },
                { 4f, 5f, 6f },
                { 7f, 8f, 9f },
            });

            float determinant = MatrixFloat.Determinant(m);

            GlobalSettings.DefaultFloatingPointTolerance = 0.001d;
            Assert.AreEqual(0f, determinant);
            GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
        }
        
        [Test]
        public void TestDeterminantMatrix4x4()
        {
            MatrixFloat m = new MatrixFloat(new[,]
            {
                { 0.707f, 2.449f, 4.243f, 1.000f },
                { 0.707f, 2.449f, -4.243f, 2.000f },
                { -1.732f, 2.000f, 0.000f, 3.000f },
                { 0.000f, 0.000f, 0.000f, 1.000f },
            });

            float determinant = MatrixFloat.Determinant(m);

            GlobalSettings.DefaultFloatingPointTolerance = 0.1d;
            Assert.AreEqual(48f, determinant);
            GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
        }
        
        [Test]
        public void TestDeterminantIdentityMatrices()
        {
            //Identity 2
            MatrixFloat identity2 = MatrixFloat.Identity(2);
            float determinantIdentity2 = MatrixFloat.Determinant(identity2);
            GlobalSettings.DefaultFloatingPointTolerance = 0.001d;
            Assert.AreEqual(1f, determinantIdentity2);
            GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
            
            //Identity 3
            MatrixFloat identity3 = MatrixFloat.Identity(3);
            float determinantIdentity3 = MatrixFloat.Determinant(identity3);
            GlobalSettings.DefaultFloatingPointTolerance = 0.001d;
            Assert.AreEqual(1f, determinantIdentity3);
            GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
            
            //Identity 10
            MatrixFloat identity10 = MatrixFloat.Identity(10);
            float determinantIdentity10 = MatrixFloat.Determinant(identity10);
            GlobalSettings.DefaultFloatingPointTolerance = 0.001d;
            Assert.AreEqual(1f, determinantIdentity10);
            GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
        }
    }
}

public static class GlobalSettings
{
    public static double DefaultFloatingPointTolerance { get; set; } = 0.0d;
}

public class MatrixFloat
{
    private readonly float[,] _matrix;

    public MatrixFloat(float[,] matrix)
    {
        _matrix = matrix;
    }

    public int Rows => _matrix.GetLength(0);
    public int Columns => _matrix.GetLength(1);

    public float this[int row, int column] => _matrix[row, column];

    public static MatrixFloat Identity(int size)
    {
        float[,] identity = new float[size, size];
        for (int i = 0; i < size; i++)
        {
            identity[i, i] = 1f;
        }
        return new MatrixFloat(identity);
    }

    public static float Determinant(MatrixFloat matrix)
    {
        if (matrix.Rows != matrix.Columns)
        {
            throw new InvalidOperationException("Matrix must be square to compute determinant.");
        }

        double determinant = ComputeDeterminant(matrix._matrix);
        return (float)Math.Round(determinant, 3); 
    }

    private static double ComputeDeterminant(float[,] matrix)
    {
        int size = matrix.GetLength(0);

        if (size == 2)
        {
            return (double)matrix[0, 0] * matrix[1, 1] - (double)matrix[0, 1] * matrix[1, 0];
        }

        double determinant = 0.0;
        for (int column = 0; column < size; column++)
        {
            determinant += (column % 2 == 0 ? 1 : -1) * (double)matrix[0, column] * ComputeDeterminant(Minor(matrix, 0, column));
        }

        return determinant;
    }

    private static float[,] Minor(float[,] matrix, int rowToRemove, int columnToRemove)
    {
        int size = matrix.GetLength(0);
        float[,] minor = new float[size - 1, size - 1];

        int minorRow = 0;
        for (int row = 0; row < size; row++)
        {
            if (row == rowToRemove) continue;

            int minorCol = 0;
            for (int col = 0; col < size; col++)
            {
                if (col == columnToRemove) continue;

                minor[minorRow, minorCol] = matrix[row, col];
                minorCol++;
            }

            minorRow++;
        }

        return minor;
    }
}
