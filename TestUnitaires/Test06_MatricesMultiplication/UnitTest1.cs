using NUnit.Framework;

namespace Test06_MatricesMultiplication
{
    [TestFixture]
    public class Tests06MatricesMultiplication
    {
        [Test]
        public void TestMatricesMultiplicationInstance()
        {
            MatrixInt m1 = new MatrixInt(new[,]
            {
                { 1, 2 },
                { 7, 10 },
                { 4, 5 },
                { 3, 1 }
            });

            MatrixInt m2 = new MatrixInt(new[,]
            {
                { 4, 4, 2, 2, 7 },
                { 1, 7, 1, 2, 0 }
            });

            MatrixInt m3 = m1.Multiply(m2);

            Assert.AreEqual(new[,]
            {
                { 6, 18, 4, 6, 7 },
                { 38, 98, 24, 34, 49 },
                { 21, 51, 13, 18, 28 },
                { 13, 19, 7, 8, 21 },
            }, m3.ToArray2D());
        }
        
        [Test]
        public void TestMatricesMultiplicationStatic()
        {
            MatrixInt m1 = new MatrixInt(new[,]
            {
                { 1, 4 },
                { 2, 1 },
                { 7, 5 }
            });

            MatrixInt m2 = new MatrixInt(new[,]
            {
                { 4, 3, 5 },
                { 1, 2, 1 }
            });

            MatrixInt m3 = MatrixInt.Multiply(m1, m2);

            Assert.AreEqual(new[,]
            {
                { 8, 11, 9 },
                { 9, 8, 11 },
                { 33, 31, 40 }
            }, m3.ToArray2D());
        }
        
        [Test]
        public void TestMatricesMultiplicationOperator()
        {
            MatrixInt m1 = new MatrixInt(new[,]
            {
                { 1, 4 },
                { 2, 2 },
                { 7, 6 },
                { 23, 1 },
            });

            MatrixInt m2 = new MatrixInt(new[,]
            {
                { 8, 5, 2 },
                { 3, 1, 2 }
            });

            MatrixInt m3 = m1 * m2;

            Assert.AreEqual(new[,]
            {
                { 20, 9, 10 },
                { 22, 12, 8 },
                { 74, 41, 26 },
                { 187, 116, 48 }
            }, m3.ToArray2D());
        }
        
        [Test]
        public void TestImpossibleMultiplication()
        {
            MatrixInt m1 = new MatrixInt(new[,]
            {
                { 1, 2 },
                { 3, 4 }
            });

            MatrixInt m2 = new MatrixInt(new[,]
            {
                { 0, 1, 5, 9 },
                { 7, 4, 20, 36 },
                { 2, 0, 87, 1 },
                { 0, 0, 0, 1 },
                { 2, 4, 6, 8 }
            });
            
            //Multiply Methods is possible only if m1.NbColumns and m2.NbLines are equals
            //Throw Exception instead
            //See Exception Documentation =>
            //https://docs.microsoft.com/fr-fr/dotnet/csharp/fundamentals/exceptions/
            //https://docs.microsoft.com/fr-fr/dotnet/api/system.exception?view=net-6.0
            
            Assert.Throws<MatrixMultiplyException>(() =>
            {
                m1.Multiply(m2);
            });
            
            Assert.Throws<MatrixMultiplyException>(() =>
            {
                MatrixInt.Multiply(m1, m2);
            });
            
            Assert.Throws<MatrixMultiplyException>(() =>
            {
                MatrixInt m3 = m1 * m2;
            });
        }
    }
    
      public class MatrixInt
    {
        
        public int numberOfRow;
        public int numberOfCollumn;
        public int[,] matrix;
        public static MatrixInt operator *(MatrixInt m, int multiplier) => Multiply(m, multiplier);
        public static MatrixInt operator *( int multiplier,MatrixInt m) => Multiply(multiplier,m);
        public static MatrixInt operator -(MatrixInt m) => SignSwitcher(m);
        public static MatrixInt operator -(MatrixInt m1, MatrixInt m2) => Subtract(m1,m2);
        public static MatrixInt operator *(MatrixInt m1, MatrixInt m2) => Multiply(m1, m2);

        

        
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

     
        
      


        
        public MatrixInt Multiply(int multiplicand)
        {
            for (int i = 0; i < numberOfRow; i++)
            {
                for (int j = 0; j < numberOfCollumn; j++)
                {
                    matrix[i, j] *= multiplicand;
                }
            }
            return new MatrixInt(matrix);
        }
        
