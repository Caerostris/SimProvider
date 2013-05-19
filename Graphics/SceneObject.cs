using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
namespace SimProvider.Graphics
{
    public class SceneObject
    {
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public Vector3 Scale { get; set; }
        public string[] Texture { get; set; }
        public string[] Model { get; set; }
        public SceneObject(Vector3 pos, Vector3 rot, Vector3 scale, string model, string texture)
        {
            Position = pos;
            Rotation = rot;
            Scale = scale;
            Model = new string[]{model};
            Texture = new string[] { texture };
        }
        public SceneObject(Vector3 pos, Vector3 rot, Vector3 scale, string[] model, string[] texture)
        {
            Position = pos;
            Rotation = rot;
            Scale = scale;
            Model = model;
            Texture = texture;
        }
        public Matrix4 ObjectMatrix
        {
            get
            {
                return Matrix4.Scale(Scale) * Matrix4.CreateRotationY(Rotation.Y) * Matrix4.CreateRotationX(Rotation.X) * Matrix4.CreateRotationZ(Rotation.Z) * Matrix4.CreateTranslation(Position);
            }
        }
        public SceneObject Clone
        {
            get { return (SceneObject)this.MemberwiseClone(); }
        }
    }
}
