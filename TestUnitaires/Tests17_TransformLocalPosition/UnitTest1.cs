using NUnit.Framework;

namespace Maths_Matrices.Tests
{
    [TestFixture]
    public class Tests17_TransformLocalPosition
    {
        [Test]
        public void TestDefaultValues()
        {
            GlobalSettings.DefaultFloatingPointTolerance = 0.001d;
            
            Transform t = new Transform();
            
            //Default Position
            Assert.AreEqual(0f, t.LocalPosition.x);
            Assert.AreEqual(0f, t.LocalPosition.y);
            Assert.AreEqual(0f, t.LocalPosition.z);

            //Default Translation Matrix
            Assert.AreEqual(new[,]
            {
                { 1f, 0f, 0f, 0f },
                { 0f, 1f, 0f, 0f },
                { 0f, 0f, 1f, 0f },
                { 0f, 0f, 0f, 1f },
            }, t.LocalTranslationMatrix.ToArray2D());
            
            GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
        }
        
        [Test]
        public void TestTransformChangePosition()
        {
            GlobalSettings.DefaultFloatingPointTolerance = 0.001d;
            
            Transform t = new Transform();
            
            //Translation
            t.LocalPosition = new Vector3(5f, 2f, 1f);
            Assert.AreEqual(new[,]
            {
                { 1f, 0f, 0f, 5f },
                { 0f, 1f, 0f, 2f },
                { 0f, 0f, 1f, 1f },
                { 0f, 0f, 0f, 1f },
            }, t.LocalTranslationMatrix.ToArray2D());

            GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
        }
        
    }

    public static class GlobalSettings
    {
        public static double DefaultFloatingPointTolerance { get; set; } = 0.0d;
    }

    public struct Vector3
    {
        public float x, y, z;

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    public class Transform
    {
        private Vector3 localPosition;
        private float[,] localTranslationMatrix;

        public Transform()
        {
            localPosition = new Vector3(0f, 0f, 0f);
            localTranslationMatrix = new float[4, 4]
            {
                { 1f, 0f, 0f, 0f },
                { 0f, 1f, 0f, 0f },
                { 0f, 0f, 1f, 0f },
                { 0f, 0f, 0f, 1f },
            };
        }

        public Vector3 LocalPosition
        {
            get => localPosition;
            set
            {
                localPosition = value;
                UpdateTranslationMatrix();
            }
        }

        public Matrix LocalTranslationMatrix => new Matrix(localTranslationMatrix);

        private void UpdateTranslationMatrix()
        {
            localTranslationMatrix[0, 3] = localPosition.x;
            localTranslationMatrix[1, 3] = localPosition.y;
            localTranslationMatrix[2, 3] = localPosition.z;
        }
    }

    public class Matrix
    {
        private readonly float[,] values;

        public Matrix(float[,] values)
        {
            this.values = values;
        }

        public float[,] ToArray2D()
        {
            return values;
        }

        public override bool Equals(object obj)
        {
            if (obj is Matrix other)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (System.Math.Abs(values[i, j] - other.values[i, j]) > GlobalSettings.DefaultFloatingPointTolerance)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return values.GetHashCode();
        }
    }
}
