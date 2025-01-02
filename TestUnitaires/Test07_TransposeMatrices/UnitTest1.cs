using NUnit.Framework;

namespace Test07_TransposeMatrices
{
    [TestFixture]
    public class Test07TransposeMatrices
    {
        [Test]
        public void TestTransposeMatrixInstance()
        {
            MatrixInt m1 = new MatrixInt(new[,]
            {
                { 1, 2, 3 },
                { 4, 5, 6 }
            });

            MatrixInt m1t = m1.Transpose();

            Assert.AreEqual(new[,]
            {
                { 1, 4 },
                { 2, 5 },
                { 3, 6 }
            }, m1t.ToArray2D());
        }
        
        [Test]
        public void TestTransposeMatrixStatic()
        {
            MatrixInt m1 = new MatrixInt(new[,]
            {
                { 1, 2, 3 },
                { 4, 5, 6 }
            });

            MatrixInt m1t = MatrixInt.Transpose(m1);

            Assert.AreEqual(new[,]
            {
                { 1, 4 },
                { 2, 5 },
                { 3, 6 }
            }, m1t.ToArray2D());
        }
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

        public MatrixInt Transpose()
        {
            MatrixInt newMatrix = new MatrixInt(numberOfCollumn, numberOfRow);

            for (int i = 0; i < numberOfRow; i++)
            {
                for (int j = 0; j < numberOfCollumn; j++)
                {
                    newMatrix[j, i] = matrix[i, j];
                }
            }

            return newMatrix;
        }

        
        public static MatrixInt Transpose(MatrixInt m)
        {
            MatrixInt newMatrix = new MatrixInt(m.numberOfCollumn, m.numberOfRow);

            for (int i = 0; i < m.numberOfRow; i++)
            {
                for (int j = 0; j < m.numberOfCollumn; j++)
                {
                    newMatrix[j, i] = m[i, j];
                }
            }

            return newMatrix;
        }

     
        
      


        
        
        
       
        
     
        
     
        }
      
    }
      
    
        public class MatrixMultiplyException : Exception;
    
