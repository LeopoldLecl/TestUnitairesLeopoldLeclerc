using System;
using NUnit.Framework;

namespace Maths_Matrices
{
    [TestFixture]
    public class Tests11_InvertMatricesUsingRowReduction
    {
        [Test]
        public void TestInvertMatrixInstance()
        {
            MatrixFloat m = new MatrixFloat(new[,]
            {
                { 2f, 3f, 8f },
                { 6f, 0f, -3f },
                { -1f, 3f, 2f },
            });

            MatrixFloat mInverted = m.InvertByRowReduction();

            GlobalSettings.DefaultFloatingPointTolerance = 0.0001d;
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

            MatrixFloat mInverted = m.InvertByRowReduction();

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
                MatrixFloat mInverted = m.InvertByRowReduction();
            });
        }
    }

    public class MatrixFloat
    {
        private float[,] data;

        public MatrixFloat(float[,] data)
        {
            this.data = data;
        }

        public float[,] ToArray2D()
        {
            return this.data;
        }

        public MatrixFloat InvertByRowReduction()
        {
            int n = this.data.GetLength(0);
            if (n != this.data.GetLength(1))
            {
                throw new MatrixInvertException("La matrice n'est pas carrée et donc non inversible.");
            }

            float[,] augmentedMatrix = new float[n, 2 * n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    augmentedMatrix[i, j] = this.data[i, j];
                    augmentedMatrix[i, n + j] = (i == j) ? 1 : 0;
                }
            }

            for (int i = 0; i < n; i++)
            {
                float diagonalElement = augmentedMatrix[i, i];
                if (Math.Abs(diagonalElement) < GlobalSettings.DefaultFloatingPointTolerance)
                {
                    throw new MatrixInvertException("La matrice est non inversible (déterminant nul ou proche de zéro).");
                }

                for (int j = 0; j < 2 * n; j++)
                {
                    augmentedMatrix[i, j] /= diagonalElement;
                }

                for (int k = 0; k < n; k++)
                {
                    if (k != i)
                    {
                        float rowMultiplier = augmentedMatrix[k, i];
                        for (int j = 0; j < 2 * n; j++)
                        {
                            augmentedMatrix[k, j] -= rowMultiplier * augmentedMatrix[i, j];
                        }
                    }
                }
            }

            float[,] invertedMatrix = new float[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    invertedMatrix[i, j] = augmentedMatrix[i, n + j];
                }
            }

            return new MatrixFloat(invertedMatrix);
        }
    }

    public class MatrixInvertException : Exception
    {
        public MatrixInvertException(string message) : base(message) { }
    }

    public static class GlobalSettings
    {
        public static double DefaultFloatingPointTolerance = 0.001d;
    }
}
