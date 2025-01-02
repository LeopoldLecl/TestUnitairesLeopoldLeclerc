namespace TestUnitaires02_CopyAndModifiy;

public class Tests
{
    [TestFixture]
    public class Tests02_CopyAndModifyMatrices
    {
        [Test]
        public void TestModifyMatrix()
        {
            MatrixInt m = new MatrixInt(2, 2);
            Assert.AreEqual(new[,]
            {
                { 0, 0 },
                { 0, 0 },
            }, m.ToArray2D());

            //See Indexers Documentation =>
            //https://docs.microsoft.com/fr-fr/dotnet/csharp/programming-guide/indexers/
            m[0, 0] = 1;
            m[0, 1] = 2;
            m[1, 0] = 3;
            m[1, 1] = 4;

            Assert.AreEqual(new[,]
            {
                { 1, 2 },
                { 3, 4 },
            }, m.ToArray2D());
        }

        [Test]
        public void TestCopyAndChangeMatrices()
        {
            MatrixInt m1 = new MatrixInt(new[,]
                {
                    { 1, 2, 3 },
                    { 4, 5, 6 },
                    { 7, 8, 9 },
                }
            );

            MatrixInt m2 = new MatrixInt(m1);
            m2[0, 0] = 23;

            Assert.AreEqual(new[,]
            {
                { 23, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 },
            }, m2.ToArray2D());

            Assert.AreEqual(new[,]
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 },
            }, m1.ToArray2D());
        }
    }


    public class MatrixInt
    {
        
        public int numberOfRow;
        public int numberOfCollumn;
        public int[,] matrix;
        
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
        public int this[int row, int column]
        {
            get { return matrix[row, column]; }
            set { matrix[row, column] = value; }    
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

}