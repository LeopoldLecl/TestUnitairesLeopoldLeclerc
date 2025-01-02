using System;
using NUnit.Framework;

namespace Maths_Matrices.Tests
{
    [TestFixture]
    public class Tests16_TransformationMatrices
    {
        [Test]
        public void TestTranslatePoint()
        {
            GlobalSettings.DefaultFloatingPointTolerance = 0.001d;

            Vector4 v = new Vector4(1f, 0f, 0f, 1f);
            MatrixFloat m = new MatrixFloat(new[,]
            {
                { 1f, 0f, 0f, 5f },
                { 0f, 1f, 0f, 3f },
                { 0f, 0f, 1f, 1f },
                { 0f, 0f, 0f, 1f },
            });

            Vector4 vTransformed = m * v;
            Assert.AreEqual(6f, vTransformed.x, GlobalSettings.DefaultFloatingPointTolerance);
            Assert.AreEqual(3f, vTransformed.y, GlobalSettings.DefaultFloatingPointTolerance);
            Assert.AreEqual(1f, vTransformed.z, GlobalSettings.DefaultFloatingPointTolerance);

            Vector4 vTransformedInverted = m.InvertByRowReduction() * vTransformed;
            Assert.AreEqual(1f, vTransformedInverted.x, GlobalSettings.DefaultFloatingPointTolerance);
            Assert.AreEqual(0f, vTransformedInverted.y, GlobalSettings.DefaultFloatingPointTolerance);
            Assert.AreEqual(0f, vTransformedInverted.z, GlobalSettings.DefaultFloatingPointTolerance);

            vTransformedInverted = m.InvertByDeterminant() * vTransformed;
            Assert.AreEqual(1f, vTransformedInverted.x, GlobalSettings.DefaultFloatingPointTolerance);
            Assert.AreEqual(0f, vTransformedInverted.y, GlobalSettings.DefaultFloatingPointTolerance);
            Assert.AreEqual(0f, vTransformedInverted.z, GlobalSettings.DefaultFloatingPointTolerance);

            GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
        }

        [Test]
        public void TestTranslateDirection()
        {
            GlobalSettings.DefaultFloatingPointTolerance = 0.001d;

            Vector4 v = new Vector4(1f, 0f, 0f, 0f);
            MatrixFloat m = new MatrixFloat(new[,]
            {
                { 1f, 0f, 0f, 5f },
                { 0f, 1f, 0f, 3f },
                { 0f, 0f, 1f, 1f },
                { 0f, 0f, 0f, 1f },
            });
            Vector4 vTransformed = m * v;

            Assert.AreEqual(1f, vTransformed.x, GlobalSettings.DefaultFloatingPointTolerance);
            Assert.AreEqual(0f, vTransformed.y, GlobalSettings.DefaultFloatingPointTolerance);
            Assert.AreEqual(0f, vTransformed.z, GlobalSettings.DefaultFloatingPointTolerance);

            Vector4 vTransformedInverted = m.InvertByRowReduction() * vTransformed;
            Assert.AreEqual(1f, vTransformedInverted.x, GlobalSettings.DefaultFloatingPointTolerance);
            Assert.AreEqual(0f, vTransformedInverted.y, GlobalSettings.DefaultFloatingPointTolerance);
            Assert.AreEqual(0f, vTransformedInverted.z, GlobalSettings.DefaultFloatingPointTolerance);

            vTransformedInverted = m.InvertByDeterminant() * vTransformed;
            Assert.AreEqual(1f, vTransformedInverted.x, GlobalSettings.DefaultFloatingPointTolerance);
            Assert.AreEqual(0f, vTransformedInverted.y, GlobalSettings.DefaultFloatingPointTolerance);
            Assert.AreEqual(0f, vTransformedInverted.z, GlobalSettings.DefaultFloatingPointTolerance);

            GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
        }

