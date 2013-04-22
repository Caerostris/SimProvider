using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace SimProvider.Graphics
{
    public class Model
    {
        List<Vector3> vertices = new List<Vector3>();
        List<Vector3> normals = new List<Vector3>();
        List<Vector2> texCoords = new List<Vector2>();
        List<Face> faces = new List<Face>();

        public string Name { get; set; }
        public List<Vector3> Vertices { get { return vertices; } set { vertices = value; } }
        public List<Vector3> Normals { get { return normals; } set { normals = value; } }
        public List<Vector2> TexCoords { get { return texCoords; } set { texCoords = value; } }
        public List<Face> Faces { get { return faces; } set { faces = value; } }
    }
}
