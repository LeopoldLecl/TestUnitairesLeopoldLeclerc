using System;

namespace Maths_Matrices.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class Tests14_AdjugateMatrices
    {
        [Test]
        public void TestCalculateAdjugateMatrixInstance()
        {
            MatrixFloat m = new MatrixFloat(new[,]
            {
                { 1f, 2f },
                { 3f, 4f }
            });

            MatrixFloat adjM = m.Adjugate();
            GlobalSettings.DefaultFloatingPointTolerance = 0.001d;
            Assert.AreEqual(new[,]
            {
                { 4f, -2f },
                { -3f, 1f },
            }, adjM.ToArray2D());
            GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
        }

        [Test]
        public void TestCalculateAdjugateMatrixStatic()
        {
            MatrixFloat m = new MatrixFloat(new[,]
            {
                { 1f, 0f, 5f },
                { 2f, 1f, 6f },
                { 3f, 4f, 0f },
            });

            MatrixFloat adjM = MatrixFloat.Adjugate(m);

            GlobalSettings.DefaultFloatingPointTolerance = 0.001d;
            Assert.AreEqual(new[,]
            {
                { -24f, 20f, -5f },
                { 18f, -15f, 4f },
                { 5f, -4f, 1f },
            }, adjM.ToArray2D());
            GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
        }

        [Test]
        public void TestCalculateAdjugateMatrixIdentity4x4()
        {
            MatrixFloat m = new MatrixFloat(new[,]
            {
                { 1f, 0f, 0f, 0f },
                { 0f, 1f, 0f, 0f },
                { 0f, 0f, 1f, 0f },
                { 0f, 0f, 0f, 1f },
            });

            MatrixFloat adjM = m.Adjugate();
            GlobalSettings.DefaultFloatingPointTolerance = 0.001d;
            Assert.AreEqual(new[,]
            {
                { 1f, 0f, 0f, 0f },
                { 0f, 1f, 0f, 0f },
                { 0f, 0f, 1f, 0f },
                { 0f, 0f, 0f, 1f },
            }, adjM.ToArray2D());
            GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
        }
    }
}

namespace Maths_Matrices
{
    public class MatrixFloat
    {
        private readonly float[,] _matrix;

        public MatrixFloat(float[,] matrix)
        {
            _matrix = matrix;
        }

        public float[,] ToArray2D()
        {
            return _matrix;
        }

        public MatrixFloat Adjugate()
        {
            return Adjugate(this);
        }

        public static MatrixFloat Adjugate(MatrixFloat matrix)
        {
            int rows = matrix._matrix.GetLength(0);
            int cols = matrix._matrix.GetLength(1);

            if (rows != cols)
                throw new InvalidOperationException("Adjugate can only be calculated for square matrices.");

            float[,] result = new float[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[j, i] = Cofactor(matrix._matrix, i, j);
                }
            }

            return new MatrixFloat(result);
        }

        private static float Cofactor(float[,] matrix, int row, int col)
        {
            int size = matrix.GetLength(0);
            float[,] minor = new float[size - 1, size - 1];
            int minorRow = 0, minorCol;

            for (int i = 0; i < size; i++)
            {
                if (i == row) continue;

                minorCol = 0;
                for (int j = 0; j < size; j++)
                {
                    if (j == col) continue;

                    minor[minorRow, minorCol] = matrix[i, j];
                    minorCol++;
                }

                minorRow++;
            }

            return (float)Math.Pow(-1, row + col) * Determinant(minor);
        }

        private static float Determinant(float[,] matrix)
        {
            int size = matrix.GetLength(0);

            if (size == 1)
                return matrix[0, 0];

            if (size == 2)
                return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];

            float det = 0;

            for (int col = 0; col < size; col++)
            {
                det += matrix[0, col] * Cofactor(matrix, 0, col);
            }

            return det;
        }
    }

    public static class GlobalSettings
    {
        public static double DefaultFloatingPointTolerance { get; set; } = 0.0d;
    }
}
