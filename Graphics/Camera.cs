using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;


namespace SimProvider.Graphics
{
    public class Camera
    {
        private Vector3 position;
        public Vector3 Position { get { return position; } set { position = value; } }
        private Vector3 rotation;
        public Vector3 Rotation { get { return rotation; } set { rotation = value; } }

        private Matrix4 projectionMatrix;
        public Matrix4 ProjectionMatrix { get { return projectionMatrix; } set { projectionMatrix = value; } }
        

        public Matrix4 ViewMatrix
        {
            get
            {
                return Matrix4.CreateRotationX(rotation.X) * Matrix4.CreateRotationY(rotation.Y) * Matrix4.CreateRotationZ(rotation.Z) * Matrix4.CreateTranslation(position);
            }
        }

        public Matrix4 ViewProjection
        {
            get
            {
                return ViewMatrix * ProjectionMatrix;
            }
        }

        public Camera(float fovy,float aspect,float near,float far)
        {
            projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(fovy, aspect, near, far);
        }
        public Camera(float fovy, float aspect, float near, float far,Vector3 pos, Vector3 tar)
        {
            projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(fovy, aspect, near, far);
            position = pos;

        }
    }
}
