namespace Test03_IdentityMatices;

public class Tests
{
    [TestFixture]
    public class Tests03_IdentityMatrices
    {
        [Test]
        public void TestGenerateIdentityMatrices()
        {
            MatrixInt identity2 = MatrixInt.Identity(2);
            Assert.AreEqual(new[,]
            {
                { 1, 0 },
                { 0, 1 },
            }, identity2.ToArray2D());

            MatrixInt identity3 = MatrixInt.Identity(3);
            Assert.AreEqual(new[,]
            {
                { 1, 0, 0 },
                { 0, 1, 0 },
                { 0, 0, 1 },
            }, identity3.ToArray2D());

            MatrixInt identity4 = MatrixInt.Identity(4);
            Assert.AreEqual(new[,]
            {
                { 1, 0, 0, 0 },
                { 0, 1, 0, 0 },
                { 0, 0, 1, 0 },
                { 0, 0, 0, 1 },
            }, identity4.ToArray2D());
        }

        [Test]
        public void TestMatricesIsIdentity()
        {
            MatrixInt identity2 = new MatrixInt(new[,]
            {
                { 1, 0 },
                { 0, 1 }
            });
            Assert.IsTrue(identity2.IsIdentity());

            MatrixInt identity3 = new MatrixInt(new[,]
            {
                { 1, 0, 0 },
                { 0, 1, 0 },
                { 0, 0, 1 }
            });
            Assert.IsTrue(identity3.IsIdentity());

            MatrixInt notSameColumnsAndLines = new MatrixInt(new[,]
            {
                { 1, 0, 0 },
                { 0, 1, 0 }
            });
            Assert.IsFalse(notSameColumnsAndLines.IsIdentity());

            MatrixInt notIdentity1 = new MatrixInt(new[,]
            {
                { 1, 0, 0 },
                { 0, 2, 0 },
                { 0, 0, 3 },
            });
            Assert.IsFalse(notIdentity1.IsIdentity());

            MatrixInt notIdentity2 = new MatrixInt(new[,]
            {
                { 1, 0, 4 },
                { 0, 1, 0 },
                { 0, 0, 1 },
            });
            Assert.IsFalse(notIdentity2.IsIdentity());
        }
        public class MatrixInt
        {
            int numberOfRows;
            int numberOfColumns;
            int[,] matrix;


            public MatrixInt(int numberOfRows, int numberOfColumns)
            {
                this.numberOfRows = numberOfRows;
                this.numberOfColumns = numberOfColumns;
                matrix = new int[numberOfRows, numberOfColumns];
            }
            
            
            public MatrixInt(int[,] NMatrix)
            {
                matrix = NMatrix;
                this.numberOfRows = matrix.GetLength(0);
                this.numberOfColumns = matrix.GetLength(1);
            }

            public MatrixInt(MatrixInt newMatrixInt)
            {
             this.matrix = newMatrixInt.matrix;
            }
            public int this[int row, int column]
            {
                get { return matrix[row, column]; }
                set { matrix[row, column] = value; }    
            }

            public int[,] ToArray2D()
            {
                int[,] newMatrix = new int[numberOfRows, numberOfColumns];
            
                for (int i = 0; i < numberOfRows; i++)
                {
                    for (int j = 0; j < numberOfColumns; j++)
                    {
                        newMatrix[i, j] = matrix[i, j];
                    }
                }
                return newMatrix;
            }
            public static MatrixInt Identity(int numberOfRowsNCollumns)
            {

                
                int[,] newMatrix = new int[numberOfRowsNCollumns, numberOfRowsNCollumns];
                
                for (int i = 0; i < numberOfRowsNCollumns; i++)
                {
                    for (int j = 0; j < numberOfRowsNCollumns; j++)
                    {
                        if (i == j)
                        {
                            newMatrix[i, j] = 1;
                        }
                        else
                        {
                            newMatrix[i, j] = 0;
                        }
                    }
                }
                return new MatrixInt(newMatrix);
            }

            public bool IsIdentity()
            {
                bool isIdentity = true;
                for (int i = 0; i < numberOfRows; i++)
                {
                    for (int j = 0; j < numberOfColumns; j++)
                    {
                        if (i == j)
                        {
                            if (matrix[i, j] == 1)
                            {
                                isIdentity = true;
                            }
                            if (matrix[i, j] == 0)
                            {
                                isIdentity = false;
                            }
                        }
                        else
                        {
                            if (matrix[i, j] == 0)
                            {
                                isIdentity = true;
                            }

                            if (matrix[i, j] == 1)
                            {
                                isIdentity = false;
                            }
                        }
                    }
                }
                return isIdentity;
            }
        }

        
    }
}