        public static MatrixInt Multiply(MatrixInt oldMatrix,int multiplicand)
        {
            for (int i = 0; i < oldMatrix.numberOfRow; i++)
            {
                for (int j = 0; j < oldMatrix.numberOfCollumn; j++)
                {
                    oldMatrix.matrix[i, j] *= multiplicand;
                }
            }
            return new MatrixInt(oldMatrix.matrix);
        }
        
        public static MatrixInt Multiply(int multiplicand,MatrixInt oldMatrix)
        {
            for (int i = 0; i < oldMatrix.numberOfRow; i++)
            {
                for (int j = 0; j < oldMatrix.numberOfCollumn; j++)
                {
                    oldMatrix.matrix[i, j] *= multiplicand;
                }
            }
            return new MatrixInt(oldMatrix.matrix);
        }
        
        public static MatrixInt SignSwitcher(MatrixInt m1)
        {
            MatrixInt result = new MatrixInt(m1.numberOfRow, m1.numberOfCollumn);
            
            for (int i = 0; i < result.numberOfRow; i++)
            {
                for (int j = 0; j < result.numberOfCollumn; j++)
                {
                    result.matrix[i, j] = -m1.matrix[i, j];
                }
            }

            return new MatrixInt(result.matrix);
        }
        
        public static MatrixInt Subtract(MatrixInt m1, MatrixInt m2)
        {
            MatrixInt result = new MatrixInt(m1.numberOfRow, m1.numberOfCollumn);
            for (int i = 0; i < result.numberOfRow; i++)
            {
                for (int j = 0; j < result.numberOfCollumn; j++)
                {
                  result.matrix[i,j] = m1.matrix[i, j] - m2.matrix[i, j];
                }
            }

            return new MatrixInt(result.matrix);
        }
        
        public MatrixInt Multiply(MatrixInt m2)
        {
            if (numberOfCollumn != m2.numberOfRow) throw new MatrixMultiplyException();
            MatrixInt result = new MatrixInt(numberOfRow, m2.numberOfCollumn);
            for (int i = 0; i < result.numberOfRow; i++)
            {
                for (int j = 0; j < result.numberOfCollumn; j++)
                {
                    for (int k = 1; k < numberOfCollumn; k++)
                    {
                        int a = matrix[i, k - 1];
                        int b = m2.matrix[k - 1, j];
                        int c = matrix[i, k];
                        int d = m2.matrix[k, j];

                        // int e_firstM = Matrix[i, k] * m2.Matrix[k, j];
                        // int e_secondM = Matrix[i, l] * m2.Matrix[l, j];

                        result.matrix[i, j] = a * b + c * d;
                    }
                }
            }

            return result;
        }
        public static MatrixInt Multiply(MatrixInt m1, MatrixInt m2)
        {
            if (m1.numberOfCollumn != m2.numberOfRow) throw new MatrixMultiplyException();
            MatrixInt result = new MatrixInt(m1.numberOfRow, m2.numberOfCollumn);
            for (int i = 0; i < result.numberOfRow; i++)
            {
                for (int j = 0; j < result.numberOfCollumn; j++)
                {
                    for (int k = 1; k < m1.numberOfCollumn; k++)
                    {
                        int a = m1.matrix[i, k - 1];
                        int b = m2.matrix[k - 1, j];
                        int c = m1.matrix[i, k];
                        int d = m2.matrix[k, j];

                        // int e_firstM = Matrix[i, k] * m2.Matrix[k, j];
                        // int e_secondM = Matrix[i, l] * m2.Matrix[l, j];

                        result.matrix[i, j] = a * b + c * d;
                    }
                }
            }

            return result;
        }
    }
      
    
        public class MatrixMultiplyException : Exception;
    
}
