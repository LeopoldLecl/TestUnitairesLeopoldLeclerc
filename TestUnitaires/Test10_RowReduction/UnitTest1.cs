using NUnit.Framework;

namespace Maths_Matrices.Tests
{
    [TestFixture]
    public class Tests10_RowReduction
    {
        [Test]
        public void TestApplyRowReduction_CourseExample()
        {
            MatrixFloat m1 = new MatrixFloat(new[,]
            {
                { 3f, 2f, -3f },
                { 4f, -3f, 6f },
                { 1f, 0f, -1f }
            });

            MatrixFloat m2 = new MatrixFloat(new[,]
            {
                { -13f },
                { 7f },
                { -5f }
            });

            (m1, m2) = MatrixRowReductionAlgorithm.Apply(m1, m2);
            GlobalSettings.DefaultFloatingPointTolerance = 0.001f;

            AssertMatricesEqual(new[,]
            {
                { 1f, 0f, 0f },
                { 0f, 1f, 0f },
                { 0f, 0f, 1f }
            }, m1.ToArray2D());

            AssertMatricesEqual(new[,]
            {
                { -2f },
                { 1f },
                { 3f }
            }, m2.ToArray2D());

            GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
        }

        [Test]
        public void TestApplyRowReduction_Exercise()
        {
            MatrixFloat m1 = new MatrixFloat(new[,]
            {
                { 2f, 1f, 3f },
                { 0f, 1f, -1f },
                { 1f, 3f, -1f }
            });

            MatrixFloat m2 = new MatrixFloat(new[,]
            {
                { 0f },
                { 0f },
                { 0f }
            });

            (m1, m2) = MatrixRowReductionAlgorithm.Apply(m1, m2);
            GlobalSettings.DefaultFloatingPointTolerance = 0.001f;

            AssertMatricesEqual(new[,]
            {
                { 1f, 0f, 2f },
                { 0f, 1f, -1f },
                { 0f, 0f, 0f }
            }, m1.ToArray2D());

            AssertMatricesEqual(new[,]
            {
                { 0f },
                { 0f },
                { 0f }
            }, m2.ToArray2D());

            GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
        }

        private void AssertMatricesEqual(float[,] expected, float[,] actual)
        {
            int rows = expected.GetLength(0);
            int cols = expected.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Assert.AreEqual(
                        Math.Round(expected[i, j], 6),
                        Math.Round(actual[i, j], 6),
                        GlobalSettings.DefaultFloatingPointTolerance,
                        $"Values differ at index [{i},{j}]"
                    );
                }
            }
        }
    }

    public static class GlobalSettings
    {
        public static double DefaultFloatingPointTolerance { get; set; } = 0.0d;
    }

    public class MatrixFloat
    {
        private readonly float[,] _data;

        public int Rows => _data.GetLength(0);
        public int Columns => _data.GetLength(1);

        public MatrixFloat(float[,] data)
        {
            _data = data;
        }

        public float[,] ToArray2D()
        {
            return (float[,])_data.Clone();
        }

        public float this[int row, int col]
        {
            get => _data[row, col];
            set => _data[row, col] = value;
        }

        public void SwapRows(int row1, int row2)
        {
            for (int i = 0; i < Columns; i++)
            {
                float temp = _data[row1, i];
                _data[row1, i] = _data[row2, i];
                _data[row2, i] = temp;
            }
        }

        public void MultiplyRow(int row, float scalar)
        {
            for (int i = 0; i < Columns; i++)
            {
                _data[row, i] *= scalar;
            }
        }

        public void AddRowMultiple(int targetRow, int sourceRow, float scalar)
        {
            for (int i = 0; i < Columns; i++)
            {
                _data[targetRow, i] += _data[sourceRow, i] * scalar;
            }
        }
    }

    public static class MatrixRowReductionAlgorithm
    {
        public static (MatrixFloat, MatrixFloat) Apply(MatrixFloat m1, MatrixFloat m2)
        {
            int rows = m1.Rows;

            for (int pivot = 0; pivot < rows; pivot++)
            {
                if (Math.Abs(m1[pivot, pivot]) > GlobalSettings.DefaultFloatingPointTolerance)
                {
                    float pivotValue = m1[pivot, pivot];
                    m1.MultiplyRow(pivot, 1f / pivotValue);
                    m2.MultiplyRow(pivot, 1f / pivotValue);
                }

                for (int row = 0; row < rows; row++)
                {
                    if (row != pivot && Math.Abs(m1[row, pivot]) > GlobalSettings.DefaultFloatingPointTolerance)
                    {
                        float factor = -m1[row, pivot];
                        m1.AddRowMultiple(row, pivot, factor);
                        m2.AddRowMultiple(row, pivot, factor);
                    }
                }
            }

            return (m1, m2);
        }
    }
}
