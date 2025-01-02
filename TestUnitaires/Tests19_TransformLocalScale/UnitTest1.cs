using NUnit.Framework;
using System;

namespace Maths_Matrices.Tests
{
    [TestFixture]
    public class Tests19_TransformLocalScale
    {
        [Test]
        public void TestDefaultValues()
        {
            GlobalSettings.DefaultFloatingPointTolerance = 0.001d;
            
            Transform t = new Transform();

            //Default Scale
            Assert.AreEqual(1f, t.LocalScale.x);
            Assert.AreEqual(1f, t.LocalScale.y);
            Assert.AreEqual(1f, t.LocalScale.z);
            
            //Default Matrix
            Assert.AreEqual(new[,]
            {
                { 1f, 0f, 0f, 0f },
                { 0f, 1f, 0f, 0f },
                { 0f, 0f, 1f, 0f },
                { 0f, 0f, 0f, 1f },
            }, t.LocalScaleMatrix.ToArray2D());
            
            GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
        }

        [Test]
        public void TestChangeScale()
        {
            GlobalSettings.DefaultFloatingPointTolerance = 0.001d;
            
            Transform t = new Transform();

            //Scale X
            t.LocalScale = new Vector3(2f, 1f, 1f);
            Assert.AreEqual(new[,]
            {
                { 2f, 0f, 0f, 0f },
                { 0f, 1f, 0f, 0f },
                { 0f, 0f, 1f, 0f },
                { 0f, 0f, 0f, 1f },
            }, t.LocalScaleMatrix.ToArray2D());
            
            //Scale Y
            t.LocalScale = new Vector3(1f, 5f, 1f);
            Assert.AreEqual(new[,]
            {
                { 1f, 0f, 0f, 0f },
                { 0f, 5f, 0f, 0f },
                { 0f, 0f, 1f, 0f },
                { 0f, 0f, 0f, 1f },
            }, t.LocalScaleMatrix.ToArray2D());
            
            //Scale Z
            t.LocalScale = new Vector3(1f, 1f, 23f);
            Assert.AreEqual(new[,]
            {
                { 1f, 0f, 0f, 0f },
                { 0f, 1f, 0f, 0f },
                { 0f, 0f, 23f, 0f },
                { 0f, 0f, 0f, 1f },
            }, t.LocalScaleMatrix.ToArray2D());
            
            GlobalSettings.DefaultFloatingPointTolerance = 0.0d;
        }
    }

    public static class GlobalSettings
    {
        public static double DefaultFloatingPointTolerance { get; set; }
    }

    public class Transform
    {
        private Vector3 _localScale = new Vector3(1f, 1f, 1f);

        public Vector3 LocalScale
        {
            get { return _localScale; }
            set
            {
                _localScale = value;
                UpdateLocalScaleMatrix();
            }
        }

        public Matrix4x4 LocalScaleMatrix { get; private set; }

        public Transform()
        {
            LocalScaleMatrix = Matrix4x4.Identity;
        }

        private void UpdateLocalScaleMatrix()
        {
            LocalScaleMatrix = new Matrix4x4(
                _localScale.x, 0f, 0f, 0f,
                0f, _localScale.y, 0f, 0f,
                0f, 0f, _localScale.z, 0f,
                0f, 0f, 0f, 1f
            );
        }
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

    public class Matrix4x4
    {
        private float[,] _matrix = new float[4, 4];

        public Matrix4x4(
            float m00, float m01, float m02, float m03,
            float m10, float m11, float m12, float m13,
            float m20, float m21, float m22, float m23,
            float m30, float m31, float m32, float m33)
        {
            _matrix[0, 0] = m00; _matrix[0, 1] = m01; _matrix[0, 2] = m02; _matrix[0, 3] = m03;
            _matrix[1, 0] = m10; _matrix[1, 1] = m11; _matrix[1, 2] = m12; _matrix[1, 3] = m13;
            _matrix[2, 0] = m20; _matrix[2, 1] = m21; _matrix[2, 2] = m22; _matrix[2, 3] = m23;
            _matrix[3, 0] = m30; _matrix[3, 1] = m31; _matrix[3, 2] = m32; _matrix[3, 3] = m33;
        }

        public static Matrix4x4 Identity => new Matrix4x4(
            1f, 0f, 0f, 0f,
            0f, 1f, 0f, 0f,
            0f, 0f, 1f, 0f,
            0f, 0f, 0f, 1f
        );

        public float[,] ToArray2D()
        {
            return _matrix;
        }
    }
}