        [Test]
        public void TestScalePoint()
        {
            GlobalSettings.DefaultFloatingPointTolerance = 0.001d;

            Vector4 v = new Vector4(2f, 1f, 3f, 1f);
            MatrixFloat m = new MatrixFloat(new[,]
            {
                { 0.5f, 0f, 0f, 0f },
                { 0.0f, 2f, 0f, 0f },
                { 0.0f, 0f, 3f, 0f },
                { 0.0f, 0f, 0f, 1f },
            });

            Vector4 vTransformed = m * v;
            Assert.AreEqual(1f, vTransformed.x, GlobalSettings.DefaultFloatingPointTolerance);
            Assert.AreEqual(2f, vTransformed.y, GlobalSettings.DefaultFloatingPointTolerance);
            Assert.AreEqual(9f, vTransformed.z, GlobalSettings.DefaultFloatingPointTolerance);

            Vector4 vTransformedInverted = m.InvertByRowReduction() * vTransformed;
            Assert.AreEqual(2f, vTransformedInverted.x, GlobalSettings.DefaultFloatingPointTolerance);
            Assert.AreEqual(1f, vTransformedInverted.y, GlobalSettings.DefaultFloatingPointTolerance);
            Assert.AreEqual(3f, vTransformedInverted.z, GlobalSettings.DefaultFloatingPointTolerance);

            vTransformedInverted = m.InvertByDeterminant() * vTransformed;
            Assert.AreEqual(2f, vTransformedInverted.x, GlobalSettings.DefaultFloatingPointTolerance);
            Assert.AreEqual(1f, vTransformedInverted.y, GlobalSettings.DefaultFloatingPointTolerance);
            Assert.AreEqual(3f, vTransformedInverted.z, GlobalSettings.DefaultFloatingPointTolerance);

            GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
        }

        [Test]
        public void TestRotatePoint()
        {
            GlobalSettings.DefaultFloatingPointTolerance = 0.001d;

            Vector4 v = new Vector4(1f, 4f, 7f, 1f);
            double a = Math.PI / 2d;
            float cosA = Math.Abs(a - Math.PI / 2d) < 1e-10 ? 0f : (float)Math.Cos(a);
            float sinA = Math.Abs(a - Math.PI / 2d) < 1e-10 ? 1f : (float)Math.Sin(a);
            MatrixFloat m = new MatrixFloat(new[,]
            {
                { cosA, -sinA, 0f, 0f },
                { sinA, cosA, 0f, 0f },
                { 0f, 0f, 1f, 0f },
                { 0f, 0f, 0f, 1f },
            });

            Vector4 vTransformed = m * v;
            Assert.AreEqual(-4f, vTransformed.x, GlobalSettings.DefaultFloatingPointTolerance);
            Assert.AreEqual(1f, vTransformed.y, GlobalSettings.DefaultFloatingPointTolerance);
            Assert.AreEqual(7f, vTransformed.z, GlobalSettings.DefaultFloatingPointTolerance);

            Vector4 vTransformedInverted = m.InvertByRowReduction() * vTransformed;
            Assert.AreEqual(1f, vTransformedInverted.x, GlobalSettings.DefaultFloatingPointTolerance);
            Assert.AreEqual(4f, vTransformedInverted.y, GlobalSettings.DefaultFloatingPointTolerance);
            Assert.AreEqual(7f, vTransformedInverted.z, GlobalSettings.DefaultFloatingPointTolerance);

            vTransformedInverted = m.InvertByDeterminant() * vTransformed;
            Assert.AreEqual(1f, vTransformedInverted.x, GlobalSettings.DefaultFloatingPointTolerance);
            Assert.AreEqual(4f, vTransformedInverted.y, GlobalSettings.DefaultFloatingPointTolerance);
            Assert.AreEqual(7f, vTransformedInverted.z, GlobalSettings.DefaultFloatingPointTolerance);

            GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
        }
    }

    public static class GlobalSettings
    {
        public static double DefaultFloatingPointTolerance = 0.0d;
    }

    public class Vector4
    {
        public float x, y, z, w;

        public Vector4(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
    }

    public class MatrixFloat
    {
        public float[,] Values { get; }

        public MatrixFloat(float[,] values)
        {
            Values = values;
        }

        public static Vector4 operator *(MatrixFloat matrix, Vector4 vector)
        {
            float x = matrix.Values[0, 0] * vector.x +
                      matrix.Values[0, 1] * vector.y +
                      matrix.Values[0, 2] * vector.z +
                      matrix.Values[0, 3] * vector.w;

            float y = matrix.Values[1, 0] * vector.x +
                      matrix.Values[1, 1] * vector.y +
                      matrix.Values[1, 2] * vector.z +
                      matrix.Values[1, 3] * vector.w;

            float z = matrix.Values[2, 0] * vector.x +
                      matrix.Values[2, 1] * vector.y +
                      matrix.Values[2, 2] * vector.z +
                      matrix.Values[2, 3] * vector.w;

            float w = matrix.Values[3, 0] * vector.x +
                      matrix.Values[3, 1] * vector.y +
                      matrix.Values[3, 2] * vector.z +
                      matrix.Values[3, 3] * vector.w;

            return new Vector4(x, y, z, w);
        }

