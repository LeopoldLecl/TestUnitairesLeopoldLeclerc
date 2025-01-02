namespace Test05_AddAndSubstract;

public class Tests
{
  [TestFixture]
    public class Tests05_MatricesAddAndSubtract
    {
        [Test]
        public void TestSumMatricesInstances()
        {
            MatrixInt m1 = new MatrixInt(new[,]
            {
                { 1, 7 },
                { 8, 5 },
                { 4, 17 }
            });

            MatrixInt m2 = new MatrixInt(new[,]
            {
                { 65, 4 },
                { 3, 1 },
                { 48, 2 }
            });

            m1.Add(m2);

            Assert.AreEqual(new[,]
            {
                { 66, 11 },
                { 11, 6 },
                { 52, 19 },
            }, m1.ToArray2D());
            
            Assert.AreEqual(new[,]
            {
                { 65, 4 },
                { 3, 1 },
                { 48, 2 }
            }, m2.ToArray2D());
        }
        
        [Test]
        public void TestSumMatricesStatic()
        {
            MatrixInt m1 = new MatrixInt(new[,]
            {
                { 1, 7 },
                { 8, 5 },
                { 4, 17 }
            });

            MatrixInt m2 = new MatrixInt(new[,]
            {
                { 65, 4 },
                { 3, 1 },
                { 48, 2 }
            });

            MatrixInt m3 = MatrixInt.Add(m1, m2);
            
            Assert.AreEqual(new[,]
            {
                { 66, 11 },
                { 11, 6 },
                { 52, 19 },
            }, m3.ToArray2D());
            
            Assert.AreEqual(new[,]
            {
                { 1, 7 },
                { 8, 5 },
                { 4, 17 }
            }, m1.ToArray2D());
            
            Assert.AreEqual(new[,]
            {
                { 65, 4 },
                { 3, 1 },
                { 48, 2 }
            }, m2.ToArray2D());
        }
        
        [Test]
        public void TestSumMatricesOperator()
        {
            MatrixInt m1 = new MatrixInt(new[,]
            {
                { 1, 7 },
                { 8, 5 },
                { 4, 17 }
            });

            MatrixInt m2 = new MatrixInt(new[,]
            {
                { 65, 4 },
                { 3, 1 },
                { 48, 2 }
            });

            MatrixInt m3 = m1 + m2;
            
            Assert.AreEqual(new[,]
            {
                { 66, 11 },
                { 11, 6 },
                { 52, 19 },
            }, m3.ToArray2D());
            
            Assert.AreEqual(new[,]
            {
                { 1, 7 },
                { 8, 5 },
                { 4, 17 }
            }, m1.ToArray2D());
            
            Assert.AreEqual(new[,]
            {
                { 65, 4 },
                { 3, 1 },
                { 48, 2 }
            }, m2.ToArray2D());
        }

        [Test]
        public void TestSubtractMatricesOperator()
        {
            MatrixInt m1 = new MatrixInt(new[,]
            {
                { 1, 62 },
                { 17, 2 },
                { 3, 5 },
            });

            MatrixInt m2 = new MatrixInt(new[,]
            {
                {-3, 51},
                {9, 1},
                {4, 18},
            });

            MatrixInt m3 = m1 - m2;
            
            Assert.AreEqual(new[,]
            {
                { 4, 11 },
                { 8, 1 },
                { -1, -13 },
            }, m3.ToArray2D());
        }
        
        [Test]
        public void TestImpossibleSumMatrices()
        {
            MatrixInt m1 = new MatrixInt(new[,]
            {
                { 3, 4 },
                { 8, 5 },
            });

            MatrixInt m2 = new MatrixInt(new[,]
            {
                { 1, 7 },
                { 7, 4 },
                { 2, 0 },
            });
            
            //Add Methods need to throw exception if size are different
            //See Exception Documentation =>
            //https://docs.microsoft.com/fr-fr/dotnet/csharp/fundamentals/exceptions/
            //https://docs.microsoft.com/fr-fr/dotnet/api/system.exception?view=net-6.0
            
            Assert.Throws<MatrixSumException>(() =>
            {
                m1.Add(m2);
            });
            
            Assert.Throws<MatrixSumException>(() =>
            {
                MatrixInt.Add(m1, m2);
            });
            
            Assert.Throws<MatrixSumException>(() =>
            {
                MatrixInt m3 = m1 + m2;
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
        public static MatrixInt operator +(MatrixInt m1, MatrixInt m2) => Add(m1,m2);
        public static MatrixInt operator -(MatrixInt m1, MatrixInt m2) => Subtract(m1,m2);

        

        
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

        public static MatrixInt Add(MatrixInt m1, MatrixInt m2)
        {
            MatrixInt result = new MatrixInt(m1.numberOfRow, m1.numberOfCollumn);

            if (m1.numberOfCollumn != m2.numberOfCollumn || m1.numberOfRow != m2.numberOfRow)
            {
                throw new MatrixSumException();
            }
            for (int i = 0; i < m1.numberOfRow; i++)
            {
                for (int j = 0; j < m1.numberOfCollumn; j++)
                {
                    result.matrix[i, j] = m1.matrix[i, j] + m2.matrix[i, j];
                }
            }

            return new MatrixInt(result.matrix);
        }
        
        public MatrixInt Add(MatrixInt m1)
        {

            if (m1.numberOfCollumn != this.numberOfCollumn || m1.numberOfRow != this.numberOfRow)
            {
                throw new MatrixSumException();
            }
            for (int i = 0; i < numberOfRow; i++)
            {
                for (int j = 0; j < numberOfCollumn; j++)
                {
                    matrix[i, j] = matrix[i, j] + m1.matrix[i, j];
                }
            }

            return new MatrixInt(matrix);
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
    }

    public class MatrixSumException : Exception
    {
        
    }
}