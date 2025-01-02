using NUnit.Framework;

namespace Maths_Matrices.Tests
{
    [TestFixture]
    public class Tests08_ElementaryOperations
    {
        #region Swaps Tests

        [Test]
        public void TestSwapLines()
        {
            MatrixInt m = new MatrixInt(new[,]
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 }
            });

            MatrixElementaryOperations.SwapLines(m, 0, 1);
            Assert.AreEqual(new[,]
            {
                { 4, 5, 6 },
                { 1, 2, 3 },
                { 7, 8, 9 }
            }, m.ToArray2D());

            MatrixElementaryOperations.SwapLines(m, 0, 2);
            Assert.AreEqual(new[,]
            {
                { 7, 8, 9 },
                { 1, 2, 3 },
                { 4, 5, 6 }
            }, m.ToArray2D());

            MatrixElementaryOperations.SwapLines(m, 2, 1);
            Assert.AreEqual(new[,]
            {
                { 7, 8, 9 },
                { 4, 5, 6 },
                { 1, 2, 3 }
            }, m.ToArray2D());
        }

        [Test]
        public void TestSwapColumns()
        {
            MatrixInt m = new MatrixInt(new[,]
            {
                { 1, 4, 7 },
                { 2, 5, 8 },
                { 3, 6, 9 }
            });

            MatrixElementaryOperations.SwapColumns(m, 0, 1);
            Assert.AreEqual(new[,]
            {
                { 4, 1, 7 },
                { 5, 2, 8 },
                { 6, 3, 9 }
            }, m.ToArray2D());

            MatrixElementaryOperations.SwapColumns(m, 0, 2);
            Assert.AreEqual(new[,]
            {
                { 7, 1, 4 },
                { 8, 2, 5 },
                { 9, 3, 6 }
            }, m.ToArray2D());

            MatrixElementaryOperations.SwapColumns(m, 2, 1);
            Assert.AreEqual(new[,]
            {
                { 7, 4, 1 },
                { 8, 5, 2 },
                { 9, 6, 3 }
            }, m.ToArray2D());
        }

        #endregion

        #region Multiply Lines/Columns Tests
        
        [Test]
        public void TestMultiplyLine()
        {
            MatrixInt m = new MatrixInt(new[,]
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 }
            });
        
            MatrixElementaryOperations.MultiplyLine(m, 0, 2);
            Assert.AreEqual(new[,]
            {
                { 2, 4, 6 },
                { 4, 5, 6 },
                { 7, 8, 9 }
            }, m.ToArray2D());
        
            MatrixElementaryOperations.MultiplyLine(m, 1, 3);
            Assert.AreEqual(new[,]
            {
                { 2, 4, 6 },
                { 12, 15, 18 },
                { 7, 8, 9 }
            }, m.ToArray2D());
        
            MatrixElementaryOperations.MultiplyLine(m, 2, 10);
            Assert.AreEqual(new[,]
            {
                { 2, 4, 6 },
                { 12, 15, 18 },
                { 70, 80, 90 }
            }, m.ToArray2D());
            
            Assert.Throws<MatrixScalarZeroException>(() =>
            {
                MatrixElementaryOperations.MultiplyLine(m, 0, 0);
            });
        }
        
        [Test]
        public void TestMultiplyColumn()
        {
            MatrixInt m = new MatrixInt(new[,]
            {
                { 1, 4, 7 },
                { 2, 5, 8 },
                { 3, 6, 9 }
            });
        
            MatrixElementaryOperations.MultiplyColumn(m, 0, 2);
            Assert.AreEqual(new[,]
            {
                { 2, 4, 7 },
                { 4, 5, 8 },
                { 6, 6, 9 }
            }, m.ToArray2D());
        
            MatrixElementaryOperations.MultiplyColumn(m, 1, 3);
            Assert.AreEqual(new[,]
            {
                { 2, 12, 7 },
                { 4, 15, 8 },
                { 6, 18, 9 }
            }, m.ToArray2D());
        
            MatrixElementaryOperations.MultiplyColumn(m, 2, 10);
            Assert.AreEqual(new[,]
            {
                { 2, 12, 70 },
                { 4, 15, 80 },
                { 6, 18, 90 }
            }, m.ToArray2D());
            
            Assert.Throws<MatrixScalarZeroException>(() =>
            {
                MatrixElementaryOperations.MultiplyColumn(m, 0, 0);
            });
        }
        
        #endregion
        
        #region Add Lines/Columns to another (with factor)
        
        [Test]
        public void TestAddLineToAnother()
        {
            MatrixInt m = new MatrixInt(new[,]
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 }
            });
        
            MatrixElementaryOperations.AddLineToAnother(m, 1, 0, 2);
            Assert.AreEqual(new[,]
            {
                { 9, 12, 15 },
                { 4, 5, 6 },
                { 7, 8, 9 }
            }, m.ToArray2D());
        }
        
        [Test]
        public void TestAddColumnToAnother()
        {
            MatrixInt m = new MatrixInt(new[,]
            {
                { 1, 4, 7 },
                { 2, 5, 8 },
                { 3, 6, 9 }
            });
        
            MatrixElementaryOperations.AddColumnToAnother(m, 1, 0, 2);
            Assert.AreEqual(new[,]
            {
                { 9, 4, 7 },
                { 12, 5, 8 },
                { 15, 6, 9 }
            }, m.ToArray2D());
        }
        
        #endregion
    }
    
    public class MatrixInt
    {
        
        public int numberOfRow;
        public int numberOfCollumn;
        public int[,] matrix;
        
        public int this[int row, int column]
        {
            get { return matrix[row, column]; }
            set { matrix[row, column] = value; }    
        }
        
        public MatrixInt(int rows, int columns)
        {
            numberOfRow = rows;
            numberOfCollumn = columns;
            matrix = new int[rows, columns];
        }

        public MatrixInt(int[,] matrix)
        {
            this.matrix = matrix;
            this.numberOfRow = matrix.GetLength(0);
            this.numberOfCollumn = matrix.GetLength(1);
        }

        public MatrixInt(MatrixInt newMatrix)
        {
            numberOfRow = newMatrix.numberOfRow;
            numberOfCollumn = newMatrix.numberOfCollumn;
            matrix = newMatrix.ToArray2D();
        }

        public int[,] ToArray2D()
        {
            int[,] newMatrix = new int[numberOfRow, numberOfCollumn];
            
            for (int i = 0; i < numberOfRow; i++)
            {
                for (int j = 0; j < numberOfCollumn; j++)
                {
                    newMatrix[i, j] = matrix[i, j];
                }
            }
            return newMatrix;
        }
    }

    public class MatrixElementaryOperations
    {
        public static void SwapLines(MatrixInt m, int row1, int row2)
        {
            if (row1 == row2) return;

            for (int col = 0; col < m.numberOfCollumn; col++)
            {
                int temp = m[row1, col];
                m[row1, col] = m[row2, col];
                m[row2, col] = temp;
            }
        }

        public static void SwapColumns(MatrixInt m, int col1, int col2)
        {
            if (col1 == col2) return;

            for (int row = 0; row < m.numberOfRow; row++)
            {
                int temp = m[row, col1];
                m[row, col1] = m[row, col2];
                m[row, col2] = temp;
            }
        }
        
        public static void MultiplyLine(MatrixInt m, int row, int scalar)
        {
            if (scalar == 0)
                throw new MatrixScalarZeroException("Multiplying by zero is not allowed.");

            for (int col = 0; col < m.numberOfCollumn; col++)
            {
                m[row, col] *= scalar;
            }
        }
        
        public static void MultiplyColumn(MatrixInt m, int col, int scalar)
        {
            if (scalar == 0)
                throw new MatrixScalarZeroException("Multiplying by zero is not allowed.");

            for (int row = 0; row < m.numberOfRow; row++)
            {
                m[row, col] *= scalar;
            }
        }
        
        public static void AddLineToAnother(MatrixInt m, int sourceRow, int targetRow, int factor)
        {
            if (factor == 0) return;  // Si le facteur est zéro, rien ne se passe

            for (int col = 0; col < m.numberOfCollumn; col++)
            {
                m[targetRow, col] += m[sourceRow, col] * factor;
            }
        }
        
        public static void AddColumnToAnother(MatrixInt m, int sourceCol, int targetCol, int factor)
        {
            if (factor == 0) return;  // Si le facteur est zéro, rien ne se passe

            for (int row = 0; row < m.numberOfRow; row++)
            {
                m[row, targetCol] += m[row, sourceCol] * factor;
            }
        }
        
    }
    
    public class MatrixScalarZeroException : Exception
    {
        public MatrixScalarZeroException(string message) : base(message)
        {
        }
    }
      
    
}