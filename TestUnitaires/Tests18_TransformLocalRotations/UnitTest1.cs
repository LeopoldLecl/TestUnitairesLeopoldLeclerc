using NUnit.Framework;
using System;

namespace Maths_Matrices.Tests
{
    
    [TestFixture]
    public class Tests18_TransformLocalRotations
    {
        [Test]
        public void TestDefaultValues()
        {
            GlobalSettings.DefaultFloatingPointTolerance = 0.001d;
            
            Transform t = new Transform();
            
            //Default Rotation
            Assert.AreEqual(0f, t.LocalRotation.x);
            Assert.AreEqual(0f, t.LocalRotation.y);
            Assert.AreEqual(0f, t.LocalRotation.z);

            //Default Matrices
            Assert.AreEqual(new[,]
            {
                { 1f, 0f, 0f, 0f },
                { 0f, 1f, 0f, 0f },
                { 0f, 0f, 1f, 0f },
                { 0f, 0f, 0f, 1f },
            }, t.LocalRotationXMatrix.ToArray2D());
            
            Assert.AreEqual(new[,]
            {
                { 1f, 0f, 0f, 0f },
                { 0f, 1f, 0f, 0f },
                { 0f, 0f, 1f, 0f },
                { 0f, 0f, 0f, 1f },
            }, t.LocalRotationYMatrix.ToArray2D());
            
            Assert.AreEqual(new[,]
            {
                { 1f, 0f, 0f, 0f },
                { 0f, 1f, 0f, 0f },
                { 0f, 0f, 1f, 0f },
                { 0f, 0f, 0f, 1f },
            }, t.LocalRotationZMatrix.ToArray2D());
            
            Assert.AreEqual(new[,]
            {
                { 1f, 0f, 0f, 0f },
                { 0f, 1f, 0f, 0f },
                { 0f, 0f, 1f, 0f },
                { 0f, 0f, 0f, 1f },
            }, t.LocalRotationMatrix.ToArray2D());
            
            GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
        }
        
        [Test]
        public void TestChangeRotationXAxis()
        {
            GlobalSettings.DefaultFloatingPointTolerance = 0.001d;
            
            Transform t = new Transform();
            
            t.LocalRotation = new Vector3(30f, 0f, 0f);
            Assert.AreEqual(new[,]
            {
                { 1f, 0f, 0f, 0f },
                { 0f, 0.866f, -0.5f, 0f },
                { 0f, 0.5f, 0.866f, 0f },
                { 0f, 0f, 0f, 1f },
            }, t.LocalRotationXMatrix.ToArray2D());
            
            Assert.AreEqual(new[,]
            {
                { 1f, 0f, 0f, 0f },
                { 0f, 0.866f, -0.5f, 0f },
                { 0f, 0.5f, 0.866f, 0f },
                { 0f, 0f, 0f, 1f },
            }, t.LocalRotationMatrix.ToArray2D());

            GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
        }
        
        [Test]
        public void TestChangeRotationYAxis()
        {
            GlobalSettings.DefaultFloatingPointTolerance = 0.001d;
            
            Transform t = new Transform();

            t.LocalRotation = new Vector3(0f, 30f, 0f);
            Assert.AreEqual(new[,]
            {
                { 0.866f, 0f, 0.5f, 0f },
                { 0f, 1f, 0f, 0f },
                { -0.5f, 0f, 0.866f, 0f },
                { 0f, 0f, 0f, 1f },
            }, t.LocalRotationYMatrix.ToArray2D());
            
            Assert.AreEqual(new[,]
            {
                { 0.866f, 0f, 0.5f, 0f },
                { 0f, 1f, 0f, 0f },
                { -0.5f, 0f, 0.866f, 0f },
                { 0f, 0f, 0f, 1f },
            }, t.LocalRotationMatrix.ToArray2D());
            
            
            GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
        }
        
        [Test]
        public void TestChangeRotationZAxis()
        {
            GlobalSettings.DefaultFloatingPointTolerance = 0.001d;
            
            Transform t = new Transform();

            t.LocalRotation = new Vector3(0f, 0f, 30f);
            Assert.AreEqual(new[,]
            {
                { 0.866f, -0.5f, 0f, 0f },
                { 0.5f, 0.866f, 0f, 0f },
                { 0f, 0f, 1f, 0f },
                { 0f, 0f, 0f, 1f },
            }, t.LocalRotationZMatrix.ToArray2D());
            
            Assert.AreEqual(new[,]
            {
                { 0.866f, -0.5f, 0f, 0f },
                { 0.5f, 0.866f, 0f, 0f },
                { 0f, 0f, 1f, 0f },
                { 0f, 0f, 0f, 1f },
            }, t.LocalRotationMatrix.ToArray2D());

            GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
        }
        
