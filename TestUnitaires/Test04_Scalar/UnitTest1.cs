namespace Test04_Scalar;

public class Tests
{
 [TestFixture]
    public class Tests04_ScalarMultiplication
    {
        [Test]
        public void TestScalarMultiplicationInstance()
        {
            MatrixInt m = new MatrixInt(new[,]
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 }
            });

            m.Multiply(2);

            Assert.AreEqual(new[,]
            {
                { 2, 4, 6 },
                { 8, 10, 12 },
                { 14, 16, 18 },
            }, m.ToArray2D());
        }

        [Test]
        public void TestScalarMultiplicationStatic()
        {
            MatrixInt m = new MatrixInt(new[,]
            {
                { 0, 0, 0 },
                { 0, 5, 0 },
                { 0, 0, 0 }
            });

            MatrixInt m2 = MatrixInt.Multiply(m, 5);

            Assert.AreEqual(new[,]
            {
                { 0, 0, 0 },
                { 0, 25, 0 },
                { 0, 0, 0 },
            }, m2.ToArray2D());

            Assert.AreEqual(new[,]
            {
                { 0, 0, 0 },
                { 0, 5, 0 },
                { 0, 0, 0 },
            }, m.ToArray2D());
        }

        [Test]
        public void TestScalarMultiplicationOperator()
        {
            MatrixInt m = new MatrixInt(new[,]
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 }
            });

            //See Operator overloading documentation =>
            //https://docs.microsoft.com/fr-fr/dotnet/csharp/language-reference/operators/operator-overloading
            MatrixInt m2 = m * 2;

            Assert.AreEqual(new[,]
            {
                { 2, 4, 6 },
                { 8, 10, 12 },
                { 14, 16, 18 },
            }, m2.ToArray2D());

            Assert.AreEqual(new[,]
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 }
            }, m.ToArray2D());

            MatrixInt m4 = 4 * m;
            
            Assert.AreEqual(new[,]
            {
                { 4, 8, 12 },
                { 16, 20, 24 },
                { 28, 32, 36 },
            }, m4.ToArray2D());
        }

        [Test]
        public void TestNegativeMatrix()
        {
            MatrixInt m1 = new MatrixInt(new int[,]
            {
                { -1, 2, 3 },
                { 4, -5, 6 },
                { -7, 8, 9 }
            });

            MatrixInt m2 = -m1;
            
            Assert.AreEqual(new[,]
            {
                { 1, -2, -3 },
                { -4, 5, -6 },
                { 7, -8, -9 }
            }, m2.ToArray2D());
        }
    }
    
    public class MatrixInt
    {
        
        public int numberOfRow;
        public int numberOfCollumn;
        public int[,] matrix;
        public static MatrixInt operator *(MatrixInt m, int multiplier) => Multiply(m, multiplier);
        public static MatrixInt operator *( int multiplier,MatrixInt m) => Multiply(multiplier,m);
        public static MatrixInt operator -(MatrixInt m) => Multiply(m,-1);


        
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

        public MatrixInt(MatrixInt matrix)
        {
            
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
    }
}