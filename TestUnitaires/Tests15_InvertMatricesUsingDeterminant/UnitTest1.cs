using System;
using NUnit.Framework;

namespace Maths_Matrices.Tests
{
    [TestFixture]
    public class Tests15_InvertMatricesUsingDeterminant
    {
        [Test]
        public void TestInvertMatrixInstance()
        {
            //If you need help, See => https://www.sangakoo.com/en/unit/inverse-matrix-using-determinants
            //Or you can reuse the same principle from course => https://www.wikihow.com/Find-the-Inverse-of-a-3x3-Matrix
            MatrixFloat m = new MatrixFloat(new[,]
            {
                { 2f, 3f, 8f },
                { 6f, 0f, -3f },
                { -1f, 3f, 2f },
            });

            MatrixFloat mInverted = m.InvertByDeterminant();

            GlobalSettings.DefaultFloatingPointTolerance = 0.001d;
            Assert.AreEqual(new[,]
            {
                { 0.066f, 0.133f, -0.066f },
                { -0.066f, 0.088f, 0.4f },
                { 0.133f, -0.066f, -0.133f }
            }, mInverted.ToArray2D());
            GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
        }

        [Test]
        public void TestInvertMatrixStatic()
        {
            MatrixFloat m = new MatrixFloat(new[,]
            {
                { 1f, 2f },
                { 3f, 4f },
            });

            MatrixFloat mInverted = MatrixFloat.InvertByDeterminant(m);

            GlobalSettings.DefaultFloatingPointTolerance = 0.001d;
            Assert.AreEqual(new[,]
            {
                { -2f, 1f },
                { 1.5f, -0.5f },
            }, mInverted.ToArray2D());
            GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
        }

        [Test]
        public void TestInvertImpossibleMatrix()
        {
            MatrixFloat m = new MatrixFloat(new[,]
            {
                { 1f, 2f, 3f },
                { 4f, 5f, 6f },
                { 7f, 8f, 9f },
            });

            Assert.Throws<MatrixInvertException>(() =>
            {
                MatrixFloat mInverted = m.InvertByDeterminant();
            });
        }
    }

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

        public MatrixFloat InvertByDeterminant()
        {
            return InvertByDeterminant(this);
        }

        public static MatrixFloat InvertByDeterminant(MatrixFloat matrix)
        {
            float determinant = CalculateDeterminant(matrix._matrix);

            if (Math.Abs(determinant) < 1e-6)
            {
                throw new MatrixInvertException("Matrix is not invertible.");
            }

            float[,] adjugate = CalculateAdjugate(matrix._matrix);
            float[,] inverted = MultiplyByScalar(adjugate, 1 / determinant);

            return new MatrixFloat(inverted);
        }

        private static float[,] CalculateAdjugate(float[,] matrix)
        {
            int size = matrix.GetLength(0);
            float[,] adjugate = new float[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    float[,] minor = GetMinor(matrix, i, j);
                    adjugate[j, i] = (float)(Math.Pow(-1, i + j) * CalculateDeterminant(minor));
                }
            }

            return adjugate;
        }

        private static float[,] GetMinor(float[,] matrix, int row, int col)
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

            return minor;
        }

        private static float CalculateDeterminant(float[,] matrix)
        {
            int size = matrix.GetLength(0);

            if (size == 1) return matrix[0, 0];

            if (size == 2)
            {
                return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
            }

            float determinant = 0;

            for (int col = 0; col < size; col++)
            {
                float[,] minor = GetMinor(matrix, 0, col);
                determinant += (float)(Math.Pow(-1, col) * matrix[0, col] * CalculateDeterminant(minor));
            }

            return determinant;
        }

        private static float[,] MultiplyByScalar(float[,] matrix, float scalar)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            float[,] result = new float[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = matrix[i, j] * scalar;
                }
            }

            return result;
        }
    }

    public class MatrixInvertException : Exception
    {
        public MatrixInvertException(string message) : base(message) { }
    }

    public static class GlobalSettings
    {
        public static double DefaultFloatingPointTolerance { get; set; } = 0.0d;
    }
}