        [Test]
        public void TestChangeRotationMultipleAxis()
        {
            GlobalSettings.DefaultFloatingPointTolerance = 0.001d;
            
            Transform t = new Transform();
            
            //For LocalRotationMatrix attribute =>
            //Rotations are performed around the Z axis, the X axis, and the Y axis, in that order.
            //Like Unity => https://docs.unity3d.com/ScriptReference/Transform-eulerAngles.html
            t.LocalRotation = new Vector3(30f, 60f, 90f);
            
            Assert.AreEqual(new[,]
            {
                { 0f, 0f, 1f, 0f },
                { 1f, 0f, 0f, 0f },
                { 0f, 1f, 0f, 0f },
                { 0f, 0f, 0f, 1f },
            }, t.LocalRotationMatrix.ToArray2D());
            
            GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
        }
    }
    public class Vector3
    {
        public float x, y, z;
        public Vector3(float x = 0f, float y = 0f, float z = 0f)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    public static class GlobalSettings
    {
        public static double DefaultFloatingPointTolerance { get; set; } = 0.0d;
    }

    public static class MatrixExtensions
    {
        public static float[,] ToArray2D(this float[,] matrix) => matrix; // No conversion needed
    }

    public class Transform
    {
        private Vector3 localRotation;
        public Vector3 LocalRotation
        {
            get => localRotation;
            set
            {
                localRotation = value;
                UpdateMatrices();
            }
        }

        public float[,] LocalRotationXMatrix { get; private set; }
        public float[,] LocalRotationYMatrix { get; private set; }
        public float[,] LocalRotationZMatrix { get; private set; }
        public float[,] LocalRotationMatrix { get; private set; }

        public Transform()
        {
            localRotation = new Vector3();
            UpdateMatrices();
        }

        private void UpdateMatrices()
        {
            LocalRotationXMatrix = CalculateRotationX(localRotation.x);
            LocalRotationYMatrix = CalculateRotationY(localRotation.y);
            LocalRotationZMatrix = CalculateRotationZ(localRotation.z);

            LocalRotationMatrix = MultiplyMatrices(
                MultiplyMatrices(LocalRotationYMatrix, LocalRotationXMatrix),
                LocalRotationZMatrix);
        }

        private float[,] CalculateRotationX(float angle)
        {
            float rad = DegreesToRadians(angle);
            return new float[,]
            {
                { 1f, 0f, 0f, 0f },
                { 0f, (float)Math.Cos(rad), -(float)Math.Sin(rad), 0f },
                { 0f, (float)Math.Sin(rad), (float)Math.Cos(rad), 0f },
                { 0f, 0f, 0f, 1f }
            };
        }

        private float[,] CalculateRotationY(float angle)
        {
            float rad = DegreesToRadians(angle);
            return new float[,]
            {
                { (float)Math.Cos(rad), 0f, (float)Math.Sin(rad), 0f },
                { 0f, 1f, 0f, 0f },
                { -(float)Math.Sin(rad), 0f, (float)Math.Cos(rad), 0f },
                { 0f, 0f, 0f, 1f }
            };
        }

        private float[,] CalculateRotationZ(float angle)
        {
            float rad = DegreesToRadians(angle);
            return new float[,]
            {
                { (float)Math.Cos(rad), -(float)Math.Sin(rad), 0f, 0f },
                { (float)Math.Sin(rad), (float)Math.Cos(rad), 0f, 0f },
                { 0f, 0f, 1f, 0f },
                { 0f, 0f, 0f, 1f }
            };
        }

        private float[,] MultiplyMatrices(float[,] m1, float[,] m2)
        {
            int size = m1.GetLength(0);
            float[,] result = new float[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    result[i, j] = 0f;
                    for (int k = 0; k < size; k++)
                    {
                        result[i, j] += m1[i, k] * m2[k, j];
                    }
                }
            }
            return result;
        }

        private float DegreesToRadians(float degrees)
        {
            return (float)(degrees * Math.PI / 180.0);
        }
    }

}