        public MatrixFloat InvertByRowReduction()
        {
            int size = Values.GetLength(0);
            float[,] identity = CreateIdentityMatrix(size);
            float[,] augmented = AugmentMatrix(Values, identity);

            PerformGaussianElimination(augmented);

            float[,] inverted = ExtractRightHalf(augmented);
            return new MatrixFloat(inverted);
        }

        public MatrixFloat InvertByDeterminant()
        {
            float determinant = CalculateDeterminant(Values);
            if (Math.Abs(determinant) < GlobalSettings.DefaultFloatingPointTolerance)
                throw new InvalidOperationException("Matrix is not invertible (determinant is zero).");

            float[,] adjoint = CalculateAdjoint(Values);
            float[,] inverse = ScaleMatrix(adjoint, 1f / determinant);
            return new MatrixFloat(inverse);
        }

        private static float[,] CreateIdentityMatrix(int size)
        {
            float[,] identity = new float[size, size];
            for (int i = 0; i < size; i++)
                identity[i, i] = 1f;
            return identity;
        }

        private static float[,] AugmentMatrix(float[,] original, float[,] identity)
        {
            int size = original.GetLength(0);
            float[,] augmented = new float[size, size * 2];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    augmented[i, j] = original[i, j];
                    augmented[i, j + size] = identity[i, j];
                }
            }
            return augmented;
        }

        private static void PerformGaussianElimination(float[,] matrix)
        {
            int size = matrix.GetLength(0);
            for (int i = 0; i < size; i++)
            {
                NormalizeRow(matrix, i);
                EliminateOtherRows(matrix, i);
            }
        }

        private static void NormalizeRow(float[,] matrix, int row)
        {
            int size = matrix.GetLength(0);
            float pivot = matrix[row, row];
            if (Math.Abs(pivot) < GlobalSettings.DefaultFloatingPointTolerance)
                throw new InvalidOperationException("Matrix is singular and cannot be inverted.");

            for (int j = 0; j < size * 2; j++)
                matrix[row, j] /= pivot;
        }

        private static void EliminateOtherRows(float[,] matrix, int row)
        {
            int size = matrix.GetLength(0);
            for (int i = 0; i < size; i++)
            {
                if (i == row) continue;

                float factor = matrix[i, row];
                for (int j = 0; j < size * 2; j++)
                    matrix[i, j] -= factor * matrix[row, j];
            }
        }

        private static float[,] ExtractRightHalf(float[,] augmented)
        {
            int size = augmented.GetLength(0);
            float[,] result = new float[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    result[i, j] = augmented[i, j + size];
            return result;
        }

        private static float CalculateDeterminant(float[,] matrix)
        {
            int size = matrix.GetLength(0);
            if (size == 1)
            {
                return matrix[0, 0];
            }
            if (size == 2)
            {
                return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
            }

            float determinant = 0f;
            for (int i = 0; i < size; i++)
            {
                determinant += (i % 2 == 0 ? 1f : -1f) * matrix[0, i] * CalculateDeterminant(Minor(matrix, 0, i));
            }
            return determinant;
        }

        private static float[,] Minor(float[,] matrix, int row, int col)
        {
            int size = matrix.GetLength(0);
            float[,] minor = new float[size - 1, size - 1];

            int r = 0, c;
            for (int i = 0; i < size; i++)
            {
                if (i == row) continue;
                c = 0;
                for (int j = 0; j < size; j++)
                {
                    if (j == col) continue;
                    minor[r, c] = matrix[i, j];
                    c++;
                }
                r++;
            }
            return minor;
        }

        private static float[,] CalculateAdjoint(float[,] matrix)
        {
            int size = matrix.GetLength(0);
            float[,] adjoint = new float[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    float[,] minor = Minor(matrix, i, j);
                    adjoint[j, i] = ((i + j) % 2 == 0 ? 1f : -1f) * CalculateDeterminant(minor);
                }
            }
            return adjoint;
        }

        private static float[,] ScaleMatrix(float[,] matrix, float scalar)
        {
            int size = matrix.GetLength(0);
            float[,] scaled = new float[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    scaled[i, j] = matrix[i, j] * scalar;
            return scaled;
        }
    }
}
